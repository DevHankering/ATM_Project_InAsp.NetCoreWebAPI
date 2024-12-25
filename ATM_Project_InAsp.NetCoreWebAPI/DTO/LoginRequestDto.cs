using System.ComponentModel.DataAnnotations;

namespace ATM_Project_InAsp.NetCoreWebAPI.DTO
{
    public class LoginRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
