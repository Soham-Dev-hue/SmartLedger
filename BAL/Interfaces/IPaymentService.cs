using SmartLedger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLedger.BAL.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> AddPaymentAsync(PaymentRequest request);
        Task<IEnumerable<PaymentResponse>> GetPaymentsByInvoiceAsync(Guid invoiceId);
        Task<IEnumerable<PaymentResponse>> GetPaymentsByOrgAsync(Guid orgId);
        Task<PaymentResponse?> GetPaymentByIdAsync(Guid id);
        Task<bool> UpdatePaymentAsync(Guid id, PaymentRequest request);
        Task<bool> DeletePaymentAsync(Guid id);
    }
}
