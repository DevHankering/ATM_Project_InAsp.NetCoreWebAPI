namespace ATM_Project_InAsp.NetCoreWebAPI.DTO
{
    public class UserRequestDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public long? PhoneNumber { get; set; }
        public IFormFile? AdhaarCard { get; set; }
        public IFormFile? PanCard { get; set; }
    }
}
