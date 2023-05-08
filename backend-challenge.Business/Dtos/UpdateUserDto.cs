namespace backend_challenge.Business.Dtos
{
    [Serializable]
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
