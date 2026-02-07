using Application.DTOs.Entities;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.Country;
using Microsoft.AspNetCore.Mvc;

namespace HorizonFutureVest.Controllers
{
    public class CountryController : Controller
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _countryService.GetAll();
            var vms = dtos.Select(d => new CountryViewModel
            {
                Id = d.Id,
                Name = d.Name,
                IsoCode = d.IsoCode
            }).ToList();

            return View(vms);
        }

        public IActionResult Create()
        {
            return View("Save", new SaveCountryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            var dto = new CountryDto
            {
                Name = vm.Name,
                IsoCode = vm.IsoCode
            };

            await _countryService.Add(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var all = await _countryService.GetAll();
            var dto = all.FirstOrDefault(x => x.Id == id);

            if (dto == null) return RedirectToAction("Index");

            var vm = new SaveCountryViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                IsoCode = dto.IsoCode
            };

            ViewBag.EditMode = true;
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            var dto = new CountryDto
            {
                Id = vm.Id,
                Name = vm.Name,
                IsoCode = vm.IsoCode
            };

            await _countryService.Update(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var all = await _countryService.GetAll();
            var dto = all.FirstOrDefault(x => x.Id == id);

            if (dto == null) return RedirectToAction("Index");

            return View(new DeleteViewModel { Id = dto.Id, Name = $"Registro {dto.Id}" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel vm)
        {
            await _countryService.Delete(vm.Id);
            return RedirectToAction("Index");
        }
    }
}