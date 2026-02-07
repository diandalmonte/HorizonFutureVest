using Application.DTOs.Entities;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.MacroIndicator;
using Microsoft.AspNetCore.Mvc;

namespace HorizonFutureVest.Controllers
{
    public class MacroIndicatorController : Controller
    {
        private readonly MacroIndicatorService _service;

        public MacroIndicatorController(MacroIndicatorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAll();
            var vms = dtos.Select(d => new MacroIndicatorViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Weight = d.Weight,
                IsBetterHigh = d.IsBetterHigh
            }).ToList();
            return View(vms);
        }

        public IActionResult Create()
        {
            return View("Save", new SaveMacroIndicatorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveMacroIndicatorViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            await _service.Add(new MacroIndicatorDto
            {
                Name = vm.Name,
                Weight = vm.Weight,
                IsBetterHigh = vm.IsBetterHigh
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var all = await _service.GetAll();
            var dto = all.FirstOrDefault(x => x.Id == id);

            if (dto == null) return RedirectToAction("Index");

            ViewBag.EditMode = true;
            return View("Save", new SaveMacroIndicatorViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Weight = dto.Weight,
                IsBetterHigh = dto.IsBetterHigh
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveMacroIndicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            await _service.Update(new MacroIndicatorDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Weight = vm.Weight,
                IsBetterHigh = vm.IsBetterHigh
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
    }
}