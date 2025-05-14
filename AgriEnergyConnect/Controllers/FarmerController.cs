using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FarmerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult Index()
        {
            var email = User.Identity.Name;
            var farmer = _context.Farmers.FirstOrDefault(f => f.Email.ToLower() == email.ToLower());

            if (farmer == null)
                return Unauthorized();

            var products = _context.Products
                .Where(p => p.FarmerID == farmer.FarmerID)
                .ToList();

            return View(products);
        }


        public IActionResult AddProduct() => View();

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var email = User.Identity?.Name;
            TempData["Error"] = $"Trying to find farmer with email: {email}";

            if (!ModelState.IsValid) return View(product);

            var farmer = _context.Farmers
                .FirstOrDefault(f => f.Email.ToLower() == email.ToLower());

            if (farmer == null)
            {
                TempData["Error"] = "Farmer profile not found.";
                return Unauthorized();
            }

            product.ProductionDate = DateTime.Now;
            product.FarmerID = farmer.FarmerID;

            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["Success"] = "Product added successfully!";
            return RedirectToAction("Index");
        }

    }


}
