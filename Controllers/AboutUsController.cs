using Microsoft.AspNetCore.Mvc;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Models;
using UserRoles.Services;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAboutUsService _service;
        public AboutUsController(ILogger<HomeController> logger, IAboutUsService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var aboutUs = await _service.List();
            return View(aboutUs);
        }


        public async Task<IActionResult> Create()
        {  
            var model = new AboutUsRequestDto();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsRequestDto model)
        {
            if (ModelState.IsValid)
            {
                await _service.AddOrUpdateAboutUsAsync(model);
                return RedirectToAction("Create");
            }
            return View(model);
        }



        public async Task<IActionResult> Edit(Guid id)
        {
            var aboutUs = await _service.GetById(id);
            if (aboutUs == null)
                return NotFound();

            return View(aboutUs);
        }


    }
}
