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
    class MaintenanceStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public MaintenanceStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddMaintenanceAsync(Maintenance maintenance)
        {
            await _carParkSystemDbContext.MaintenanceRecords.AddAsync(maintenance);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Maintenance> GetMaintenanceByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.MaintenanceRecords.FindAsync(id);
        }

        public async Task<IEnumerable<Maintenance>> GetAllMaintenancesAsync()
        {
            return await _carParkSystemDbContext.MaintenanceRecords.ToListAsync();
        }

        public async Task<List<Maintenance>> GetAllMaintenancesByFilterAsync(int pageSize, int pageNumber, Expression<Func<Maintenance, bool>>? filter)
        {
            var query = _carParkSystemDbContext.MaintenanceRecords.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.ServiceDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateMaintenanceAsync(Guid id, Maintenance newMaintenance)
        {
            var maintenance = await _carParkSystemDbContext.MaintenanceRecords
                       .FirstOrDefaultAsync(a => a.MaintenanceID == id);

            if (maintenance != null)
            {
                maintenance.VehicleID = newMaintenance.VehicleID;
                maintenance.ServiceDate = newMaintenance.ServiceDate;
                maintenance.ServiceType = newMaintenance.ServiceType;
                maintenance.Cost = newMaintenance.Cost;
                maintenance.ServiceCenter = newMaintenance.ServiceCenter;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteMaintenanceAsync(Guid id)
        {
            var maintenance = await _carParkSystemDbContext.MaintenanceRecords
                           .FirstOrDefaultAsync(a => a.MaintenanceID == id);
            if (maintenance != null)
            {
                _carParkSystemDbContext.MaintenanceRecords.Remove(maintenance);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}