using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Entities;
using Persistence.Entidades;
using Persistence.Repositories;

namespace Application.Services
{
    public class CountryService
    {
        private readonly Repository<Country> _repo;

        public CountryService(Repository<Country> repo)
        {
            _repo = repo;
        }

        public async Task Add(CountryDto countryDto)
        {
            await _repo.AddAsync(new Country
            {
                Name = countryDto.Name,
                IsoCode = countryDto.IsoCode
            });
        }

        public async Task<List<CountryDto>> GetAll()
        {
            List<Country> countries = await _repo.GetAll();
            List<CountryDto> mappedCountries = countries.Select(c => new CountryDto { Id = c.Id, Name = c.Name, IsoCode = c.IsoCode}).ToList();
            return mappedCountries;
        }

        public async Task Update(CountryDto countryDto)
        {
            Country entity = new Country
            {
                Id = countryDto.Id,
                Name = countryDto.Name,
                IsoCode = countryDto.IsoCode
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
