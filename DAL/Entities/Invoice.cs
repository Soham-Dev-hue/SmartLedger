using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartLedger.DAL.Entities;

[Table("invoices", Schema = "Finance")]
public partial class Invoice
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("org_id")]
    public Guid? OrgId { get; set; }

    [Column("vendor_id")]
    public Guid? VendorId { get; set; }

    [Column("invoice_number")]
    [StringLength(100)]
    public string? InvoiceNumber { get; set; }

    [Column("date_issued")]
    public DateOnly? DateIssued { get; set; }

    [Column("due_date")]
    public DateOnly? DueDate { get; set; }

    [Column("total_amount")]
    [Precision(12, 2)]
    public decimal? TotalAmount { get; set; }

    [Column("currency")]
    [StringLength(10)]
    public string? Currency { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    [Column("category")]
    [StringLength(100)]
    public string? Category { get; set; }

    [Column("ocr_text")]
    public string? OcrText { get; set; }

    [Column("ai_summary")]
    public string? AiSummary { get; set; }

    [Column("anomaly_score")]
    public double? AnomalyScore { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("OrgId")]
    [InverseProperty("Invoices")]
    public virtual Organization? Org { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("VendorId")]
    [InverseProperty("Invoices")]
    public virtual Vendor? Vendor { get; set; }
}
