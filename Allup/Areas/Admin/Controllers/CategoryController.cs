using Allup.Areas.Admin.ViewModels;
using Allup.DAL;
using Allup.Models;
using Allup.Models.Entity;
using Fiorella.Areas.Admin.Controllers.Constants;
using Fiorella.Areas.Admin.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AllupDbContext _context;
        public CategoryController(AllupDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories.Include(c=>c.Children).FirstOrDefaultAsync(c=>c.Id==id);
            if (category==null)
            {
                return NotFound();
            }
            return View(category);
        }
        
        public async Task<IActionResult> Create()
        {
            var parents = await _context.Categories.Where(c=>c.IsMain && !c.IsDeleted).ToListAsync();
            ViewBag.Parents = parents;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            var parents = await _context.Categories.Where(c => c.IsMain && !c.IsDeleted).ToListAsync();
            ViewBag.Parents = parents;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.IsMain)
            {
                if (model.File==null)
                {
                    return View();
                }
                if (!model.File.IsContains())
                {
                    ModelState.AddModelError("File", "File is not supported");
                    return View();
                }

                if (model.File.IsRightSize(1024))
                {
                    ModelState.AddModelError(nameof(model.File), "Size can not be greater than 1mb.");
                }

                var imageName = FileUtils.FileCreate(FileConstants.ImagePath, model.File);

                Category category = new Category
                {
                    Name = model.Name,
                    Image = imageName,
                    IsMain = model.IsMain,
                };

                await _context.Categories.AddAsync(category);
            }
            else
            {
                var parent = await _context.Categories.FirstOrDefaultAsync(c=>!c.IsMain && !c.IsDeleted && c.Id==model.ParentId);
                if (parent == null)
                {
                    ModelState.AddModelError("", "Choose valid category");
                    return View();
                }

                Category category = new Category
                {
                    Name = model.Name,
                    Image = "wrong",
                    IsMain = model.IsMain,
                    Parent = parent,
                };

                await _context.Categories.AddAsync(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
