using API.BLL.Entities;
using API.BLL.Interfaces;
using API.BLL.Specifications;
using API.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public class GenricReposatory<T> : IGenricReposatory<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenricReposatory(StoreContext context)
        {
            this.context = context;
        }

        public void Add(T Entity)
        => context.Set<T>().Add(Entity);

        public async Task<int> CountAsync(ISpecifications<T> specifications)
         => await ApplySpecification(specifications).CountAsync();

        public void Delete(T Entity)
         => context.Set<T>().Remove(Entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        => await context.Set<T>().FindAsync(id);

        public async Task<T> GetEntityWithSpecifcation(ISpecifications<T> specifications)
            => await ApplySpecification(specifications).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> listwithspecificationasync(ISpecifications<T> specifications)
                => await ApplySpecification(specifications).ToListAsync();

        public void Update(T Entity)
        => context.Set<T>().Update(Entity);

        private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
          => SpcifcationEvaluator<T>.GetQuery(context.Set<T>(),specifications);
    }
}
