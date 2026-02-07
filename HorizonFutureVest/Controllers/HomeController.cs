using Application.DTOs;
using Application.Exceptions;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.ReturnRate;
using Microsoft.AspNetCore.Mvc;

namespace HorizonFutureVest.Controllers
{
    public class HomeController : Controller
    {
        private readonly RankingService _rankingService;
        private readonly CountryIndicatorService _countryIndicatorService;
        private readonly AppService _appService;

        public HomeController(RankingService rankingService, CountryIndicatorService countryIndicatorService, AppService appService)
        {
            _rankingService = rankingService;
            _countryIndicatorService = countryIndicatorService;
            _appService = appService;
        }

        public async Task<IActionResult> Index(int? year)
        {
            var vm = new RankingHomeViewModel();

            //Obtener los años disponibles para el select
            var indicators = await _countryIndicatorService.GetAll();
            vm.AvailableYears = indicators.Select(i => i.Year).Distinct().OrderByDescending(y => y).ToList();

            //Si se selecciona un año a traves de GetRanking, se da el ranking
            if (year.HasValue)
            {
                vm.SelectedYear = year.Value;
                try
                {
                    var results = await _rankingService.GenerateCountryRanking(year.Value, false);
                    vm.RankingResults = results.ToList();
                }
                catch (InsufficientEligibleCountries ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al calcular el ranking.");
                }
            }

            return View(vm);
        }

        public Task<IActionResult> GetRanking(int? year)
        {
            return Index(year);
        }

        public async Task<IActionResult> ReturnRate()
        {
            // Obtener configuracion actual
            var dto = await _appService.GetReturnRate();

            var vm = new ReturnRateViewModel
            {
                MinReturnRate = dto.MinReturnRate,
                MaxReturnRate = dto.MaxReturnRate
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnRate(ReturnRateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var dto = new ReturnRateDto
                {
                    MinReturnRate = vm.MinReturnRate,
                    MaxReturnRate = vm.MaxReturnRate
                };


                await _appService.SetReturnRate(dto);

                ViewBag.Message = "Tasas actualizadas correctamente.";
                return View(vm);
            }
            catch (InvalidReturnRateException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar las tasas.");
                return View(vm);
            }
        }
    }
}