using ATM_Project_InAsp.NetCoreWebAPI.Models;

namespace ATM_Project_InAsp.NetCoreWebAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<UserDomainModel> CreateUser(UserDomainModel userDomainModel);
    }
}
