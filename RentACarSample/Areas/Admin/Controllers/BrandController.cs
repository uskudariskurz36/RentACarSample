using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarSample.Areas.Admin.Models;
using RentACarSample.Common;
using RentACarSample.Entities;

namespace RentACarSample.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class BrandController : Controller
    {
        private DatabaseContext _databaseContext;

        public BrandController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            List<Brand> model = _databaseContext.Brands.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BrandCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand brand = new Brand();
                brand.Name = model.Name;
                brand.Description = model.Description;
                brand.Hidden = model.Hidden;

                _databaseContext.Brands.Add(brand);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Brand? brand = _databaseContext.Brands.Find(id);

            if (brand != null)
            {
                _databaseContext.Brands.Remove(brand);
                _databaseContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
