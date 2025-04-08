using CarParkSystem.App.DTOs;
using CarParkSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Interfaces
{
    public interface IBidService
    {
        public Task AddBidAsync(CreateBidDto dto);
        public Task<BidDto> GetBidByIdAsync(Guid id);
        public Task<IEnumerable<BidDto>> GetAllBidsAsync();
        public Task<IEnumerable<BidDto>> GetFilteredBidsAsync(Guid? userId, string? status, string? cargo, string? from, string? to, DateTime? startDate, DateTime? endDate);
        public Task UpdateBidAsync(Guid id, BidDto dto);
        public Task DeleteBidAsync(Guid id);
        public Task<List<BidDto>> GetAllBidsByFilterAsync(Expression<Func<BidDto, bool>>? filter);
    }
}
