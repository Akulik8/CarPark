using CarParkSystem.App.Interfaces;
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
    public class SubdivisionStorage : ISubdivisionStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public SubdivisionStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddSubdivisionAsync(Subdivision subdivision)
        {
            await _carParkSystemDbContext.Subdivisions.AddAsync(subdivision);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Subdivision> GetSubdivisionByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Subdivisions.FindAsync(id);
        }

        public async Task<IEnumerable<Subdivision>> GetAllSubdivisionsAsync()
        {
            return await _carParkSystemDbContext.Subdivisions.ToListAsync();
        }

        public async Task<List<Subdivision>> GetAllSubdivisionsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Subdivision, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Subdivisions.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateSubdivisionAsync(Guid id, Subdivision newSubdivision)
        {
            var subdivision = await _carParkSystemDbContext.Subdivisions
                       .FirstOrDefaultAsync(a => a.SubdivisionID == id);

            if (subdivision != null)
            {
                subdivision.Name = newSubdivision.Name;
                subdivision.Address = newSubdivision.Address;
                subdivision.PhoneNumber = newSubdivision.PhoneNumber;
                subdivision.Status = newSubdivision.Status;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteSubdivisionAsync(Guid id)
        {
            var subdivision = await _carParkSystemDbContext.Subdivisions
                         .FirstOrDefaultAsync(a => a.SubdivisionID == id);
            if (subdivision != null)
            {
                _carParkSystemDbContext.Subdivisions.Remove(subdivision);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}