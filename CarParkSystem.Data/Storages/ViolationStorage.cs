using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class ViolationStorage
    {
        //private readonly CarParkSystemDbContext _carParkSystemDbContext;

        //public ViolationStorage(CarParkSystemDbContext carParkSystemDbContext)
        //{
        //    _carParkSystemDbContext = carParkSystemDbContext;
        //}

        //public async Task AddViolationAsync(Violation violation)
        //{
        //    await _carParkSystemDbContext.Violations.AddAsync(violation);
        //    await _carParkSystemDbContext.SaveChangesAsync();
        //}

        //public async Task<Violation> GetViolationByIdAsync(Guid id)
        //{
        //    return await _carParkSystemDbContext.Violations.FindAsync(id);
        //}

        //public async Task<IEnumerable<Violation>> GetAllViolationsAsync()
        //{
        //    return await _carParkSystemDbContext.Violations.ToListAsync();
        //}

        //public async Task<List<Violation>> GetAllViolationsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Violation, bool>>? filter)
        //{
        //    var query = _carParkSystemDbContext.Violations.AsQueryable();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    query = query
        //        .OrderBy(x => x.ViolationDate)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    return await query.ToListAsync();
        //}

        //public async Task UpdateViolationAsync(Guid id, Violation newViolation)
        //{
        //    var violation = await _carParkSystemDbContext.Violations
        //               .FirstOrDefaultAsync(a => a.ViolationID == id);

        //    if (violation != null)
        //    {
        //        violation.DriverID = newViolation.DriverID;
        //        violation.VehicleID = newViolation.VehicleID;
        //        violation.ViolationDate = newViolation.ViolationDate;
        //        violation.ViolationType = newViolation.ViolationType;
        //        violation.FineAmount = newViolation.FineAmount;
        //        violation.Paid = newViolation.Paid;

        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteViolationAsync(Guid id)
        //{
        //    var violation = await _carParkSystemDbContext.Violations
        //                 .FirstOrDefaultAsync(a => a.ViolationID == id);
        //    if (violation != null)
        //    {
        //        _carParkSystemDbContext.Violations.Remove(violation);
        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}
    }
}