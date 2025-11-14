using SmartLedger.BAL.Interfaces;
using SmartLedger.BAL.Models;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLedger.BAL.Services
{
    public class CashflowService : ICashflowService
    {
        private readonly ICashflowPredictionRepository _predictionRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPaymentRepository _paymentRepo;

        public CashflowService(
            ICashflowPredictionRepository predictionRepo,
            IInvoiceRepository invoiceRepo,
            IPaymentRepository paymentRepo)
        {
            _predictionRepo = predictionRepo;
            _invoiceRepo = invoiceRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task<IEnumerable<CashflowPredictionDto>> GetPredictionsAsync(Guid orgId)
        {
            var list = await _predictionRepo.GetByOrgAsync(orgId);

            return list.Select(x => new CashflowPredictionDto
            {
                Id = x.Id,
                OrgId = x.OrgId ?? Guid.Empty,
                Month = x.Month,
                ProjectedInflow = x.ProjectedInflow ?? 0m,
                ProjectedOutflow = x.ProjectedOutflow ?? 0m,
                Confidence = x.Confidence ?? 0.0,
                GeneratedAt = x.GeneratedAt ?? DateTime.UtcNow
            });
        }

        public async Task<CashflowPredictionDto?> GetPredictionForMonthAsync(Guid orgId, DateOnly month)
        {
            var prediction = await _predictionRepo.GetForMonthAsync(orgId, month);

            if (prediction == null) return null;

            return new CashflowPredictionDto
            {
                Id = prediction.Id,
                OrgId = prediction.OrgId ?? Guid.Empty,
                Month = prediction.Month,
                ProjectedInflow = prediction.ProjectedInflow ?? 0m,
                ProjectedOutflow = prediction.ProjectedOutflow ?? 0m,
                Confidence = prediction.Confidence ?? 0.0,
                GeneratedAt = prediction.GeneratedAt ?? DateTime.UtcNow
            };
        }

        public async Task<CashflowPredictionDto> GeneratePredictionAsync(Guid orgId)
        {
            // Real ML will come later — for now use simple aggregation
            var invoices = (await _invoiceRepo.GetInvoicesByOrgAsync(orgId)).ToList();

            // safe handling if Payments collection is null on any invoice
            var payments = invoices
                .Where(i => i.Payments != null)
                .SelectMany(i => i.Payments ?? Enumerable.Empty<Payment>())
                .ToList();

            decimal inflow = payments.Sum(p => p.Amount);
            decimal outflow = invoices.Sum(i => i.TotalAmount ?? 0m);

            var month = DateOnly.FromDateTime(DateTime.UtcNow);

            var entity = new CashflowPrediction
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                Month = month,
                ProjectedInflow = inflow,
                ProjectedOutflow = outflow,
                Confidence = 0.78, // dummy confidence score
                GeneratedAt = DateTime.UtcNow
            };

            await _predictionRepo.AddAsync(entity);
            await _predictionRepo.SaveAsync();

            return new CashflowPredictionDto
            {
                Id = entity.Id,
                OrgId = orgId,
                Month = month,
                ProjectedInflow = inflow,
                ProjectedOutflow = outflow,
                Confidence = entity.Confidence ?? 0.0,
                GeneratedAt = entity.GeneratedAt ?? DateTime.UtcNow
            };
        }
    }
}
