using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Models;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class NabBarContentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public NabBarContentController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: NabBarContent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NabBarContent model)
        {
            model.Id = Guid.NewGuid();
       

                foreach (var item in model.Items)
                {
                    item.Id = Guid.NewGuid();
                    item.NavBarContentId = model.Id;
                }

                _context.NavBarContents.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            

            //return View(model);
        }

    }
}
