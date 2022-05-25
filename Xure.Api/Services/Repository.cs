using System;
using System.Collections.Generic;
using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Xure.Api.Services
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {

        private readonly AppDbContext _context;        
        public Repository(AppDbContext context)
        {
            this._context = context;   
        }
        public void Create(T item)
        {
            if (item == null)
            {                
                throw new ArgumentNullException($"{nameof(Create)} сущность не должна быть равна null");
            }

            try
            {
                _context.Set<T>().Add(item);
                Save();
            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(item)} не может быть создан: {ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                T item = _context.Set<T>().Find(id);                
                _context.Set<T>().Remove(item);
                Save();
            }
            catch 
            {
                throw new Exception($"{nameof(id)}: Не найдена сущность");
            }
        }

        public void Delete(long id)
        {
            try
            {
                T item = _context.Set<T>().Find(id);
                _context.Set<T>().Remove(item);
                Save();
            }
            catch
            {
                throw new Exception($"{nameof(id)}: Не найдена сущность");
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>();
            }
            catch(Exception ex)
            {
                throw new Exception($"не найдены сущности: {ex.Message}");
            }        
        }
        
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось сохранить контекст: {ex.Message}");
            }
        }

        public void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException($"{nameof(item)} не найдена");
            }

            try
            {
                _context.Update(item);
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(item)} не может быть обновлен: {ex.Message}");
            }
        }

        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<T> GetWithInclude(Func<T,bool> predicate, params Expression<Func<T,object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<T> Include(params Expression<Func<T,object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties));
        }
    }
}
