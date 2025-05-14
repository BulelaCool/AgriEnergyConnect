using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.AgriEnergyConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult RegisterFarmer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterFarmer(RegisterFarmerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Farmer");

                var farmer = new Farmer
                {
                    Name = model.Name,
                    Email = model.Email,
                    Contact = model.Contact,
                    FarmerID = new Random().Next(1000, 9999)
                };

                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Farmer registered successfully!";
                return RedirectToAction("Index");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public IActionResult Index()
        {
            var farmers = _context.Farmers.Include(f => f.Products).ToList();
            return View(farmers);
        }


        public IActionResult FilterProducts(string category, DateTime? from, DateTime? to)
        {
            var query = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.Contains(category));

            if (from.HasValue)
                query = query.Where(p => p.ProductionDate >= from.Value);

            if (to.HasValue)
                query = query.Where(p => p.ProductionDate <= to.Value);

            return View(query.ToList());
        }
    }
}
