using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using ATM_Project_InAsp.NetCoreWebAPI.Enum;

namespace ATM_Project_InAsp.NetCoreWebAPI.DTO
{
    public class UserRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public long? PhoneNumber { get; set; }
        public IFormFile? AdhaarCard { get; set; }
        public IFormFile? PanCard { get; set; }
        //public CDEnum? CD { get; set; }
        //public decimal? Amount { get; set; }
    }
}
