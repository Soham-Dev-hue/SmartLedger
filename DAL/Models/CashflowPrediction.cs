using System;

namespace SmartLedger.BAL.Models
{
    public class CashflowPredictionDto
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public DateOnly Month { get; set; }
        public decimal ProjectedInflow { get; set; }
        public decimal ProjectedOutflow { get; set; }
        public double Confidence { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
