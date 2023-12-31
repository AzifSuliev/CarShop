using CarShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.DataAccess.Repository.IRepository
{
    public interface IBrandRepository: IRepository<Brand>
    {
        void Update(Brand entity);
    }
}
