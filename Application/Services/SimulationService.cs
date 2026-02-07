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
    public class SimulationService
    {
        private readonly Repository<SimulationIndicator> _repo;

        public SimulationService(Repository<SimulationIndicator> repo)
        {
            _repo = repo;
        }

        public async Task Add(SimulationIndicatorDto dto)
        {
            await _repo.AddAsync(new SimulationIndicator
            {
                Name = dto.Name,
                Weight = dto.Weight,
                IsBetterHigh = dto.IsBetterHigh,
                MacroIndicatorId = dto.MacroIndicatorId
            });
        }

        public async Task<List<SimulationIndicatorDto>> GetAll()
        {
            List<SimulationIndicator> simulationIndicators = await _repo.GetAll();
            List<SimulationIndicatorDto> mappedSimIndicators = simulationIndicators.Select(si => new SimulationIndicatorDto { Id = si.Id, Name = si.Name, Weight = si.Weight, IsBetterHigh = si.IsBetterHigh, MacroIndicatorId = si.MacroIndicatorId}).ToList();
            return mappedSimIndicators;
        }

        public async Task Update(SimulationIndicatorDto dto)
        {
            SimulationIndicator entity = new SimulationIndicator
            {
                Id = dto.Id,
                Name = dto.Name,
                Weight = dto.Weight,
                IsBetterHigh = dto.IsBetterHigh,
                MacroIndicatorId = dto.MacroIndicatorId
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
