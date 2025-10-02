using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Models;
using UserRoles.Services;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarousalService _carousalService;
        private readonly IDealService _dealService;
        private readonly IAboutUsService _aboutUsService;
        private readonly ITrekAppService _trekAppService;
        public HomeController(ILogger<HomeController> logger, ICarousalService carousalService, IDealService dealService, IAboutUsService aboutUsService, ITrekAppService trekAppService)
        {
            _logger = logger;
            _carousalService = carousalService;
            _dealService = dealService;
            _aboutUsService = aboutUsService;
            _trekAppService = trekAppService;
        }





        public async Task<IActionResult> Index()
        {   
            var carouselImages = await _carousalService.List(CarousalEnum.Home);
            ViewBag.CarouselImages = carouselImages;
            //var deals = await _dealService.List();
             var deals = await _trekAppService.TopThreeTrekData(); // List<DealResponseDto>
            ViewBag.PromoDeals = deals;

            ViewBag.aboutUs = await _aboutUsService.List();
            return View();
        }

      


        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult User()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
