using SmartLedger.BAL.Interfaces;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;
using SmartLedger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLedger.BAL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IRepository<Invoice> _invoiceRepo;

        public PaymentService(IPaymentRepository paymentRepo, IRepository<Invoice> invoiceRepo)
        {
            _paymentRepo = paymentRepo;
            _invoiceRepo = invoiceRepo;
        }

        public async Task<PaymentResponse> AddPaymentAsync(PaymentRequest request)
        {
            // Basic validation: invoice must exist
            var invoice = await _invoiceRepo.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
                throw new KeyNotFoundException("Invoice not found.");

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                InvoiceId = request.InvoiceId,
                Amount = request.Amount,
                PaymentDate = request.PaymentDate,
                Method = request.Method,
                Reference = request.Reference,
                CreatedAt = DateTime.UtcNow
            };

            await _paymentRepo.AddAsync(payment);
            await _paymentRepo.SaveAsync();

            return MapToResponse(payment);
        }

        public async Task<IEnumerable<PaymentResponse>> GetPaymentsByInvoiceAsync(Guid invoiceId)
        {
            var list = await _paymentRepo.GetPaymentsByInvoiceAsync(invoiceId);
            return list.Select(MapToResponse);
        }

        public async Task<IEnumerable<PaymentResponse>> GetPaymentsByOrgAsync(Guid orgId)
        {
            var list = await _paymentRepo.GetPaymentsByOrgAsync(orgId);
            return list.Select(MapToResponse);
        }

        public async Task<PaymentResponse?> GetPaymentByIdAsync(Guid id)
        {
            var p = await _paymentRepo.GetByIdAsync(id);
            if (p == null) return null;
            return MapToResponse(p);
        }

        public async Task<bool> UpdatePaymentAsync(Guid id, PaymentRequest request)
        {
            var existing = await _paymentRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Amount = request.Amount;
            existing.PaymentDate = request.PaymentDate;
            existing.Method = request.Method;
            existing.Reference = request.Reference;

            _paymentRepo.Update(existing);
            await _paymentRepo.SaveAsync();
            return true;
        }

        public async Task<bool> DeletePaymentAsync(Guid id)
        {
            var p = await _paymentRepo.GetByIdAsync(id);
            if (p == null) return false;

            _paymentRepo.Remove(p);
            await _paymentRepo.SaveAsync();
            return true;
        }

        private PaymentResponse MapToResponse(Payment p) =>
            new PaymentResponse
            {
                Id = p.Id,
                InvoiceId = p.InvoiceId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Method = p.Method,
                Reference = p.Reference,
                CreatedAt = p.CreatedAt
            };
    }
}
