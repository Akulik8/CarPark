using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class TripStorage
    {
        //private readonly CarParkSystemDbContext _carParkSystemDbContext;

        //public TripStorage(CarParkSystemDbContext carParkSystemDbContext)
        //{
        //    _carParkSystemDbContext = carParkSystemDbContext;
        //}

        //public async Task AddTripAsync(Trip trip)
        //{
        //    await _carParkSystemDbContext.Trips.AddAsync(trip);
        //    await _carParkSystemDbContext.SaveChangesAsync();
        //}

        //public async Task<Trip> GetTripByIdAsync(Guid id)
        //{
        //    return await _carParkSystemDbContext.Trips.FindAsync(id);
        //}

        //public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        //{
        //    return await _carParkSystemDbContext.Trips.ToListAsync();
        //}

        //public async Task<List<Trip>> GetAllTripsByFilterAsync(int pageSize, int pageNumber, Expression<Func<Trip, bool>>? filter)
        //{
        //    var query = _carParkSystemDbContext.Trips.AsQueryable();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    query = query
        //        .OrderBy(x => x.StartTime)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    return await query.ToListAsync();
        //}

        //public async Task UpdateTripAsync(Guid id, Trip newTrip)
        //{
        //    var trip = await _carParkSystemDbContext.Trips
        //               .FirstOrDefaultAsync(a => a.TripID == id);

        //    if (trip != null)
        //    {
        //        trip.VehicleID = newTrip.VehicleID;
        //        trip.DriverID = newTrip.DriverID;
        //        trip.StartTime = newTrip.StartTime;
        //        trip.EndTime = newTrip.EndTime;
        //        trip.MileageAtStart = newTrip.MileageAtStart;
        //        trip.MileageAtEnd = newTrip.MileageAtEnd;
        //        trip.FuelUsed = newTrip.FuelUsed;

        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteTripAsync(Guid id)
        //{
        //    var trip = await _carParkSystemDbContext.Trips
        //                       .FirstOrDefaultAsync(a => a.TripID == id);
        //    if (trip != null)
        //    {
        //        _carParkSystemDbContext.Trips.Remove(trip);
        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}
    }
}