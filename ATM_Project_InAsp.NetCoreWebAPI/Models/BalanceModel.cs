using System.ComponentModel.DataAnnotations;
using ATM_Project_InAsp.NetCoreWebAPI.Enum;

namespace ATM_Project_InAsp.NetCoreWebAPI.Models
{
    public class BalanceModel
    {
        public Guid Id { get; set; }
        public decimal BalanceAmount { get; set; } // For simplicity, just using a balance amount

      
    }
}
