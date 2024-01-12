using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.DataAccess.Repository.IRepository
{
    /// <summary>
    /// Функционал интерфейса применим к разным моделям/сущностям
    /// для реализации операций CrUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T: class
    {
        // T - Category

        IEnumerable<T> GetAll(string? includeCategoryProperties = null, string? includeBrandProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeCategoryProperties = null, string? includeBrandProperties = null);
        
        // Методы для создания и удаления сущностей
        void Add(T entity);
        void Remove(T entity);
        // Метод, позволяющий удалять несколько сущностей за один вызов
        void RemoveRange(IEnumerable<T> entities);
    }
}
