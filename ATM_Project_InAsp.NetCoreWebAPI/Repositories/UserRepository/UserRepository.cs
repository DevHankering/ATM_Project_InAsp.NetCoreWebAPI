
using ATM_Project_InAsp.NetCoreWebAPI.Data;
using ATM_Project_InAsp.NetCoreWebAPI.DTO;
using ATM_Project_InAsp.NetCoreWebAPI.Models;
using ATM_Project_InAsp.NetCoreWebAPI.RTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace ATM_Project_InAsp.NetCoreWebAPI.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly Account_Db account_Db;

        public UserRepository(Account_Db account_Db)
        {
            this.account_Db = account_Db;
        }

        public async Task<BalanceResponseRto> CDMoney(BalanceDto balanceDto)
        {
            var user =await account_Db
                .Users
                .Include(x => x.Balance)    
                .FirstOrDefaultAsync(x => x.Name == balanceDto.Name);

            if(user == null)
            {
                return null;
            }

            if(balanceDto.CD == 0)
            {
                user.Balance.BalanceAmount += balanceDto.BalanceAmount;
            }
            else
            {
                user.Balance.BalanceAmount -= balanceDto.BalanceAmount;
            }

            bool z;
            if (balanceDto.CD == 0)
            {
                z = false;
            }
            else
            {
                z = true;
            }

            var balanceSheet = new BalanceSheet()
            {
                Customer_ID = user.Id,
                CustomerName = user.Name,
                UpdatedAtLocalTimeZone = DateTime.Now,
                UpdatedAtUniversalTimeZone = DateTime.UtcNow,
                CD = z ? $"Debited = {balanceDto.BalanceAmount}" : $"Credited = {balanceDto.BalanceAmount}",

                CurrentBalanceAmount = user.Balance.BalanceAmount

            };

            await account_Db.BalanceSheet.AddAsync(balanceSheet);

            account_Db.Entry(user).State = EntityState.Modified;

            await account_Db.SaveChangesAsync();

            var balance = user.Balance.BalanceAmount;
            var balanceRto = new BalanceResponseRto()
            {
                BalanceAmount = balance,
            };
            return balanceRto;

        }

        public async Task<BalanceResponseRto> CheckBalance(string Name)
        {
            var user = await account_Db
                .Users
                .Include(a => a.Balance)
                .FirstOrDefaultAsync(x => x.Name == Name);

            var balance = user.Balance.BalanceAmount;
            var balanceRto = new BalanceResponseRto()
            {
                BalanceAmount = balance,
            };
            return balanceRto;
        }

        public async Task<UserModel> CreateUser(UserModel userDomainModel)
        {
            var userEntry = await account_Db.AddAsync(userDomainModel);
            await account_Db.SaveChangesAsync();
            var user = userEntry.Entity;
            return user;

        }

        public async Task<List<UserModel>> GetAllUsers()
        {
           var  users =   await account_Db.Users.
                Include(a => a.Document).ToListAsync();
            return users;
        }

        public async Task<UserModel> GetUserById(Guid id)
        {
            return await account_Db.Users.Include(a => a.Document).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<BalanceSheet>> GetYourDataAsync()
        {
            return await account_Db.BalanceSheet.ToListAsync();
        }


    }
}