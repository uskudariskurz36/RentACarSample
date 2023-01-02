using RentACarSample.Areas.Admin.Models;
using RentACarSample.Entities;

namespace RentACarSample.Managers
{
    public interface IBrandManager
    {
        Brand Create(BrandCreateViewModel model);
        Brand? GetById(int id);
        bool IsNameExists(string name);
        bool IsNameExistsBySelfOnly(int id, string name);
        List<Brand> List();
        void Remove(Brand brand);
        void Update(int id, BrandEditViewModel model);
    }

    public class BrandManager : IBrandManager
    {
        private DatabaseContext _databaseContext;

        public BrandManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Brand> List()
        {
            return _databaseContext.Brands.ToList();
        }

        public bool IsNameExists(string name)
        {
            return _databaseContext.Brands.Any(x => x.Name.ToLower() == name.ToLower());
        }

        public Brand Create(BrandCreateViewModel model)
        {
            Brand brand = new Brand();
            brand.Name = model.Name;
            brand.Description = model.Description;
            brand.Hidden = model.Hidden;

            _databaseContext.Brands.Add(brand);
            _databaseContext.SaveChanges();

            return brand;
        }

        public Brand? GetById(int id)
        {
            return _databaseContext.Brands.Find(id);
        }

        public bool IsNameExistsBySelfOnly(int id, string name)
        {
            return _databaseContext.Brands.Any(x => x.Name.ToLower() == name.ToLower() && x.Id != id);
        }

        public void Update(int id, BrandEditViewModel model)
        {
            Brand brand = GetById(id);

            brand.Name = model.Name;
            brand.Description = model.Description;
            brand.Hidden = model.Hidden;

            _databaseContext.SaveChanges();
        }

        public void Remove(Brand brand)
        {
            _databaseContext.Brands.Remove(brand);
            _databaseContext.SaveChanges();
        }
    }
}
