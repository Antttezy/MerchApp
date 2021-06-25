using System.Linq;

namespace Domain.Services.Interfaces
{
    public interface IRepository<T>
    {
        public IQueryable<T> All();

        public T Get(int id);

        public void Remove(T item);

        public void Add(T item);

        public void Update(T item);
    }
}
