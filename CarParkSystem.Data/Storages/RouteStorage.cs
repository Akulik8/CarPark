using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    class RouteStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public RouteStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddRouteAsync(Route route)
        {
            await _carParkSystemDbContext.Routes.AddAsync(route);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Route> GetRouteByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Routes.FindAsync(id);
        }

        public async Task<IEnumerable<Route>> GetAllRoutesAsync()
        {
            return await _carParkSystemDbContext.Routes.ToListAsync();
        }

        public async Task<List<Route>> GetAllRoutesByFilterAsync(int pageSize, int pageNumber, Expression<Func<Route, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Routes.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.RouteID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateRouteAsync(Guid id, Route newRoute)
        {
            var route = await _carParkSystemDbContext.Routes
                       .FirstOrDefaultAsync(a => a.RouteID == id);

            if (route != null)
            {
                route.TripID = newRoute.TripID;
                route.StartPoint = newRoute.StartPoint;
                route.EndPoint = newRoute.EndPoint;
                route.Distance = newRoute.Distance;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteRouteAsync(Guid id)
        {
            var route = await _carParkSystemDbContext.Routes
                     .FirstOrDefaultAsync(a => a.RouteID == id);
            if (route != null)
            {
                _carParkSystemDbContext.Routes.Remove(route);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}