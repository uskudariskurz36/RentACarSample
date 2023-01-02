using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarSample.Areas.Admin.Models;
using RentACarSample.Common;
using RentACarSample.Entities;
using RentACarSample.Managers;

namespace RentACarSample.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class BrandController : Controller
    {
        private IBrandManager _brandManager;

        public BrandController(IBrandManager brandManager)
        {
            _brandManager = brandManager;
        }

        public IActionResult Index()
        {
            List<Brand> model = _brandManager.List();
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
                if (_brandManager.IsNameExists(model.Name))
                {
                    ModelState.AddModelError(nameof(model.Name), "İlgili marka zaten bulunmaktadır.");
                }
                else
                {
                    Brand brand = _brandManager.Create(model);

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Brand? brand = _brandManager.GetById(id);

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
                Brand? brand = _brandManager.GetById(id);

                if (brand == null)
                {
                    ModelState.AddModelError("", "İlgili marka bulunamadı.");
                }
                else
                {
                    if (_brandManager.IsNameExistsBySelfOnly(id, model.Name))
                    {
                        ModelState.AddModelError(nameof(model.Name), "İlgili marka zaten bulunmaktadır.");
                    }
                    else
                    {
                        _brandManager.Update(id, model);

                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Brand? brand = _brandManager.GetById(id);

            if (brand != null)
            {
                _brandManager.Remove(brand);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
