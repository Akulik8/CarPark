using CarParkSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Interfaces
{
    public interface IBidStorage
    {
        public Task AddBidAsync(Bid bid);
        public Task<Bid> GetBidByIdAsync(Guid id);
        public Task<IEnumerable<Bid>> GetAllBidsAsync();
        public Task<List<Bid>> GetAllBidsByFilterAsync(Expression<Func<Bid, bool>>? filter);
        public Task UpdateBidAsync(Guid id, Bid newBid);
        public Task DeleteBidAsync(Guid id);
        public IQueryable<Bid> Query();
    }
}
