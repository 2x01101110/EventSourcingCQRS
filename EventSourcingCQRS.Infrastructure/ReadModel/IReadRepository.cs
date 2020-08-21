using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Infrastructure.ReadModel
{
    public interface IReadRepository<T> where T : IReadEntity
    {
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression = null);

        Task<T> GetByIdAsync(Guid id);
    }
}
