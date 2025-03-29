using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Data.Storages
{
    class DocumentStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public DocumentStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddDocumentAsync(Document document)
        {
            await _carParkSystemDbContext.Documents.AddAsync(document);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Documents.FindAsync(id);
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _carParkSystemDbContext.Documents.ToListAsync();
        }

        public async Task<List<Document>> GetAllDocumentsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Document, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Documents.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.ExpiryDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateDocumentAsync(Guid id, Document newDocument)
        {
            var document = await _carParkSystemDbContext.Documents
                       .FirstOrDefaultAsync(a => a.DocumentID == id);

            if (document != null)
            {
                document.VehicleID = newDocument.VehicleID;
                document.DocumentType = newDocument.DocumentType;
                document.IssueDate = newDocument.IssueDate;
                document.ExpiryDate = newDocument.ExpiryDate;
                document.FilePath = newDocument.FilePath;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            var document = await _carParkSystemDbContext.Documents
                           .FirstOrDefaultAsync(a => a.DocumentID == id);
            if (document != null)
            {
                _carParkSystemDbContext.Documents.Remove(document);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}