using CarParkSystem.App.Interfaces;
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
    public class BidStorage : IBidStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public BidStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddBidAsync(Bid bid)
        {
            await _carParkSystemDbContext.Bids.AddAsync(bid);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<Bid> GetBidByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Bids.FindAsync(id);
        }

        public async Task<IEnumerable<Bid>> GetAllBidsAsync()
        {
            return await _carParkSystemDbContext.Bids.ToListAsync();
        }

        public IQueryable<Bid> Query() => _carParkSystemDbContext.Bids.AsQueryable();

        public async Task<List<Bid>> GetAllBidsByFilterAsync(Expression<Func<Bid, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Bids.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.DoDate);

            return await query.ToListAsync();
        }

        public async Task UpdateBidAsync(Guid id, Bid newBid)
        {
            var bid = await _carParkSystemDbContext.Bids
                       .FirstOrDefaultAsync(a => a.BidID == id);

            if (bid != null)
            {
                bid.DeliveryDate = newBid.DeliveryDate;
                bid.DoDate = newBid.DoDate;
                bid.Cargo = newBid.Cargo;
                bid.Weight = newBid.Weight;
                bid.Volume = newBid.Volume;
                bid.From = newBid.From;
                bid.To = newBid.To;
                bid.Note = newBid.Note;
                bid.Status = newBid.Status;
                bid.SubdivisionID = newBid.SubdivisionID;
                bid.UserID = newBid.UserID;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBidAsync(Guid id)
        {
            var bid = await _carParkSystemDbContext.Bids
                      .FirstOrDefaultAsync(a => a.BidID == id);
            if (bid != null)
            {
                _carParkSystemDbContext.Bids.Remove(bid);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}