using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Infrastructure.ReadModel
{
    public class Repository<T> : IRepository<T> where T : class, IReadEntity
    {
        private readonly ReadModelContext _context;

        public Repository(ReadModelContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression = null)
        {
            var entities = await this._context.Set<T>()
                //.Where(expression)
                .AsNoTracking()
                .ToListAsync();

            return entities.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await this._context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public void InsertEntity(T entity)
        {
            this._context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            this._context.Entry<T>(entity).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
