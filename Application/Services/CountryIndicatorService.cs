using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entidades;
using Application.DTOs.Entities;
using Application.DTOs.Filters;
using Persistence.Repositories;

namespace Application.Services
{
    public class CountryIndicatorService
    {
        private readonly CountryIndicatorRepository _repo;

        public CountryIndicatorService(CountryIndicatorRepository repo)
        {
            _repo = repo;
        }

        public async Task Add(CountryIndicatorDto dto)
        {
            await _repo.AddAsync(new CountryIndicator { CountryId = dto.CountryId, MacroIndicatorId = dto.MacroIndicatorId, Value = dto.Value, Year = dto.Year });
        }

        public async Task<List<CountryIndicatorDto>> GetAll()
        {
            List<CountryIndicator> countryIndicators = await _repo.GetAll();
            List<CountryIndicatorDto> mappedIndicators = countryIndicators.Select(ci => new CountryIndicatorDto { Id = ci.Id, MacroIndicatorId = ci.MacroIndicatorId, CountryId = ci.CountryId, Value = ci.Value, Year = ci.Year }).ToList();
            return mappedIndicators;
        }

        public async Task Update(CountryIndicatorDto dto)
        {
            CountryIndicator entity = new CountryIndicator { Id = dto.Id,
                CountryId = dto.CountryId,
                MacroIndicatorId = dto.MacroIndicatorId,
                Value = dto.Value,
                Year = dto.Year };
            _repo.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            _repo.DeleteAsync(id);
        }

        public async Task GetByFilter(CountryIndicatorFilter filter)
        {
            _repo.GetByFilter(filter.Year, filter.MacroIndicatorId);
        }
    }
}
