namespace SmartLedger.BAL.Models.Organization
{
    public class OrganizationCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Industry { get; set; }
        public string? GstNumber { get; set; }
        public string? Country { get; set; }
        public string? Plan { get; set; } = "Free";
    }

  
        public class OrganizationDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public string? Industry { get; set; }
            public string? GstNumber { get; set; }
            public string? Country { get; set; }
            public string? Plan { get; set; }
            public DateTime? CreatedAt { get; set; }
        }
    

}
