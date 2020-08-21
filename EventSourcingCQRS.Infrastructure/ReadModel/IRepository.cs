using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Infrastructure.ReadModel
{
    public interface IRepository<T> : IReadRepository<T> where T : IReadEntity
    {
        void InsertEntity(T entity);
        void UpdateEntity(T entity);
        Task SaveAsync();
    }
}
