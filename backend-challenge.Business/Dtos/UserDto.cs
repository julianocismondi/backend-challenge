using backend_challenge.Domain.Entities;
using System.Text.Json.Serialization;

namespace backend_challenge.Business.Dtos
{
    [Serializable]
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
