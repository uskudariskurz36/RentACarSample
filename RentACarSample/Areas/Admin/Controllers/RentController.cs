using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACarSample.Areas.Admin.Models;
using RentACarSample.Entities;

namespace RentACarSample.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentController : Controller
    {
        private DatabaseContext _databaseContext;

        public RentController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET : /Admin/Rent/GetCreatePartial
        public IActionResult GetCreatePartial()
        {
            RentCreateViewModel model = new RentCreateViewModel();
            model.ReceivedDate = DateTime.Now;
            model.Cars = new SelectList(_databaseContext.Cars.ToList(), nameof(Car.Id), nameof(Car.Plate));
            model.Customers = new SelectList(_databaseContext.Customers.ToList(), nameof(Customer.Id), nameof(Customer.FullName));

            return PartialView("_CreateRentPartial", model);
        }

        // GET : /Admin/Rent/GetPriceData?carId=1
        public IActionResult GetPriceData(int carId)
        {
            Car car = _databaseContext.Cars.Find(carId);
            return Json(car.DailyPrice);
        }

        [HttpPost]
        public IActionResult Create(RentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Rent rent = new Rent
                {
                    CarId = model.CarId,
                    CustomerId = model.CustomerId,
                    Price = model.Price,
                    KM = model.KM,
                    ReceivedDate = model.ReceivedDate,
                    Description = model.Description,
                    Received = false,
                    RentDate = DateTime.Now
                };

                _databaseContext.Rents.Add(rent);
                _databaseContext.SaveChanges();

                CloseModalPartialViewModel partialModel = new CloseModalPartialViewModel
                {
                    ModalId = "modalCreate",
                    ShowToastr = true,
                    ToastrType = "success",
                    ToastrTitle = "Kiralama Yapıldı",
                    ToastrMessage = "Araç kiralama kaydı başarıyla yapılmıştır."
                };

                return PartialView("_CloseModalPartial", partialModel);
            }

            model.Cars = new SelectList(_databaseContext.Cars.ToList(), nameof(Car.Id), nameof(Car.Plate));
            model.Customers = new SelectList(_databaseContext.Customers.ToList(), nameof(Customer.Id), nameof(Customer.FullName));

            return PartialView("_CreateRentPartial", model);
        }
    }
}
