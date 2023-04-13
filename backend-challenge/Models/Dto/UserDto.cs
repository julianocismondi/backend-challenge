using System.Text.Json.Serialization;

namespace backend_challenge.Dto
{
    [Serializable]
    public class UserDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public int RoleId { get; set; } = 2;
    }
}
