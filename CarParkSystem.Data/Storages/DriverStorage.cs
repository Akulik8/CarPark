using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class DriverStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public DriverStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddDrivertAsync(Driver driver)
        {
            await _carParkSystemDbContext.Drivers.AddAsync(driver);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Drivers.FindAsync(id);
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            return await _carParkSystemDbContext.Drivers.ToListAsync();
        }

        public async Task<List<Driver>> GetAllDriversByFilterAsync(int pageSize, int pageNumber, Expression<Func<Driver, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Drivers.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateDriverAsync(Guid id, Driver newDriver)
        {
            var driver = await _carParkSystemDbContext.Drivers
                       .FirstOrDefaultAsync(a => a.DriverID == id);

            if (driver != null)
            {
                driver.FirstName = newDriver.FirstName;
                driver.LastName = newDriver.LastName;
                driver.LicenseNumber = newDriver.LicenseNumber;
                driver.LicenseCategory = newDriver.LicenseCategory;
                driver.DateOfBirth = newDriver.DateOfBirth;
                driver.PhoneNumber = newDriver.PhoneNumber;
                driver.Address = newDriver.Address;
                driver.EmploymentDate = newDriver.EmploymentDate;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAlertAsync(Guid id)
        {
            var driver = await _carParkSystemDbContext.Drivers
                           .FirstOrDefaultAsync(a => a.DriverID == id);
            if (driver != null)
            {
                _carParkSystemDbContext.Drivers.Remove(driver);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}