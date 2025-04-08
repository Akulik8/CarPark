using CarParkSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Interfaces
{
    public interface ISubdivisionStorage
    {
        public Task AddSubdivisionAsync(Subdivision subdivision);
        public Task<Subdivision> GetSubdivisionByIdAsync(Guid id);
        public Task<IEnumerable<Subdivision>> GetAllSubdivisionsAsync();
        public Task<List<Subdivision>> GetAllSubdivisionsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Subdivision, bool>>? filter);
        public Task UpdateSubdivisionAsync(Guid id, Subdivision newSubdivision);
        public Task DeleteSubdivisionAsync(Guid id);
    }
}
