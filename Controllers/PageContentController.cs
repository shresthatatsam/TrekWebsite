using Microsoft.AspNetCore.Mvc;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Models;
using UserRoles.Services;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class PageContentController : Controller
    {
        private readonly IPageContentService _service;

        public PageContentController(IPageContentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(PageContentTypeEnum? contentTypeFilter, bool? isPublishedFilter)
        {
            var model = await _service.GetFilteredPageContentsAsync(contentTypeFilter, isPublishedFilter);

            ViewBag.ContentTypeFilter = contentTypeFilter;
            ViewBag.IsPublishedFilter = isPublishedFilter;
            ViewBag.ContentTypes = Enum.GetNames(typeof(PageContentTypeEnum));

            return View(model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var contents = await _service.GetAllAsync();
        //    return View(contents);
        //}

        public async Task<IActionResult> Details(Guid id)
        {
            var content = await _service.GetByIdAsync(id);
            if (content == null) return NotFound();
            return View(content);
        }

        public IActionResult Create()
        {
            ViewBag.ContentTypes = Enum.GetValues(typeof(PageContentTypeEnum));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PageContentRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ContentTypes = Enum.GetValues(typeof(PageContentTypeEnum));
                return View(model);
            }

            model.Id = Guid.NewGuid();
            model.CreatedAt = DateTime.UtcNow;
            await _service.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var content = await _service.GetByIdAsync(id);
            if (content == null) return NotFound();

            ViewBag.ContentTypes = Enum.GetValues(typeof(PageContentTypeEnum));
            return View(content);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PageContentRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ContentTypes = Enum.GetValues(typeof(PageContentTypeEnum));
                return View(model);
            }

            model.UpdatedAt = DateTime.UtcNow;
            await _service.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var content = await _service.GetByIdAsync(id);
            if (content == null) return NotFound();
            return View(content);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
