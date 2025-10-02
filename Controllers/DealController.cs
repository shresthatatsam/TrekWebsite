using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Services;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class DealController : Controller
    {
        private readonly IDealService _dealService;
        public DealController(IDealService dealService)
        {
            _dealService = dealService;
        }

        public async Task<IActionResult> Index()
        {
            var Deals = await _dealService.List();
            return View(Deals);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DealRequestDto dto)
        {
            var response = await this._dealService.Create(dto); 

            if (response != null)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to upload image.");
            return View(dto);
        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var Deal = await _dealService.GetById(id);
            return View(Deal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DealRequestDto model)
        {
           
            var success = await _dealService.Update(model);

            TempData[success ? "Success" : "Error"] = success ? "Updated successfully." : "Update failed.";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deal =await _dealService.Delete(id);
            if (deal == null)
            {
                return NotFound();
            }

          

            return RedirectToAction("Index"); // Or wherever you want to redirect after delete
        }

    }
}
