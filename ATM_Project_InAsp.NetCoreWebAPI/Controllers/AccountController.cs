using System.Security.Cryptography.X509Certificates;
using ATM_Project_InAsp.NetCoreWebAPI.Data;
using ATM_Project_InAsp.NetCoreWebAPI.DTO;
using ATM_Project_InAsp.NetCoreWebAPI.Models;
using ATM_Project_InAsp.NetCoreWebAPI.Repositories.TokenRepository;
using ATM_Project_InAsp.NetCoreWebAPI.Repositories.UserRepository;
using ATM_Project_InAsp.NetCoreWebAPI.RTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//For pdf
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using OfficeOpenXml;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
//...

namespace ATM_Project_InAsp.NetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly Account_Db account_Db;
        private static readonly Random _random = new Random();


        public AccountController(IUserRepository userRepository, UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, Account_Db account_db)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            account_Db = account_db;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserRequestDto userRequestDto)
        {
            var adhaarFileName = Path.GetFileName(userRequestDto.AdhaarCard.FileName);
            var adhaarFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\Documents", adhaarFileName);

            var panCardFileName = Path.GetFileName(userRequestDto.PanCard.FileName);
            var panCardFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\Documents", panCardFileName);

            var directory = Path.GetDirectoryName(adhaarFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save adhaar file into the server
            using (var stream = new FileStream(adhaarFilePath, FileMode.Create))
            {
                await userRequestDto.AdhaarCard.CopyToAsync(stream);
            }

            // Save panCard file into the server
            using (var stream = new FileStream(panCardFilePath, FileMode.Create))
            {
                await userRequestDto.AdhaarCard.CopyToAsync(stream);
            }

            var userDomainModel = new UserModel()
            {
                Name = userRequestDto.Name,
                Address = userRequestDto.Address,
                PhoneNumber = userRequestDto.PhoneNumber,
                //CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedAtLocalTimeZone = DateTime.Now,
                CreatedAtUniversalTimeZone = DateTime.UtcNow,
                Document = new DocumentModel()
                {
                    AdhaarCardUrl = adhaarFilePath,
                    PanCardUrl = panCardFilePath,
                },
                Balance = new BalanceModel()
                {
                    BalanceAmount = 0,
                }
            };

            userDomainModel = await userRepository.CreateUser(userDomainModel);
            if (userDomainModel == null)
            {
                return NotFound();
            }

            //Generate random 4 digit no.
            long Pmin = 1000;
            long Pmax = 9999;
            var PinNo = (_random.NextDouble() * (Pmax - Pmin) + Pmin).ToString();


            //Register User
            var identityUser = new IdentityUser
            {
                UserName = userRequestDto.Name,
                Email = userRequestDto.Name
            };

            var identityResult = await userManager.CreateAsync(identityUser, PinNo);

            if (identityResult.Succeeded)
            {

                identityResult = await userManager.AddToRolesAsync(identityUser, ["User"]);

                if (identityResult.Succeeded)
                {


                    return Ok($"User was registered! Please login. and The Pin is {PinNo} and userName is {userDomainModel.Name}");
                }
            }
            return BadRequest("Something went wrong");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        //return Ok(jwtToken);
                        var response = new LoginResponseRto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);

                    }

                }
            }

            return BadRequest("Username or password incorrect");
        }



        [HttpGet]
        [Route("getAllUsers")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllUsers()
        {
            var UserDomainModel = await userRepository.GetAllUsers();

            //Convert Domain model to Dto
            var userResponseRto = new List<UserResponseRto>();
            foreach (var user in UserDomainModel)
            {
                userResponseRto.Add(new UserResponseRto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    AdhaarCardUrl = user.Document.AdhaarCardUrl,
                    PanCardUrl = user.Document.PanCardUrl,
                    CreatedAtLocalTimeZone = user.CreatedAtLocalTimeZone,
                    CreatedAtUniversalTimeZone = user.CreatedAtUniversalTimeZone,
                });
            }

            return Ok(userResponseRto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await userRepository.GetUserById(id);
            return Ok(user);
        }



        [HttpGet]
        [Route("checkBankBalance{Name}")]
        public async Task<IActionResult> CheckBalance(string Name)
        {
            var balance = await userRepository.CheckBalance(Name);

            return Ok(balance);
        }

        [HttpPut]
        [Route("CreditDebitMoney")]
        public async Task<IActionResult> CDMoney([FromForm] BalanceDto balanceDto)
        {
            var balance = await userRepository.CDMoney(balanceDto);
            return Ok($"your current balance is: {balance.BalanceAmount}");
        }




//Upload Excel File

        [HttpPost("uploadExcellFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                // Load the Excel file
                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    // Get the first worksheet from the Excel file
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    // Create a list to hold the user data
                    var users = new List<ExcelData>();

                    // Iterate through each row and extract the data
                    for (int row = 2; row <= rowCount; row++)  // Assuming first row is header
                    {
                        var user = new ExcelData
                        {
                            Name = worksheet.Cells[row, 1].Text,
                            Address = worksheet.Cells[row, 2].Text,
                            PhoneNumber = worksheet.Cells[row, 3].Text
                        };
                        users.Add(user);
                    }

                    // Insert data into the database
                    await account_Db.ExcelData.AddRangeAsync(users);
                    await account_Db.SaveChangesAsync();

                    return Ok(new { message = "File uploaded and data inserted successfully!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing the file.", error = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetExcelData")]
        public async Task<IActionResult> GetExcelData()
        {
            var getExcelData = await account_Db.ExcelData.ToListAsync();
            return Ok(getExcelData);
        }

       
        //Upload CSV file

        [HttpPost("uploadCsvFile")]
        public async Task<IActionResult> UploadCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var products = new List<CsvDataModel>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0; // Reset the stream position after the copy

                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Assume CSV has a header row
                    Delimiter = ",", // CSV delimiter (e.g., ',' or ';')
                }))
                {
                    // Read records and map to the Product model
                    var records = csv.GetRecords<CsvDataModel>().ToList();
                    products.AddRange(records);
                }
            }

            // Save the parsed data to the database
            await account_Db.CsvData.AddRangeAsync(products);
            await account_Db.SaveChangesAsync();

            return Ok(new { message = "CSV file uploaded and data saved successfully." });
        }

        [HttpGet]
        [Route("GetCSVFileData")]
        public async Task<IActionResult> GetCsvData()
        {
            var getCsvData = await account_Db.CsvData.ToListAsync();
            return Ok(getCsvData);
        }


    }

}

