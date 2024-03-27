using CarShop.DataAccess.Data;
using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(Product entity)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == entity.Id);
            if (objFromDb != null)
            {
                objFromDb.CarName = entity.CarName;
                objFromDb.Description = entity.Description;
                objFromDb.basicEquipmentPrice = entity.basicEquipmentPrice;
                objFromDb.fullEquipmentPrice = entity.fullEquipmentPrice;
                objFromDb.CategoryId = entity.CategoryId;
                objFromDb.BrandId = entity.BrandId;
                //if(objFromDb.ImageURL != null)
                //{
                //    objFromDb.ImageURL = entity.ImageURL;
                //}
            }
        }
    }
}
