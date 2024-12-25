using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_Project_InAsp.NetCoreWebAPI.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public long? PhoneNumber { get; set; }
        public DateTime? CreatedAtLocalTimeZone { get; set; }
        public DateTime? CreatedAtUniversalTimeZone { get; set; }
        // Foreign keys
        public Guid? DocumentId { get; set; }
        public Guid? BalanceId { get; set; }
       

        // Navigation property for Document (One-to-One)
        public DocumentModel? Document { get; set; }
        public BalanceModel? Balance { get; set; }
        public ICollection<BalanceSheet?> BalanceSheet { get; set; }

    }
}
