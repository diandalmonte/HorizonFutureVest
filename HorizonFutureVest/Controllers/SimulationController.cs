using Application.DTOs.Entities;
using Application.Services;
using Application.ViewModels;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.SimulationIndicator;

namespace HorizonFutureVest.Controllers
{
    public class SimulationController : Controller
    {
        private readonly SimulationService _simulationService;
        private readonly MacroIndicatorService _macroIndicatorService;
        private readonly RankingService _rankingService;
        private readonly CountryIndicatorService _countryIndicatorService;

        public SimulationController(SimulationService simulationService, MacroIndicatorService macroIndicatorService, RankingService rankingService, CountryIndicatorService countryIndicatorService)
        {
            _simulationService = simulationService;
            _macroIndicatorService = macroIndicatorService;
            _rankingService = rankingService;
            _countryIndicatorService = countryIndicatorService;
        }

        // --- CRUD DE INDICADORES DE SIMULACIÓN ---

        public async Task<IActionResult> Index()
        {
            var dtos = await _simulationService.GetAll();
            var vms = dtos.Select(d => new SimulationIndicatorViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Weight = d.Weight,
                IsBetterHigh = d.IsBetterHigh,
                // Asumiendo que el DTO viene con la propiedad de navegación o buscamos el nombre
                MacroIndicatorName = d.MacroIndicator != null ? d.MacroIndicator.Name : "ID: " + d.MacroIndicatorId
            }).ToList();
            return View(vms);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.MacroIndicators = await _macroIndicatorService.GetAll();
            return View("Save", new SaveSimulationIndicatorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSimulationIndicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MacroIndicators = await _macroIndicatorService.GetAll();
                return View("Save", vm);
            }

            await _simulationService.Add(new SimulationIndicatorDto
            {
                Name = vm.Name,
                Weight = vm.Weight,
                IsBetterHigh = vm.IsBetterHigh,
                MacroIndicatorId = vm.MacroIndicatorId
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            // Simulación GetById
            var all = await _simulationService.GetAll();
            var dto = all.FirstOrDefault(x => x.Id == id);

            if (dto == null) return RedirectToAction("Index");

            ViewBag.EditMode = true;
            ViewBag.MacroIndicators = await _macroIndicatorService.GetAll();

            return View("Save", new SaveSimulationIndicatorViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Weight = dto.Weight,
                IsBetterHigh = dto.IsBetterHigh,
                MacroIndicatorId = dto.MacroIndicatorId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveSimulationIndicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                ViewBag.MacroIndicators = await _macroIndicatorService.GetAll();
                return View("Save", vm);
            }

            await _simulationService.Update(new SimulationIndicatorDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Weight = vm.Weight,
                IsBetterHigh = vm.IsBetterHigh,
                MacroIndicatorId = vm.MacroIndicatorId
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            // Simulación GetById
            var all = await _simulationService.GetAll();
            var dto = all.FirstOrDefault(x => x.Id == id);
            if (dto == null) return RedirectToAction("Index");

            return View(new DeleteViewModel { Id = dto.Id, Name = dto.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel vm)
        {
            await _simulationService.Delete(vm.Id);
            return RedirectToAction("Index");
        }

        // --- RANKING SIMULADO ---

        public async Task<IActionResult> GetSimulatedRanking(int? year)
        {
            var vm = new RankingHomeViewModel();

            // Obtener años disponibles
            var indicators = await _countryIndicatorService.GetAll();
            vm.AvailableYears = indicators.Select(i => i.Year).Distinct().OrderByDescending(y => y).ToList();

            if (year.HasValue)
            {
                vm.SelectedYear = year.Value;
                try
                {
                    // isSimulation = true
                    var results = await _rankingService.GenerateCountryRanking(year.Value, true);
                    vm.RankingResults = results.ToList();
                }
                catch (InsufficientEligibleCountries ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(vm);
        }
    }
}
