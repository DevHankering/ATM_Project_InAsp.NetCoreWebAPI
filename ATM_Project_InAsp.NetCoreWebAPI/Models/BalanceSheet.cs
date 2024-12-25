using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATM_Project_InAsp.NetCoreWebAPI.Enum;

namespace ATM_Project_InAsp.NetCoreWebAPI.Models
{
    public class BalanceSheet
    {
        [Key]
        public Guid Transaction_ID { get; set; }
        //Foreign key
        public Guid? Customer_ID { get; set; }

        public string? CustomerName { get; set; }
        public DateTime? UpdatedAtLocalTimeZone { get; set; }
        public DateTime? UpdatedAtUniversalTimeZone { get; set; }        
        //public DCEnum? DC {  get; set; }
        public string? CD { get; set; }
        //public decimal? Credit { get; set; }
        //public decimal? Debit { get; set; }
        public decimal? CurrentBalanceAmount { get; set; }




        //Navigation property
        public UserModel? User { get; set; }
    }
}
