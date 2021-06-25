using Domain.Core.Models;
using Domain.Services.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UserRepositoryService.Services
{
    public class CoordinatorDbRepository : IRepository<Coordinator>
    {
        private readonly UserContext userContext;

        public CoordinatorDbRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public void Add(Coordinator item)
        {
            userContext.Coordinators.Add(item);
            userContext.SaveChanges();
        }

        public IQueryable<Coordinator> All()
        {
            return userContext.Coordinators.AsNoTracking();
        }

        public Coordinator Get(int id)
        {
            return userContext.Coordinators.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public void Remove(Coordinator item)
        {
            var c = userContext.Coordinators.FirstOrDefault(a => a.Id == item.Id);
            userContext.Coordinators.Remove(c);
            userContext.SaveChanges();
        }

        public void Update(Coordinator item)
        {
            var c = userContext.Coordinators.FirstOrDefault(a => a.Id == item.Id);
            userContext.Coordinators.Update(c);
            userContext.SaveChanges();
        }
    }
}
