using ProductAPI_CouponAPI.Models;

namespace ProductAPI_CouponAPI.Data
{
    public class CustomerStore
    {
        public static List<CustomerAccount> customerList = new List<CustomerAccount>{
            
            new CustomerAccount {Id = 1, Name = "Rhea", MemebershipLevel = "Gold", Created = DateTime.Now, Updated = DateTime.Now, LastActive = DateTime.Now},
            new CustomerAccount {Id = 1, Name = "Ldog", MemebershipLevel = "Silver", Created = DateTime.Now, Updated = DateTime.Now, LastActive = DateTime.Now},
            new CustomerAccount {Id = 3, Name = "CoyCoy", MemebershipLevel = "Bronze", Created = DateTime.Now, Updated = DateTime.Now, LastActive = DateTime.Now}
            };
    }
}
