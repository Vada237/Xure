using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Xure.Api.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();                     
        void Update(T item);
        void Create(T item);
        void Delete(int id);
        void Save();        
    }    
}
