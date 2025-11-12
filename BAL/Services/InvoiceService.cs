using SmartLedger.BAL.Interfaces;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;
using SmartLedger.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLedger.BAL.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<Invoice>> GetOrgInvoicesAsync(Guid orgId)
            => await _invoiceRepository.GetInvoicesByOrgAsync(orgId);

        public async Task<Invoice?> GetInvoiceByIdAsync(Guid id)
            => await _invoiceRepository.GetByIdAsync(id);

        public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            invoice.Id = Guid.NewGuid();
            invoice.CreatedAt = DateTime.UtcNow;

            await _invoiceRepository.AddAsync(invoice);
            await _invoiceRepository.SaveAsync();
            return invoice;
        }

        public async Task<Invoice?> UpdateInvoiceAsync(Invoice invoice)
        {
            var existing = await _invoiceRepository.GetByIdAsync(invoice.Id);
            if (existing == null) return null;

            existing.Status = invoice.Status;
            existing.Category = invoice.Category;
            existing.AiSummary = invoice.AiSummary;
            existing.AnomalyScore = invoice.AnomalyScore;
            

            _invoiceRepository.Update(existing);
            await _invoiceRepository.SaveAsync();
            return existing;
        }

        public async Task DeleteInvoiceAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice != null)
            {
                _invoiceRepository.Remove(invoice);
                await _invoiceRepository.SaveAsync();
            }
        }

    }
}
