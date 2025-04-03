using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class RepairStorage
    {
        //private readonly CarParkSystemDbContext _carParkSystemDbContext;

        //public RepairStorage(CarParkSystemDbContext carParkSystemDbContext)
        //{
        //    _carParkSystemDbContext = carParkSystemDbContext;
        //}

        //public async Task AddRepairAsync(Repair repair)
        //{
        //    await _carParkSystemDbContext.Repairs.AddAsync(repair);
        //    await _carParkSystemDbContext.SaveChangesAsync();
        //}

        //public async Task<Repair> GetRepairByIdAsync(Guid id)
        //{
        //    return await _carParkSystemDbContext.Repairs.FindAsync(id);
        //}

        //public async Task<IEnumerable<Repair>> GetAllRepairsAsync()
        //{
        //    return await _carParkSystemDbContext.Repairs.ToListAsync();
        //}

        //public async Task<List<Repair>> GetAllRepairsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Repair, bool>>? filter)
        //{
        //    var query = _carParkSystemDbContext.Repairs.AsQueryable();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    query = query
        //        .OrderBy(x => x.RepairDate)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    return await query.ToListAsync();
        //}

        //public async Task UpdateRepairAsync(Guid id, Repair newRepair)
        //{
        //    var repair = await _carParkSystemDbContext.Repairs
        //               .FirstOrDefaultAsync(a => a.RepairID == id);

        //    if (repair != null)
        //    {
        //        repair.VehicleID = newRepair.VehicleID;
        //        repair.RepairDate = newRepair.RepairDate;
        //        repair.ProblemDescription = newRepair.ProblemDescription;
        //        repair.RepairDetails = newRepair.RepairDetails;
        //        repair.RepairCost = newRepair.RepairCost;
        //        repair.RepairCenter = newRepair.RepairCenter;

        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteRepairAsync(Guid id)
        //{
        //    var repair = await _carParkSystemDbContext.Repairs
        //                .FirstOrDefaultAsync(a => a.RepairID == id);
        //    if (repair != null)
        //    {
        //        _carParkSystemDbContext.Repairs.Remove(repair);
        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}
    }
}