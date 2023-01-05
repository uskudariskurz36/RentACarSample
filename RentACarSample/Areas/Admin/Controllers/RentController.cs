using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        // GET : /Admin/Rent/GetRentListPartial
        public IActionResult GetRentListPartial(string filter = "none")
        {
            List<Rent> rents = null;

            if (filter == "received")
            {
                rents = _databaseContext.Rents.Where(x => x.Received).Include(x => x.Car).Include(x => x.Customer).ToList();
            }
            else if(filter == "nonreceived")
            {
                rents = _databaseContext.Rents.Where(x => !x.Received).Include(x => x.Car).Include(x => x.Customer).ToList();
            }
            else
            {
                rents = _databaseContext.Rents.Include(x => x.Car).Include(x => x.Customer).ToList();
            }

            return base.PartialView("_RentListPartial", rents);
        }

        // GET : /Admin/Rent/GetCreatePartial
        public IActionResult GetCreatePartial()
        {
            RentCreateViewModel model = new RentCreateViewModel();
            model.ReceivedDate = DateTime.Now.Date;
            
            // Şu an kiralanmamı ve is available=true olan araçlar seçilebilmeli.
            List<Car> cars = _databaseContext.Cars
                .Where(car => !car.Rents.Any(rent => rent.CarId == car.Id && !rent.Received) && car.IsAvailable).ToList();


            model.Cars = new SelectList(cars, nameof(Car.Id), nameof(Car.Plate));
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
