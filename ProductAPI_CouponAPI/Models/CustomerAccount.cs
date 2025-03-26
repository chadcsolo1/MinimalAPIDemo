namespace ProductAPI_CouponAPI.Models
{
    public class CustomerAccount
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MemebershipLevel { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime LastActive { get; set; }
    }
}
