using Microsoft.AspNetCore.Identity;

namespace ATM_Project_InAsp.NetCoreWebAPI.Repositories.TokenRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
