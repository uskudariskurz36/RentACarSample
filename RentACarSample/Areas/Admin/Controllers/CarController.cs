using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACarSample.Areas.Admin.Models;
using RentACarSample.Entities;

namespace RentACarSample.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private DatabaseContext _databaseContext;

        public CarController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET: CarController
        public ActionResult Index()
        {
            List<Car> cars = _databaseContext.Cars.Include(x => x.Brand).Include(x => x.SubBrand).ToList();
            return View(cars);
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            CarCreateViewModel model = new CarCreateViewModel();
            model.Brands = new SelectList(_databaseContext.Brands.ToList(),
                    nameof(Brand.Id), nameof(Brand.Name));

            return View(model);
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car()
                {
                    BrandId = model.BrandId,
                    SubBrandId = model.SubBrandId,
                    Plate = model.Plate,
                    DailyPrice = model.DailyPrice,
                    IsAvailable = model.IsAvailable,
                };

                _databaseContext.Cars.Add(car);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            model.Brands = new SelectList(_databaseContext.Brands.ToList(),
                    nameof(Brand.Id), nameof(Brand.Name));

            return View(model);
        }


        // GET: Admin/Car/GetSubBrands?brandId=1
        public IActionResult GetSubBrands(int brandId)
        {
            List<SubBrand> subBrands =
                _databaseContext.SubBrands.Where(x => x.BrandId == brandId).ToList();

            return PartialView("_SubBrandsPartial", subBrands);
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
