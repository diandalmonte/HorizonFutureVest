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
    public class MacroIndicatorService
    {
        private readonly Repository<MacroIndicator> _repo;

        public MacroIndicatorService(Repository<MacroIndicator> repo)
        {
            _repo = repo;
        }

        public async Task Add(MacroIndicatorDto macroIndicatorDto)
        {
            await _repo.AddAsync(new MacroIndicator
            {
                Name = macroIndicatorDto.Name,
                Weight = macroIndicatorDto.Weight,
                IsBetterHigh = macroIndicatorDto.IsBetterHigh
            });
        }

        public async Task<List<MacroIndicatorDto>> GetAll()
        {
            List<MacroIndicator> macroIndicators = await _repo.GetAll();
            List<MacroIndicatorDto> mappedMacroIndicators = macroIndicators.Select(m => new MacroIndicatorDto {Id = m.Id, Name = m.Name, Weight = m.Weight, IsBetterHigh = m.IsBetterHigh}).ToList();
            return mappedMacroIndicators;
        }

        public async Task Update(MacroIndicatorDto macroIndicatorDto)
        {
            MacroIndicator entity = new MacroIndicator
            {
                Id = macroIndicatorDto.Id,
                Name = macroIndicatorDto.Name,
                Weight = macroIndicatorDto.Weight,
                IsBetterHigh = macroIndicatorDto.IsBetterHigh
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
