namespace RiverTech_Testing_Task_V2.Models
{
    public class ApiAddressModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public ApiGeoModel Geo { get; set; }
    }
}
