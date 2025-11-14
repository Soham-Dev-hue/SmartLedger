using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartLedger.DAL.Entities
{
    [Table("cashflow_predictions", Schema = "Finance")]
    public partial class CashflowPrediction
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("org_id")]
        public Guid? OrgId { get; set; }

        // store as SQL date
        [Column("month", TypeName = "date")]
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
        public virtual Organization? Org { get; set; }
    }
}
