using Xure.Data;
using System.Linq;
using System.Collections.Generic;
using Xure.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;

namespace Xure.Api.Services
{
    public class SellerRepository : Repository<Sellers>,ISellerRepository
    {
        AppDbContext appDbContext;
        UserManager<AppUser> UserManager;
        public SellerRepository(AppDbContext appDbContext,UserManager<AppUser> userManager) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
            this.UserManager = userManager;
        }

        public Company FindCompanyByEmail(string email)
        {
            var query = appDbContext.Sellers.Include(c => c.Company)
                .Include(c => c.UserInfo).Where(c => c.UserInfo.Email == email).Select(c => new Company {
                    Id = c.Company.Id,
                    Name = c.Company.Name,
                    Description = c.Company.Description,
                    DateRegistration = c.Company.DateRegistration,
                    INN = c.Company.INN,
                    OGRN = c.Company.OGRN
                }).FirstOrDefault();

            return query;
        }

        public Company FindCompanyByPhone(string phone)
        {
            var query = appDbContext.Sellers.Include(c => c.Company)
                .Include(c => c.UserInfo).Where(c => c.UserInfo.PhoneNumber == phone).Select(c => new Company
                {
                    Id = c.Company.Id,
                    Name = c.Company.Name,
                    Description = c.Company.Description,
                    DateRegistration = c.Company.DateRegistration,
                    INN = c.Company.INN,
                    OGRN = c.Company.OGRN
                }).FirstOrDefault();

            return query;
        }

        public Company FindCompanyByNameAndSurname(string Name,string Surname)
        {
            var query = appDbContext.Sellers.Include(c => c.Company)
                .Include(c => c.UserInfo).Where(c => c.UserInfo.UserName == Name && c.UserInfo.Surname == Surname).Select(c => new Company
                {
                    Id = c.Company.Id,
                    Name = c.Company.Name,
                    Description = c.Company.Description,
                    DateRegistration = c.Company.DateRegistration,
                    INN = c.Company.INN,
                    OGRN = c.Company.OGRN
                }).FirstOrDefault();

            return query;
        }

        public IEnumerable<Sellers> GetSellers()
        {
            return appDbContext.Sellers;
        }

        public IEnumerable<Sellers> GetSellersWithInclude()
        {
            return appDbContext.Sellers.Include(c => c.UserInfo).Include(c => c.Company);
        }        
    }
}
