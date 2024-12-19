using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_Project_InAsp.NetCoreWebAPI.Models
{
    public class UserDomainModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public long? PhoneNumber { get; set; }
        public DateTime? CreatedAtLocalTimeZone { get; set; }
        public DateTime? CreatedAtUniversalTimeZone { get; set; }

        //Foreign key
        public Guid DocumentId { get; set; }

        //Navigation Property
        public DocumentDomainModel? Documents { get; set; }
    }
}
