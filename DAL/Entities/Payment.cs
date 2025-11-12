using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartLedger.DAL.Entities;

[Table("payments", Schema = "Finance")]
public partial class Payment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("invoice_id")]
    public Guid? InvoiceId { get; set; }

    [Column("amount")]
    [Precision(12, 2)]
    public decimal Amount { get; set; }

    [Column("payment_date")]
    public DateOnly PaymentDate { get; set; }

    [Column("method")]
    [StringLength(50)]
    public string? Method { get; set; }

    [Column("reference")]
    public string? Reference { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("Payments")]
    public virtual Invoice? Invoice { get; set; }
}
