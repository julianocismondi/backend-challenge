using System.Text.Json.Serialization;

namespace backend_challenge.Dto
{
    [Serializable]
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        
        public int RoleId { get; set; }
    }
}
