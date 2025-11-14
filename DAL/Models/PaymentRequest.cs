using System;

namespace SmartLedger.DAL.Models
{
    public class PaymentRequest
    {
        public Guid InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public string? Method { get; set; }
        public string? Reference { get; set; }
    }
}
