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
    public interface ISubdivisionService
    {
        public Task AddSubdivisionAsync(CreateSubdivisionDto dto);
        public Task<SubdivisionDto> GetSubdivisionByIdAsync(Guid id);
        public Task<IEnumerable<SubdivisionDto>> GetAllSubdivisionsAsync();
        public Task<IEnumerable<SubdivisionDto>> GetFilteredSubdivisionsAsync(string? name, string? address, string? phoneNumber, string? status);
        public Task UpdateSubdivisionAsync(Guid id, SubdivisionDto dto);
        public Task DeleteSubdivisionAsync(Guid id);
    }
}