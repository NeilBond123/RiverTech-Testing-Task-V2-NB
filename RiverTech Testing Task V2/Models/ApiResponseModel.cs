namespace RiverTech_Testing_Task_V2.Models
{
    public class ApiResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ApiAddressModel Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public ApiCompanyModel Company { get; set; }
    }
}
