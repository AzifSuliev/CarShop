using CarShop.DataAccess.Data;
using CarShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u => u.Brand);
        }
        public void Add(T entity)
        {
            // Здесь dbSet соответствует выражению _db.Categories
            // То есть  dbSet.Add(entity) == _db.Categories.Add(entity)
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeCategoryProperties = null, string? includeBrandProperties = null, 
            bool tracked = false, string? includeProperties = null)
        {
            IQueryable<T> query;
            if (tracked) query = dbSet;
            else query = dbSet.AsNoTracking();
            query = query.Where(filter);
            // Проверка на null
            if (!string.IsNullOrEmpty(includeCategoryProperties) && !string.IsNullOrEmpty(includeBrandProperties))
            {
                foreach (var includeProp in includeCategoryProperties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

                foreach (var includeProp in includeBrandProperties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeCategoryProperties = null, string? includeBrandProperties = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // Проверка на null
            if (!string.IsNullOrEmpty(includeCategoryProperties) && !string.IsNullOrEmpty(includeBrandProperties))
            {
                foreach (var includeProp in includeCategoryProperties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

                foreach (var includeProp in includeBrandProperties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            // Здесь dbSet соответствует выражению _db.Categories
            // То есть  dbSet.Remove(entity) == _db.Categories.Remove(entity)
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            // Здесь dbSet соответствует выражению _db.Categories
            // То есть  dbSet.RemoveRange(entity) == _db.Categories.RemoveRange(entity)
            dbSet.RemoveRange(entities);
        }
    }
}
