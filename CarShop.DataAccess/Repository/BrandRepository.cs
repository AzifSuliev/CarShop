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
    public class BrandRepository : Repository<Brand>,  IBrandRepository
    {
        private ApplicationDbContext _db;

        public BrandRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(Brand entity)
        {
            _db.Brands.Update(entity);
        }
    }
}
