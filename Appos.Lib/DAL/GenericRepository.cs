using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appos.Lib.DAL
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public virtual int Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity? Get(int Id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
