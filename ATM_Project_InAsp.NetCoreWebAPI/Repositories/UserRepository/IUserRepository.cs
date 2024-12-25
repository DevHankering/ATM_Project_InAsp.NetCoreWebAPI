using ATM_Project_InAsp.NetCoreWebAPI.DTO;
using ATM_Project_InAsp.NetCoreWebAPI.Models;
using ATM_Project_InAsp.NetCoreWebAPI.RTO;
using Microsoft.AspNetCore.Mvc;

namespace ATM_Project_InAsp.NetCoreWebAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<BalanceResponseRto> CDMoney(BalanceDto balanceDto);
        Task<BalanceResponseRto> CheckBalance(string Name);
        Task<UserModel> CreateUser(UserModel userDomainModel);
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(Guid id);
        Task<List<BalanceSheet>> GetYourDataAsync();
    }
}
