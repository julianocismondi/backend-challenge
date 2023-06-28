using backend_challenge.Domain.Entities;

namespace backend_challenge.Business.Dtos
{
    public class AuthDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}
