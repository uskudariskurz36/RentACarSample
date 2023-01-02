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
                if (_databaseContext.Brands.Any(x => x.Name.ToLower() == model.Name.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Name), "İlgili marka zaten bulunmaktadır.");
                }
                else
                {
                    Brand brand = new Brand();
                    brand.Name = model.Name;
                    brand.Description = model.Description;
                    brand.Hidden = model.Hidden;

                    _databaseContext.Brands.Add(brand);
                    _databaseContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Brand brand = _databaseContext.Brands.Find(id);

            if (brand == null)
            {
                return RedirectToAction(nameof(Index));
            }

            BrandEditViewModel model = new BrandEditViewModel();
            model.Name = brand.Name;
            model.Description = brand.Description;
            model.Hidden = brand.Hidden;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, BrandEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand brand = _databaseContext.Brands.Find(id);

                if (brand == null)
                {
                    ModelState.AddModelError("", "İlgili marka bulunamadı.");
                }
                else
                {
                    if (_databaseContext.Brands.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != id))
                    {
                        ModelState.AddModelError(nameof(model.Name), "İlgili marka zaten bulunmaktadır.");
                    }
                    else
                    {
                        brand.Name = model.Name;
                        brand.Description = model.Description;
                        brand.Hidden = model.Hidden;

                        _databaseContext.SaveChanges();

                        return RedirectToAction(nameof(Index));
                    }
                }
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
