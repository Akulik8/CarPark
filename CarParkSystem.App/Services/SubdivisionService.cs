using AutoMapper;
using CarParkSystem.App.DTOs;
using CarParkSystem.App.Interfaces;
using CarParkSystem.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Services
{
    public class SubdivisionService : ISubdivisionService
    {
        private readonly ISubdivisionStorage _storage;
        private readonly IMapper _mapper;
        private readonly ILogger<SubdivisionService> _logger;

        public SubdivisionService(ISubdivisionStorage storage, IMapper mapper, ILogger<SubdivisionService> logger)
        {
            _storage = storage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddSubdivisionAsync(CreateSubdivisionDto dto)
        {
            ValidateCreateSubdivision(dto);

            var model = _mapper.Map<Subdivision>(dto);
            await _storage.AddSubdivisionAsync(model);
        }

        public async Task<SubdivisionDto> GetSubdivisionByIdAsync(Guid id)
        {
            var result = await _storage.GetSubdivisionByIdAsync(id);
            if (result == null)
                throw new KeyNotFoundException("Подразделение с указанным ID не найдено.");

            return _mapper.Map<SubdivisionDto>(result);
        }

        public async Task<IEnumerable<SubdivisionDto>> GetAllSubdivisionsAsync()
        {
            var result = await _storage.GetAllSubdivisionsAsync();
            return _mapper.Map<IEnumerable<SubdivisionDto>>(result);
        }

        public async Task<IEnumerable<SubdivisionDto>> GetFilteredSubdivisionsAsync(string? name, string? address, string? phoneNumber, string? status)
        {
            var all = await _storage.GetAllSubdivisionsAsync();
            var query = all.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(x => x.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(address))
                query = query.Where(x => x.Address.Contains(address, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                query = query.Where(x => x.PhoneNumber.Contains(phoneNumber, StringComparison.OrdinalIgnoreCase));

            return _mapper.Map<IEnumerable<SubdivisionDto>>(query.ToList());
        }

        public async Task UpdateSubdivisionAsync(Guid id, SubdivisionDto dto)
        {

            var existing = await _storage.GetSubdivisionByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Подразделение с указанным ID не найдено.");
            ValidateUpdateSubdivision(dto);

            var model = _mapper.Map<Subdivision>(dto);
            await _storage.UpdateSubdivisionAsync(id, model);
        }

        public async Task DeleteSubdivisionAsync(Guid id)
        {
            var existing = await _storage.GetSubdivisionByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Подразделение с указанным ID не найдено.");
            await _storage.DeleteSubdivisionAsync(id);
        }

        private void ValidateCreateSubdivision(CreateSubdivisionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Название подразделения обязательно.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new ArgumentException("Адрес обязателен.");

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new ArgumentException("Телефон обязателен.");
        }

        private void ValidateUpdateSubdivision(SubdivisionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Название подразделения обязательно.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new ArgumentException("Адрес обязателен.");

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new ArgumentException("Телефон обязателен.");
        }
    }
}
