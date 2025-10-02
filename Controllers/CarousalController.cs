using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class CarousalController : Controller
    {
        private readonly ICarousalService _carousalService;
        public CarousalController(ICarousalService carousalService)
        {
            this._carousalService = carousalService;
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index(CarousalEnum? filter)
        {
            var enumToUse = filter ?? CarousalEnum.Home;
            ViewBag.CurrentFilter = enumToUse;
            ViewBag.EnumOptions = Enum.GetValues(typeof(CarousalEnum)).Cast<CarousalEnum>();

            var carousals = await _carousalService.List(enumToUse);
            return View(carousals);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarousalImageRequestDto dto)
        {
            var response = await this._carousalService.Create(dto); // assuming _homeService is injected and has Create method

            if (response != null)
                return RedirectToAction("Index"); // Redirect or show success

            ModelState.AddModelError("", "Failed to upload image.");
            return View(dto);
        }



        public async Task<IActionResult> Edit(Guid id)
        {
            var carousal = await _carousalService.GetById(id);
            if (carousal == null)
                return NotFound();

            ViewBag.EnumOptions = Enum.GetValues(typeof(CarousalEnum)).Cast<CarousalEnum>();
            return View(carousal);
        }

        // ✅ POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(CarousalImageRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EnumOptions = Enum.GetValues(typeof(CarousalEnum)).Cast<CarousalEnum>();
                return View(model);
            }

            var success = await _carousalService.Update(model);

            TempData[success ? "Success" : "Error"] = success ? "Updated successfully." : "Update failed.";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _carousalService.Delete(id);

            if (result)
            {
                TempData["Success"] = "Image deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Image could not be deleted.";
            }

            return RedirectToAction(nameof(Index));
        }
   


    }
}
