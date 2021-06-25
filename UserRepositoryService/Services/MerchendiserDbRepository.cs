using Domain.Core.Models;
using Domain.Services.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UserRepositoryService.Services
{
    public class MerchendiserDbRepository : IRepository<Merchendiser>
    {
        private readonly UserContext userContext;

        public MerchendiserDbRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public void Add(Merchendiser item)
        {
            userContext.Merchendisers.Add(item);
            userContext.SaveChanges();
        }

        public IQueryable<Merchendiser> All()
        {
            return userContext.Merchendisers.Include(m => m.CurrentShift).AsNoTracking();
        }

        public Merchendiser Get(int id)
        {
            return userContext.Merchendisers.Include(m => m.CurrentShift).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public void Remove(Merchendiser item)
        {
            var m = userContext.Merchendisers.FirstOrDefault(a => a.Id == item.Id);
            userContext.Merchendisers.Remove(m);
            userContext.SaveChanges();
        }

        public void Update(Merchendiser item)
        {
            userContext.Merchendisers.Update(item);
            userContext.SaveChanges();
        }
    }
}
