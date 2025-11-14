using System;

namespace SmartLedger.DAL.Models
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public Guid? InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string? Method { get; set; }
        public string? Reference { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
