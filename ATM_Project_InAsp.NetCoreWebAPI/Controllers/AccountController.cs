using ATM_Project_InAsp.NetCoreWebAPI.DTO;
using ATM_Project_InAsp.NetCoreWebAPI.Models;
using ATM_Project_InAsp.NetCoreWebAPI.Repositories.UserRepository;
using ATM_Project_InAsp.NetCoreWebAPI.RTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM_Project_InAsp.NetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private static readonly Random _random = new Random();


        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("CreateUser")]
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

            var userDomainModel = new UserDomainModel()
            {
                Name = userRequestDto.Name,
                Address = userRequestDto.Address,
                PhoneNumber = userRequestDto.PhoneNumber,
                //CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedAtLocalTimeZone = DateTime.Now,
                CreatedAtUniversalTimeZone = DateTime.UtcNow,
                Documents = new DocumentDomainModel()
                {
                    AdhaarCardUrl = adhaarFilePath,
                    PanCardUrl = panCardFilePath,
                }
            };

            userDomainModel = await userRepository.CreateUser(userDomainModel);
            if(userDomainModel == null)
            {
                return NotFound();
            }

            //Generate random 10 digit no.
            long min = 1000000000;
            long max = 9999999999;
            var AccountNo = (long)(_random.NextDouble() * (max - min) + min); // Generate random number between min and max


            //Generate random 4 digit no.
            long Pmin = 1000;
            long Pmax = 9999;
            var PinNo = (long)(_random.NextDouble() * (Pmax - Pmin) + Pmin);


            var userResponseRto = new UserResponseRto()
            {
                Name = userDomainModel.Name,
                Address = userDomainModel.Address,
                PhoneNumber = userDomainModel.PhoneNumber,
                CreatedAtLocalTimeZone = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedAtUniversalTimeZone = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                AdhaarCardUrl = userDomainModel.Documents.AdhaarCardUrl,
                PanCardUrl = userDomainModel.Documents.PanCardUrl,
                AccountNo = AccountNo,
                PinNo = PinNo,
            };
            return Ok(userResponseRto);
        }
    }
}
