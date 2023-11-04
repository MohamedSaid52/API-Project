using API.BLL.Entities;
using API.BLL.Interfaces;
using API.DAL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        Hashtable _reposatories = new Hashtable();

        public UnitOfWork(StoreContext context)
        {
            this.context = context;
        }

        public Task<int> complete()
        =>context.SaveChangesAsync();

        public void Dispose()
        =>context.Dispose();

        public IGenricReposatory<TEntity> GenricReposatory<TEntity>() where TEntity : BaseEntity
        {
            if(_reposatories is null)
                _reposatories = new Hashtable();
            var type=typeof(TEntity).Name;
            if (!_reposatories.ContainsKey(type))
            {
                var reposatorytype = typeof(GenricReposatory<>);
                var reposatoryinstance=Activator.CreateInstance(reposatorytype.MakeGenericType(typeof(TEntity)),context);
                _reposatories.Add(type,reposatoryinstance);
            }
            return (IGenricReposatory<TEntity>)_reposatories[type];
        }
    }
}
