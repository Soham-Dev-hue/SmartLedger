using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartLedger.DAL.Entities;

[Table("organizations", Schema = "Org")]
public partial class Organization
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("industry")]
    [StringLength(100)]
    public string? Industry { get; set; }

    [Column("gst_number")]
    [StringLength(50)]
    public string? GstNumber { get; set; }

    [Column("country")]
    [StringLength(100)]
    public string? Country { get; set; }

    [Column("plan")]
    [StringLength(50)]
    public string? Plan { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Org")]
    public virtual ICollection<CashflowPrediction> CashflowPredictions { get; set; } = new List<CashflowPrediction>();

    [InverseProperty("Org")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Org")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    [InverseProperty("Org")]
    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
