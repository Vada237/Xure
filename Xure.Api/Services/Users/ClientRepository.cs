﻿using System.Linq;
using Xure.Data;
using Xure.Api.Interfaces;

namespace Xure.Api.Services
{
    public class ClientRepository : Repository<Clients>,IClientRepository
    {

        public ClientRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public Clients Get(int id)
        {
            return GetAll().First(x => x.Id == id);
        }
    }
}
