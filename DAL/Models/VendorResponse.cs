using System;

namespace SmartLedger.DAL.Models
{
    public class VendorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Category { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
