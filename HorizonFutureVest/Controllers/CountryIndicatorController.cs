using Application.DTOs.Entities;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.CountryIndicator;
using Microsoft.AspNetCore.Mvc;

namespace HorizonFutureVest.Controllers
{
    public class CountryIndicatorController : Controller
    {
        private readonly CountryIndicatorService _service;
        private readonly CountryService _countryService;
        private readonly MacroIndicatorService _macroIndicatorService;

        public CountryIndicatorController(CountryIndicatorService service, CountryService countryService, MacroIndicatorService macroIndicatorService)
        {
            _service = service;
            _countryService = countryService;
            _macroIndicatorService = macroIndicatorService;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAll();
            // Nota: Para mostrar nombres en el index, los DTOs deberían incluir la entidad o el nombre,
            // o se deben cargar los paises/macros aparte. Asumo que el DTO incluye las propiedades de navegación.
            var vms = dtos.Select(d => new CountryIndicatorViewModel
            {
                Id = d.Id,
                CountryName = d.Country != null ? d.Country.Name : "N/A", // Null check por seguridad
                MacroIndicatorName = d.MacroIndicator != null ? d.MacroIndicator.Name : "N/A",
                Value = d.Value,
                Year = d.Year
            }).ToList();
            return View(vms);
        }

        public async Task<IActionResult> Create()
        {
            await LoadViewBags();
            return View("Save", new SaveCountryIndicatorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCountryIndicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadViewBags();
                return View("Save", vm);
            }

            await _service.Add(new CountryIndicatorDto
            {
                CountryId = vm.CountryId,
                MacroIndicatorId = vm.MacroIndicatorId,
                Value = vm.Value,
                Year = vm.Year
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            // Asumiendo implementación de GetById en el servicio
            // var dto = await _service.GetById(id); 
            // Implementación simulada basada en los servicios disponibles en el contexto:
            var all = await _service.GetAll();
            var dto = all.FirstOrDefault(x => x.Id == id);

            if (dto == null) return RedirectToAction("Index");

            await LoadViewBags();
            ViewBag.EditMode = true;
            return View("Save", new SaveCountryIndicatorViewModel
            {
                Id = dto.Id,
                CountryId = dto.CountryId,
                MacroIndicatorId = dto.MacroIndicatorId,
                Value = dto.Value,
                Year = dto.Year
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCountryIndicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadViewBags();
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            await _service.Update(new CountryIndicatorDto
            {
                Id = vm.Id,
                CountryId = vm.CountryId,
                MacroIndicatorId = vm.MacroIndicatorId,
                Value = vm.Value,
                Year = vm.Year
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {

            var all = await _service.GetAll();
            var dto = all.FirstOrDefault(x => x.Id == id);

            if (dto == null) return RedirectToAction("Index");

            return View(new DeleteViewModel { Id = dto.Id, Name = $"Registro {dto.Id}" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel vm)
        {
            await _service.Delete(vm.Id);
            return RedirectToAction("Index");
        }

        private async Task LoadViewBags()
        {
            ViewBag.Countries = await _countryService.GetAll();
            ViewBag.MacroIndicators = await _macroIndicatorService.GetAll();
        }
    }
}