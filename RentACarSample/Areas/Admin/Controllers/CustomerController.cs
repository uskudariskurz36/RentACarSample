using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarSample.Areas.Admin.Models;
using RentACarSample.Entities;

namespace RentACarSample.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private DatabaseContext _databaseContext;

        public CustomerController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET:Customer
        public ActionResult Index()
        {
            return View(_databaseContext.Customers.ToList());
        }

        // GET:Customer/Details/5
        public ActionResult Details(int id)
        {
            Customer? customer = _databaseContext.Customers.Find(id);

            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET:Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Customers.Any(x => x.TCKN == model.TCKN))
                {
                    ModelState.AddModelError(nameof(model.TCKN), "İlgili TCKN sistemde kayıtlıdır.");
                    return View(model);
                }

                Customer customer = new Customer
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Description = model.Description,
                    IsBlacklist = model.IsBlacklist,
                    TCKN = model.TCKN,
                    Phone = model.Phone,
                    Email = model.Email,
                };

                _databaseContext.Customers.Add(customer);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET:Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer? customer = _databaseContext.Customers.Find(id);

            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CustomerEditViewModel model = new CustomerEditViewModel
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Description = customer.Description,
                IsBlacklist = customer.IsBlacklist,
                TCKN = customer.TCKN,
                Phone = customer.Phone,
                Email = customer.Email,
            };

            return View(model);
        }

        // POST:Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Customers.Any(x => x.TCKN == model.TCKN && x.Id != id))
                {
                    ModelState.AddModelError(nameof(model.TCKN), "İlgili TCKN sistemde kayıtlıdır.");
                    return View(model);
                }

                Customer? customer = _databaseContext.Customers.Find(id);

                if (customer == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                customer.Name = model.Name;
                customer.Surname = model.Surname;
                customer.Description = model.Description;
                customer.IsBlacklist = model.IsBlacklist;
                customer.TCKN = model.TCKN;
                customer.Phone = model.Phone;
                customer.Email = model.Email;

                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET:Customer/Delete/5
        public ActionResult Delete(int id)
        {
            Customer? customer = _databaseContext.Customers.Find(id);

            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // POST:Customer/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Customer? customer = _databaseContext.Customers.Find(id);

            if (customer != null)
            {
                _databaseContext.Customers.Remove(customer);
                _databaseContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
