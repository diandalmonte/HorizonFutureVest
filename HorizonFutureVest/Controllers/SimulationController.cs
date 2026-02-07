using Application.DTOs.Entities;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HorizonFutureVest.Controllers
{
    public class SimulationIndicatorController : Controller
    {
        private readonly SimulationService _simService;
        private readonly MacroIndicatorService _macroService;

        public SimulationIndicatorController(SimulationService simService, MacroIndicatorService macroService)
        {
            _simService = simService;
            _macroService = macroService;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _simService.GetAll();
            var macros = (await _macroService.GetAll()).ToDictionary(k => k.Id, v => v.Name); // Cache para mapeo

            var vms = dtos.Select(d => new SimulationIndicatorViewModel
            {
                Id = d.Id ?? 0,
                Name = d.Name,
                Weight = d.Weight,
                IsBetterHigh = d.IsBetterHigh,
                MacroIndicatorName = macros.ContainsKey(d.MacroIndicatorId) ? macros[d.MacroIndicatorId] : "N/A"
            }).ToList();
            return View(vms);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.MacroIndicators = await _macroService.GetAll();
            return View("Save", new SaveSimulationIndicatorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSimulationIndicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MacroIndicators = await _macroService.GetAll();
                return View("Save", vm);
            }

            await _simService.Add(new SimulationIndicatorDto
            {
                Name = vm.Name,
                Weight = vm.Weight,
                IsBetterHigh = vm.IsBetterHigh,
                MacroIndicatorId = vm.MacroIndicatorId ?? 0
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = (await _simService.GetAll()).FirstOrDefault(x => x.Id == id);
            if (dto == null) return RedirectToAction("Index");

            ViewBag.EditMode = true;
            ViewBag.MacroIndicators = await _macroService.GetAll();

            return View("Save", new SaveSimulationIndicatorViewModel
            {
                Id = dto.Id ?? 0,
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
                ViewBag.MacroIndicators = await _macroService.GetAll();
                return View("Save", vm);
            }

            await _simService.Update(new SimulationIndicatorDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Weight = vm.Weight,
                IsBetterHigh = vm.IsBetterHigh,
                MacroIndicatorId = vm.MacroIndicatorId ?? 0
            });
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var dto = (await _simService.GetAll()).FirstOrDefault(x => x.Id == id);
            if (dto == null) return RedirectToAction("Index");
            return View(new DeleteViewModel { Id = dto.Id ?? 0, Name = dto.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel vm)
        {
            await _simService.Delete(vm.Id);
            return RedirectToAction("Index");
        }
    }
}
