using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ATM_Project_InAsp.NetCoreWebAPI.Enum;

namespace ATM_Project_InAsp.NetCoreWebAPI.DTO
{
    public class BalanceDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal BalanceAmount { get; set; }
        public CDEnum CD { get; set; }
    }
}
