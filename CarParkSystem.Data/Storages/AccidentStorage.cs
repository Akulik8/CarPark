using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class AccidentStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public AccidentStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddAccidentAsync(Accident accident)
        {
            await _carParkSystemDbContext.Accidents.AddAsync(accident);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Accident> GetAccidentByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Accidents.FindAsync(id);
        }

        public async Task<IEnumerable<Accident>> GetAllAccidentsAsync()
        {
            return await _carParkSystemDbContext.Accidents.ToListAsync();
        }

        public async Task<List<Accident>> GetAllAccidentsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Accident, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Accidents.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.AccidentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateAccidentAsync(Guid id, Accident newAccident)
        {
            var accident = await _carParkSystemDbContext.Accidents
                        .FirstOrDefaultAsync(a => a.AccidentID == id);
            if (accident != null)
            {
                accident.VehicleID = newAccident.VehicleID;
                accident.DriverID = newAccident.DriverID;
                accident.AccidentDate = newAccident.AccidentDate;
                accident.Location = newAccident.Location;
                accident.AccidentDetails = newAccident.AccidentDetails;
                accident.DamageCost = newAccident.DamageCost;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAccidentAsync(Guid id)
        {
            var accident = await _carParkSystemDbContext.Accidents
                          .FirstOrDefaultAsync(a => a.AccidentID == id);
            if (accident != null)
            {
                _carParkSystemDbContext.Accidents.Remove(accident);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}
