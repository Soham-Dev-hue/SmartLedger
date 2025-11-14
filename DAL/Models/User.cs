namespace SmartLedger.BAL.Models.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public Guid? OrgId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class UpdateUserRoleDto
    {
        public string Role { get; set; } = null!;
    }
}
