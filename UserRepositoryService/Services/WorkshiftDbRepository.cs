using Domain.Core.Models;
using Domain.Services.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UserRepositoryService.Services
{
    public class WorkshiftDbRepository : IRepository<Workshift>
    {
        private readonly UserContext context;

        public WorkshiftDbRepository(UserContext context)
        {
            this.context = context;
        }

        public void Add(Workshift item)
        {
            context.Workshifts.Add(item);
            context.SaveChanges();
        }

        public IQueryable<Workshift> All()
        {
            return context.Workshifts.Include(x => x.Merchendiser).Include(x => x.Shop).AsNoTracking();
        }

        public Workshift Get(int id)
        {
            return context.Workshifts.Include(x => x.Merchendiser).Include(x => x.Shop).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Workshift item)
        {
            var w = context.Workshifts.FirstOrDefault(x => x.Id == item.Id);
            context.Workshifts.Remove(w);
            context.SaveChanges();
        }

        public void Update(Workshift item)
        {
            context.Workshifts.Update(item);
            context.SaveChanges();
        }
    }
}
