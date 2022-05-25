using System;
using System.Collections.Generic;
using System.Linq;

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
