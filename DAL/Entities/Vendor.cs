using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartLedger.DAL.Entities;

[Table("vendors", Schema = "Finance")]
public partial class Vendor
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("org_id")]
    public Guid? OrgId { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(150)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("category")]
    [StringLength(100)]
    public string? Category { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Vendor")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("OrgId")]
    [InverseProperty("Vendors")]
    public virtual Organization? Org { get; set; }
}
