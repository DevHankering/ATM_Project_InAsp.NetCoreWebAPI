
namespace ATM_Project_InAsp.NetCoreWebAPI.RTO
{
    public class UserResponseRto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public long? PhoneNumber { get; set; }
        public DateTime? CreatedAtLocalTimeZone { get; set; }
        public DateTime? CreatedAtUniversalTimeZone { get; set; }
        public string AdhaarCardUrl { get; set; }

        public string PanCardUrl { get; set; }
        public decimal BalanceAmount { get; set; }


        public string PinNo { get; set; }

        public string[] Roles { get; set; }

    }
}
