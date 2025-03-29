using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class AlertStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public AlertStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddAlertAsync(Alert alert)
        {
            await _carParkSystemDbContext.Alerts.AddAsync(alert);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Alert> GetAlertByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Alerts.FindAsync(id);
        }

        public async Task<IEnumerable<Alert>> GetAllAlertsAsync()
        {
            return await _carParkSystemDbContext.Alerts.ToListAsync();
        }

        public async Task<List<Alert>> GetAllAlertsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Alert, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Alerts.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.DueDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateAlertAsync(Guid id, Alert newAlert)
        {
            var alert = await _carParkSystemDbContext.Alerts
                       .FirstOrDefaultAsync(a => a.AlertID == id);

            if (alert != null)
            {
                alert.VehicleID = newAlert.VehicleID;
                alert.DueDate = newAlert.DueDate;
                alert.AlertType = newAlert.AlertType;
                alert.Status = newAlert.Status;
                alert.IsRead = newAlert.IsRead;
        
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAlertAsync(Guid id)
        {
            var alert = await _carParkSystemDbContext.Alerts
                           .FirstOrDefaultAsync(a => a.AlertID == id);
            if (alert != null)
            {
                _carParkSystemDbContext.Alerts.Remove(alert);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}