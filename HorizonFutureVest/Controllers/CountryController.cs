using Microsoft.AspNetCore.Mvc;

namespace HorizonFutureVest.Controllers
{
    using Application.DTOs.Entities;
    using Application.Services;
    using HorizonFutureVest.Application.Dtos;
    using HorizonFutureVest.Application.Services;
    using HorizonFutureVest.Models.ViewModels;
    using Microsoft.AspNetCore.Mvc;

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
                Id = d.Id ?? 0,
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
            if (!ModelState.IsValid) return View("Save", vm);

            await _countryService.Add(new CountryDto { Name = vm.Name, IsoCode = vm.IsoCode });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = (await _countryService.GetAll()).FirstOrDefault(x => x.Id == id); // Asumiendo GetAll si no hay GetById
            if (dto == null) return RedirectToAction("Index");

            ViewBag.EditMode = true;
            return View("Save", new SaveCountryViewModel { Id = dto.Id ?? 0, Name = dto.Name, IsoCode = dto.IsoCode });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            await _countryService.Update(new CountryDto { Id = vm.Id, Name = vm.Name, IsoCode = vm.IsoCode });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = (await _countryService.GetAll()).FirstOrDefault(x => x.Id == id);
            if (dto == null) return RedirectToAction("Index");
            return View(new DeleteViewModel { Id = dto.Id ?? 0, Name = dto.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel vm)
        {
            await _countryService.Delete(vm.Id);
            return RedirectToAction("Index");
        }
    }
}
