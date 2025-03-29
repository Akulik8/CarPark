using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace CarParkSystem.Data.Storages
{
    class InsuranceStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public InsuranceStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddInsuranceAsync(Insurance insurance)
        {
            await _carParkSystemDbContext.Insurances.AddAsync(insurance);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Insurance> GetInsuranceByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Insurances.FindAsync(id);
        }

        public async Task<IEnumerable<Insurance>> GetAllInsurancesAsync()
        {
            return await _carParkSystemDbContext.Insurances.ToListAsync();
        }

        public async Task<List<Insurance>> GetAllInsurancesByFilterAsync(int pageSize, int pageNumber, Expression<Func<Insurance, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Insurances.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.EndDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateInsuranceAsync(Guid id, Insurance newInsurance)
        {
            var insurance = await _carParkSystemDbContext.Insurances
                       .FirstOrDefaultAsync(a => a.InsuranceID == id);

            if (insurance != null)
            {
                insurance.VehicleID = newInsurance.VehicleID;
                insurance.InsuranceCompany = newInsurance.InsuranceCompany;
                insurance.PolicyNumber = newInsurance.PolicyNumber;
                insurance.StartDate= newInsurance.StartDate;
                insurance.EndDate = newInsurance.EndDate;
                insurance.InsuranceCost = newInsurance.InsuranceCost;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteInsuranceAsync(Guid id)
        {
            var insurance = await _carParkSystemDbContext.Insurances
                          .FirstOrDefaultAsync(a => a.InsuranceID == id);
            if (insurance != null)
            {
                _carParkSystemDbContext.Insurances.Remove(insurance);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}