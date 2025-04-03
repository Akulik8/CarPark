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
    class VehicleStorage
    {
        //private readonly CarParkSystemDbContext _carParkSystemDbContext;

        //public VehicleStorage(CarParkSystemDbContext carParkSystemDbContext)
        //{
        //    _carParkSystemDbContext = carParkSystemDbContext;
        //}

        //public async Task AddVehicleAsync(Vehicle vehicle)
        //{
        //    await _carParkSystemDbContext.Vehicles.AddAsync(vehicle);
        //    await _carParkSystemDbContext.SaveChangesAsync();
        //}

        //public async Task<Vehicle> GetVehicleByIdAsync(Guid id)
        //{
        //    return await _carParkSystemDbContext.Vehicles.FindAsync(id);
        //}

        //public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        //{
        //    return await _carParkSystemDbContext.Vehicles.ToListAsync();
        //}

        //public async Task<List<Vehicle>> GetAllVehiclesByFilterAsync(int pageSize, int pageNumber, Expression<Func<Vehicle, bool>>? filter)
        //{
        //    var query = _carParkSystemDbContext.Vehicles.AsQueryable();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    query = query
        //        .OrderBy(x => x.Status)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    return await query.ToListAsync();
        //}

        //public async Task UpdateVehicleAsync(Guid id, Vehicle newVehicle)
        //{
        //    var vehicle = await _carParkSystemDbContext.Vehicles
        //               .FirstOrDefaultAsync(a => a.VehicleID == id);

        //    if (vehicle != null)
        //    {
        //        vehicle.VehicleType = newVehicle.VehicleType;
        //        vehicle.VehicleCategory = newVehicle.VehicleCategory;
        //        vehicle.LicensePlate = newVehicle.LicensePlate;
        //        vehicle.Make = newVehicle.Make;
        //        vehicle.Model = newVehicle.Model;
        //        vehicle.Year = newVehicle.Year;
        //        vehicle.Mass = newVehicle.Mass;
        //        vehicle.MaxMass = newVehicle.MaxMass;
        //        vehicle.Capacity = newVehicle.Capacity;
        //        vehicle.Color = newVehicle.Color;
        //        vehicle.NumberOfSeats = newVehicle.NumberOfSeats;
        //        vehicle.FuelConsumption = newVehicle.FuelConsumption;
        //        vehicle.VIN = newVehicle.VIN;
        //        vehicle.Mileage = newVehicle.Mileage;
        //        vehicle.FuelType = newVehicle.FuelType;
        //        vehicle.Status = newVehicle.Status;

        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteVehicleAsync(Guid id)
        //{
        //    var vehicle = await _carParkSystemDbContext.Vehicles
        //                   .FirstOrDefaultAsync(a => a.VehicleID == id);
        //    if (vehicle != null)
        //    {
        //        _carParkSystemDbContext.Vehicles.Remove(vehicle);
        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}
    }
}
