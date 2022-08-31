using System.Xml.Serialization;

namespace MekashronTestTaskk.Models
{
    public class UserEntityResponse : BaseResponse
    {
        public int EntityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
