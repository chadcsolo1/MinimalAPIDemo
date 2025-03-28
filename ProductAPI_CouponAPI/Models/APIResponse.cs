using System.Net;

namespace ProductAPI_CouponAPI.Models
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; }
        public Object Result { get; set; } = null;
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; } = null;
    }
}
