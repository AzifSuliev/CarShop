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
            _db.Products.Update(entity);
        }
    }
}
