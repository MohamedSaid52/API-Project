using API.BLL.Entities;
using API.BLL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Interfaces
{
    public interface IGenricReposatory<T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetEntityWithSpecifcation(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> listwithspecificationasync(ISpecifications<T> specifications);
        Task<int> CountAsync(ISpecifications<T> specifications);
        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
