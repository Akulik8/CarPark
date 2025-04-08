using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarParkSystem.App.DTOs;
using CarParkSystem.App.Interfaces;
using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Services
{
    public class BidService : IBidService
    {
        private readonly IBidStorage _bidStorage;
        private readonly IMapper _mapper;
        private readonly ILogger<BidService> _logger;

        public BidService(IBidStorage bidStorage, IMapper mapper, ILogger<BidService> logger)
        {
            _bidStorage = bidStorage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddBidAsync(CreateBidDto dto)
        {
            ValidateCreateBid(dto);

            var bid = _mapper.Map<Bid>(dto);
            await _bidStorage.AddBidAsync(bid);
        }

        public async Task<BidDto> GetBidByIdAsync(Guid id)
        {
            var bid = await _bidStorage.GetBidByIdAsync(id);
            if (bid == null)
                throw new KeyNotFoundException("Заявка с указанным ID не найдена.");

            return _mapper.Map<BidDto>(bid);
        }

        public async Task<IEnumerable<BidDto>> GetAllBidsAsync()
        {
            var bids = await _bidStorage.GetAllBidsAsync();
            return _mapper.Map<IEnumerable<BidDto>>(bids);
        }

        public async Task<IEnumerable<BidDto>> GetFilteredBidsAsync(Guid? userId, string? status, string? cargo, string? from, string? to, DateTime? startDate, DateTime? endDate)
        {
            var all = await _bidStorage.GetAllBidsAsync();
            var query = all.AsQueryable();

            if (userId.HasValue && userId.Value != Guid.Empty)
                query = query.Where(x => x.UserID == userId);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(x => x.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(cargo))
                query = query.Where(x => x.Cargo.Contains(cargo, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(from))
                query = query.Where(x => x.From.Contains(from, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(to))
                query = query.Where(x => x.To.Contains(to, StringComparison.OrdinalIgnoreCase));

            if (startDate.HasValue)
                query = query.Where(x => x.DeliveryDate.Date >= startDate.Value.Date);

            if (endDate.HasValue)
                query = query.Where(x => x.DeliveryDate.Date <= endDate.Value.Date);

            return _mapper.Map<IEnumerable<BidDto>>(query.ToList());
        }

        public async Task<List<BidDto>> GetAllBidsByFilterAsync(Expression<Func<BidDto, bool>>? filter)
        {
            var query = _bidStorage.Query().ProjectTo<BidDto>(_mapper.ConfigurationProvider);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateBidAsync(Guid id, BidDto dto)
        {
            var existing = await _bidStorage.GetBidByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Заявка с указанным ID не найдена.");
            
            ValidateUpdateBid(dto);

            var bid = _mapper.Map<Bid>(dto);
            await _bidStorage.UpdateBidAsync(id, bid);
        }

        public async Task DeleteBidAsync(Guid id)
        {
            var existing = await _bidStorage.GetBidByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Заявка с указанным ID не найдена.");

            await _bidStorage.DeleteBidAsync(id);
        }

        private void ValidateCreateBid(CreateBidDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Cargo))
                throw new ArgumentException("Поле 'Cargo' обязательно.");

            if (string.IsNullOrWhiteSpace(dto.From))
                throw new ArgumentException("Поле 'From' обязательно.");

            if (string.IsNullOrWhiteSpace(dto.To))
                throw new ArgumentException("Поле 'To' обязательно.");

            if (dto.Weight <= 0)
                throw new ArgumentException("Вес должен быть больше 0.");

            if (dto.Volume <= 0)
                throw new ArgumentException("Объём должен быть больше 0.");

            if (dto.DeliveryDate > dto.DoDate)
                throw new ArgumentException("Дата доставки не может быть раньше даты подачи.");

            if (dto.SubdivisionID == Guid.Empty)
                throw new ArgumentException("Не указано подразделение.");

            if (dto.UserID == Guid.Empty)
                throw new ArgumentException("Не указан пользователь.");
        }

        private void ValidateUpdateBid(BidDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Cargo))
                throw new ArgumentException("Поле 'Cargo' обязательно.");

            if (string.IsNullOrWhiteSpace(dto.From))
                throw new ArgumentException("Поле 'From' обязательно.");

            if (string.IsNullOrWhiteSpace(dto.To))
                throw new ArgumentException("Поле 'To' обязательно.");

            if (dto.Weight <= 0)
                throw new ArgumentException("Вес должен быть больше 0.");

            if (dto.Volume <= 0)
                throw new ArgumentException("Объём должен быть больше 0.");

            if (dto.DeliveryDate > dto.DoDate)
                throw new ArgumentException("Дата доставки не может быть раньше даты подачи.");

            if (dto.SubdivisionID == Guid.Empty)
                throw new ArgumentException("Не указано подразделение.");

            if (dto.UserID == Guid.Empty)
                throw new ArgumentException("Не указан пользователь.");
        }
    }
}