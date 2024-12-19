
using ATM_Project_InAsp.NetCoreWebAPI.Data;
using ATM_Project_InAsp.NetCoreWebAPI.Models;
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

        public async Task<UserDomainModel> CreateUser(UserDomainModel userDomainModel)
        {
            var userEntry = await account_Db.AddAsync(userDomainModel);
            await account_Db.SaveChangesAsync();
            var user = userEntry.Entity;
            return user;

        }
    }
}