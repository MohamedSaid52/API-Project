using API.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenricReposatory<TEntity> GenricReposatory<TEntity>() where TEntity : BaseEntity;
        Task<int> complete();
    }
}
