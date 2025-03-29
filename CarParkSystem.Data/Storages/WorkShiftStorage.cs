using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class WorkShiftStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public WorkShiftStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddWorkShiftAsync(WorkShift workShift)
        {
            await _carParkSystemDbContext.WorkShifts.AddAsync(workShift);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<WorkShift> GetWorkShiftByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.WorkShifts.FindAsync(id);
        }

        public async Task<IEnumerable<WorkShift>> GetAllWorkShiftsAsync()
        {
            return await _carParkSystemDbContext.WorkShifts.ToListAsync();
        }

        public async Task<List<WorkShift>> GetAllWorkShiftsByFilterAsync(int pageSize, int pageNumber, Expression<Func<WorkShift, bool>>? filter)
        {
            var query = _carParkSystemDbContext.WorkShifts.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.StartTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateWorkShiftAsync(Guid id, WorkShift newWorkShift)
        {
            var workShift = await _carParkSystemDbContext.WorkShifts
                       .FirstOrDefaultAsync(a => a.ShiftID == id);

            if (workShift != null)
            {
                workShift.DriverID = newWorkShift.DriverID;
                workShift.VehicleID = newWorkShift.VehicleID;
                workShift.StartTime = newWorkShift.StartTime;
                workShift.EndTime = newWorkShift.EndTime;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteWorkShiftAsync(Guid id)
        {
            var workShift = await _carParkSystemDbContext.WorkShifts
                      .FirstOrDefaultAsync(a => a.ShiftID == id);
            if (workShift != null)
            {
                _carParkSystemDbContext.WorkShifts.Remove(workShift);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}