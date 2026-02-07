using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entidades;
using Persistence.Common;
using System.IO.IsolatedStorage;
using Application.DTOs.Ranking;
namespace Application.Services.Calculators
{
    public class RankCalculator
    {
        private readonly AppSettings _appSettings;
        public RankCalculator(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public IEnumerable<RankingResultDto> GenerateRankingData(List<CountryIndicator> indicatorValues, IEnumerable<IMacroIndicator> indicatorTypes)
        {
            List<NormalizedIndicatorDto> normalizedIndicators = NormalizationCalculator.Normalize(indicatorValues);

            var groupedIndicators = normalizedIndicators.GroupBy(i => i.CountryId);

            List<RankingResultDto> rankingData = [];

            foreach (var group in groupedIndicators)
            {

                decimal totalScore = group.Sum(i => i.NormalizedValue * i.MacroIndicator.Weight);

                decimal minReturnRate = _appSettings.MinReturnRate;
                decimal maxReturnRate = _appSettings.MaxReturnRate;

                decimal returnRate = (minReturnRate + (maxReturnRate - minReturnRate) * totalScore) * 100; //Se multiplica por 100 para hacerlo un valor porcentual.

                NormalizedIndicatorDto firstRecord = group.First(); //Se accede al primer elemento del grupo actual, para poder acceder a los datos de las instancia (Country)
                rankingData.Add(new RankingResultDto() { Score = totalScore, ReturnRate = returnRate, CountryName = firstRecord.Country.Name, CountryIsoCode = firstRecord.Country.IsoCode });
            }

            return rankingData;
        }
    }
}
