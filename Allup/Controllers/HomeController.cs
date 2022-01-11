using Allup.DAL;
using Allup.Models;
using Allup.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Allup.Controllers
{
    public class HomeController : Controller
    {
        private readonly AllupDbContext _context;
        public HomeController(AllupDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Where(c=>!c.IsDeleted && c.IsMain).ToListAsync();
            var homeVM = new HomeViewModel
            {
                Categories = categories,
            };
            return View(homeVM);
        }

    }
}
