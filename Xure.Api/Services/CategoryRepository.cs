using Xure.Api.Interfaces;
using Xure.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Xure.Api.Services
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public Category GetById(int id)
        {            
            return GetAll().FirstOrDefault(x => x.Id == id);                        
        }
    }
}
