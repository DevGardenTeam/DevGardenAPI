using DatabaseEf.Entities.Enums;

namespace DatabaseEf.Entities
{
    public class UserService
    {
        public int UserId { get; set; }

        public ServiceName ServiceName { get; set; }

        public string AccessToken { get; set; }
    }
}