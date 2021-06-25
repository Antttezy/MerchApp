using Domain.Core.Models;
using Domain.Services.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UserRepositoryService.Services
{
    public class ShopDbRepository : IRepository<Shop>
    {
        public ShopDbRepository(UserContext context)
        {
            Context = context;
        }

        public UserContext Context { get; }

        public void Add(Shop item)
        {
            Context.Shops.Add(item);
            Context.SaveChanges();
        }

        public IQueryable<Shop> All()
        {
            return Context.Shops.AsNoTracking();
        }

        public Shop Get(int id)
        {
            return Context.Shops.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Shop item)
        {
            var s = Context.Shops.FirstOrDefault(x => x.Id == item.Id);
            Context.Shops.Remove(s);
            Context.SaveChanges();
        }

        public void Update(Shop item)
        {
            Context.Shops.Update(item);
            Context.SaveChanges();
        }
    }
}
