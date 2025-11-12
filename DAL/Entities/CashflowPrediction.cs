using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartLedger.DAL.Entities;

[Table("cashflow_predictions", Schema = "Finance")]
public partial class CashflowPrediction
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("org_id")]
    public Guid? OrgId { get; set; }

    [Column("month")]
    public DateOnly Month { get; set; }

    [Column("projected_inflow")]
    [Precision(12, 2)]
    public decimal? ProjectedInflow { get; set; }

    [Column("projected_outflow")]
    [Precision(12, 2)]
    public decimal? ProjectedOutflow { get; set; }

    [Column("confidence")]
    public double? Confidence { get; set; }

    [Column("generated_at")]
    public DateTime? GeneratedAt { get; set; }

    [ForeignKey("OrgId")]
    [InverseProperty("CashflowPredictions")]
    public virtual Organization? Org { get; set; }
}
