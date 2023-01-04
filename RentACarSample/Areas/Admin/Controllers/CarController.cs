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

        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            CarCreateViewModel model = new CarCreateViewModel();
            model.Brands = new SelectList(_databaseContext.Brands.ToList(),
                    nameof(Brand.Id), nameof(Brand.Name));

            return View(model);
        }

        // POST: Car/Create
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

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            Car car = _databaseContext.Cars.Find(id);

            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CarEditViewModel model = new CarEditViewModel();
            model.Plate = car.Plate;
            model.DailyPrice = car.DailyPrice;
            model.IsAvailable = car.IsAvailable;
            model.BrandId = car.BrandId;
            model.SubBrandId = car.SubBrandId;

            model.Brands = new SelectList(_databaseContext.Brands.ToList(),
                    nameof(Brand.Id), nameof(Brand.Name));

            model.SubBrands = new SelectList(_databaseContext.SubBrands.Where(x => x.BrandId == car.BrandId).ToList(),
                    nameof(SubBrand.Id), nameof(SubBrand.Name));

            return View("Edit2", model);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CarEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = _databaseContext.Cars.Find(id);

                if (car != null)
                {
                    car.Plate = model.Plate;
                    car.DailyPrice = model.DailyPrice;
                    car.IsAvailable = model.IsAvailable;
                    car.BrandId = model.BrandId;
                    car.SubBrandId = model.SubBrandId;

                    _databaseContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            model.Brands = new SelectList(_databaseContext.Brands.ToList(),
                    nameof(Brand.Id), nameof(Brand.Name));

            model.SubBrands = new SelectList(_databaseContext.SubBrands.Where(x => x.BrandId == model.BrandId).ToList(),
                    nameof(SubBrand.Id), nameof(SubBrand.Name));

            return View("Edit2", model);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            Car car = _databaseContext.Cars.Find(id);

            _databaseContext.Cars.Remove(car);
            //_databaseContext.Remove(car);
            _databaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // POST: Car/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
