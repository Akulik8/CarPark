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
    class FuelRecordStorage
    {
        //private readonly CarParkSystemDbContext _carParkSystemDbContext;

        //public FuelRecordStorage(CarParkSystemDbContext carParkSystemDbContext)
        //{
        //    _carParkSystemDbContext = carParkSystemDbContext;
        //}

        //public async Task AddFuelRecordAsync(FuelRecord fuelRecord)
        //{
        //    await _carParkSystemDbContext.FuelRecords.AddAsync(fuelRecord);
        //    await _carParkSystemDbContext.SaveChangesAsync();
        //}

        //public async Task<FuelRecord> GetFuelRecordByIdAsync(Guid id)
        //{
        //    return await _carParkSystemDbContext.FuelRecords.FindAsync(id);
        //}

        //public async Task<IEnumerable<FuelRecord>> GetAllFuelRecordsAsync()
        //{
        //    return await _carParkSystemDbContext.FuelRecords.ToListAsync();
        //}

        //public async Task<List<FuelRecord>> GetAllFuelRecordsByFilterAsync(int pageSize, int pageNumber, Expression<Func<FuelRecord, bool>>? filter)
        //{
        //    var query = _carParkSystemDbContext.FuelRecords.AsQueryable();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    query = query
        //        .OrderBy(x => x.Date)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    return await query.ToListAsync();
        //}

        //public async Task UpdateFuelRecordAsync(Guid id, FuelRecord newFuelRecord)
        //{
        //    var fuelRecord = await _carParkSystemDbContext.FuelRecords
        //                 .FirstOrDefaultAsync(a => a.FuelRecordID == id);

        //    if (fuelRecord != null)
        //    {
        //        fuelRecord.VehicleID = newFuelRecord.VehicleID;
        //        fuelRecord.Date = newFuelRecord.Date;
        //        fuelRecord.FuelAmount = newFuelRecord.FuelAmount;
        //        fuelRecord.FuelPrice = newFuelRecord.FuelPrice;
        //        fuelRecord.FuelStation = newFuelRecord.FuelStation;

        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteFuelRecordAsync(Guid id)
        //{
        //    var fuelRecord = await _carParkSystemDbContext.FuelRecords
        //                .FirstOrDefaultAsync(a => a.FuelRecordID == id);
        //    if (fuelRecord != null)
        //    {
        //        _carParkSystemDbContext.FuelRecords.Remove(fuelRecord);
        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}
    }
}