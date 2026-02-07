using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Calculators;
using Microsoft.VisualBasic;
using Persistence.Common;
using Persistence.Entidades;
using Persistence.Repositories;
using Application.Exceptions;
using Application.DTOs.Ranking;

namespace Application.Services
{
    public class RankingService
    {
        private readonly Repository<MacroIndicator> _macroIndicatorRepo;
        private readonly CountryIndicatorRepository _countryIndicatorRepo;
        private readonly Repository<SimulationIndicator> _simIndicatorRepo;
        private readonly Repository<Country> _countryRepo;
        private readonly RankCalculator _rankCalculator;

        public RankingService(Repository<MacroIndicator> macroIndicatorRepo, Repository<SimulationIndicator> simIndicatorRepo, 
            CountryIndicatorRepository countryIndicatorRepo, Repository<Country> countryRepo, RankCalculator rankCalculator)
        {
            _macroIndicatorRepo = macroIndicatorRepo;
            _simIndicatorRepo = simIndicatorRepo;
            _countryIndicatorRepo = countryIndicatorRepo;
            _countryRepo = countryRepo;
            _rankCalculator = rankCalculator;
        }

        public async Task<IEnumerable<RankingResultDto>> GenerateCountryRanking(int year, bool isSimulation)
        {
            List <CountryIndicator> indicatorValues = await _countryIndicatorRepo.GetByFilter(year); //Solo se toman los valores del año pedido
            IEnumerable<IMacroIndicator> indicatorTypes;
            if (!isSimulation)
            {
                indicatorTypes = await _macroIndicatorRepo.GetAll();
            }
            else
            {
                indicatorTypes = await _simIndicatorRepo.GetAll();
            }

            indicatorValues = await ProcessEligibleCountries(indicatorValues, indicatorTypes.ToList());

            if (indicatorValues.Count == 1)
            {
                throw new InsufficientEligibleCountries($"No hay suficiente países para poder calcular el ranking y la tasa de retorno, el único país que cumple con los requisitos es {indicatorValues[0].Country.Name}. Debe agregar más indicadores a los demás países en el año seleccionado o registrar mas macroindicadores para los paises existentes");

            }
            else if (indicatorValues.Count < 1)
            {
                throw new InsufficientEligibleCountries($"No hay suficiente países para poder calcular el ranking y la tasa de retorno. Debe agregar más indicadores a los demás países en el año seleccionado o registrar mas macroindicadores para los paises existentes");
            }
            return _rankCalculator.GenerateRankingData(indicatorValues, indicatorTypes).OrderBy(r => r.Score);
        }

        private async Task<List<CountryIndicator>> ProcessEligibleCountries(List<CountryIndicator> indicatorValues, List<IMacroIndicator> indicatorTypes) //Revisa los paises que tienen el numero adecuado de macroindicadores registrados para estar en el ranking
        {
            List<Country> countries = await _countryRepo.GetAll();
            var requiredIndicators = indicatorTypes.Where(t => t.Weight > 0).Select(t => t.Id).ToList(); //Se deja fuera los macroindicadores que tienen un peso igual a 0 

            int macroIndicatorCount = requiredIndicators.Count;
            List<CountryIndicator> eligibleCountries = [];

            foreach (Country country in countries)
            {
                var countryValues = indicatorValues.Where(iv => iv.CountryId == country.Id).ToList(); //Primero se filtra por pais

                var validCountryValues = countryValues.Where(cv => requiredIndicators.Contains(cv.MacroIndicatorId)).ToList(); //Luego se verifica que el countryIndicator tenga un FK de uno de los macroindicadores validos

                int count = validCountryValues.Select(v => v.MacroIndicatorId).Count(); 

                if (count == macroIndicatorCount) // Se valida que la cantidad de indicadores validos sea la misma cantidad que los macroindicadores necesarios
                {
                    eligibleCountries.AddRange(validCountryValues);
                }

            }

            return eligibleCountries;

        }
    }
}
