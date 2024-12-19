
namespace ATM_Project_InAsp.NetCoreWebAPI.RTO
{
    public class UserResponseRto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public long? PhoneNumber { get; set; }
        public string? CreatedAtLocalTimeZone { get; set; }
        public string? CreatedAtUniversalTimeZone { get; set; }
        public string AdhaarCardUrl { get; set; }

        public string PanCardUrl { get; set; }

        public long AccountNo { get; set; }
        public long PinNo { get; set; }

    }
}
