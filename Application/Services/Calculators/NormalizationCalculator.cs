using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Ranking;
using Persistence.Common;
using Persistence.Entidades;

namespace Application.Services.Calculators
{
    public class NormalizationCalculator
    {
        public static List<NormalizedIndicatorDto> Normalize(List<CountryIndicator> indicatorValues)
        {
            if (indicatorValues == null || !indicatorValues.Any())
                return new List<NormalizedIndicatorDto>();

            // Se agrupa por macroindicadores
            var GroupedIndicators = indicatorValues.GroupBy(iv => iv.MacroIndicatorId);
            var normalizedIndicators = new List<NormalizedIndicatorDto>();

            foreach (var group in GroupedIndicators)
            {
                decimal maxValue = group.Max(iv => iv.Value);
                decimal minValue = group.Min(iv => iv.Value);

                foreach (var indicator in group)
                {
                    decimal normalizedValue;


                    if (maxValue - minValue == 0) //Para evitar division por 0
                    {
                        normalizedValue = 1.0m;
                    }
                    else
                    {
                        if (indicator.MacroIndicator.IsBetterHigh)
                        {
                            normalizedValue = BetterHighFormula(maxValue, minValue, indicator.Value);
                        }
                        else
                        {
                            normalizedValue = BetterLowFormula(maxValue, minValue, indicator.Value);
                        }
                    }

                    normalizedIndicators.Add(new NormalizedIndicatorDto
                    {
                        NormalizedValue = normalizedValue,
                        Year = indicator.Year,
                        CountryId = indicator.CountryId,
                        Country = indicator.Country,
                        MacroIndicatorId = indicator.MacroIndicatorId,
                        MacroIndicator = indicator.MacroIndicator
                    });
                }
            }

            return normalizedIndicators;
        }

        private static decimal BetterHighFormula(decimal maxValue, decimal minValue, decimal indicatorValue)
        {
            return (indicatorValue - minValue) / (maxValue - minValue);
        }

        private static decimal BetterLowFormula(decimal maxValue, decimal minValue, decimal indicatorValue)
        {
            return (maxValue - indicatorValue) / (maxValue - minValue);
        }
    }
}
