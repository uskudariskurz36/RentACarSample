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

        // GET : /Admin/Rent/GetRentReceivePartial?rentId=guid
        public IActionResult GetRentReceivePartial(Guid rentId)
        {
            Rent rent = _databaseContext.Rents.Include(x => x.Car).Include(x => x.Customer).FirstOrDefault(x => x.Id == rentId);

            return PartialView("_RentReceivePartial", rent);
        }

        // POST : /Admin/Rent/Receive/guid
        [HttpPost]
        public IActionResult Receive(Guid id)
        {
            Rent rent = _databaseContext.Rents.Find(id);

            CloseModalPartialViewModel partialModel = new CloseModalPartialViewModel();
            partialModel.ModalId = "modalReceive";
            partialModel.ShowToastr = true;

            if (rent != null)
            {
                rent.Received = true;
                _databaseContext.SaveChanges();

                partialModel.ToastrType = "success";
                partialModel.ToastrTitle = "Araç Teslim";
                partialModel.ToastrMessage = "Araç teslim alınmıştır.";
            }
            else
            {
                partialModel.ToastrType = "warning";
                partialModel.ToastrTitle = "Uyarı";
                partialModel.ToastrMessage = "Kiralama verisi bulunamadı.";
            }

            return PartialView("_CloseModalPartial", partialModel);
        }

        // GET : /Admin/Rent/GetRentListPartial
        public IActionResult GetRentListPartial(string filter = "none")
        {
            List<Rent> rents = null;

            if (filter == "received")
            {
                rents = _databaseContext.Rents.Where(x => x.Received).Include(x => x.Car).Include(x => x.Customer).ToList();
            }
            else if (filter == "nonreceived")
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

        // GET : /Admin/Rent/GetDescData?rentId=1
        public IActionResult GetDescData(Guid rentId)
        {
            return Json(_databaseContext.Rents.Find(rentId).Description);
        }

        [HttpPost]
        public IActionResult Create(RentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ReceivedDate < DateTime.Now)
                {
                    ModelState.AddModelError(nameof(model.ReceivedDate), $"Teslim tarihi {DateTime.Now.ToString("g")} tarihinden eski olamaz!");
                }
                else
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
            }

            // Şu an kiralanmamı ve is available=true olan araçlar seçilebilmeli.
            List<Car> cars = _databaseContext.Cars
                .Where(car => !car.Rents.Any(rent => rent.CarId == car.Id && !rent.Received) && car.IsAvailable).ToList();

            model.Cars = new SelectList(cars, nameof(Car.Id), nameof(Car.Plate));
            model.Customers = new SelectList(_databaseContext.Customers.ToList(), nameof(Customer.Id), nameof(Customer.FullName));

            return PartialView("_CreateRentPartial", model);
        }
    }
}
