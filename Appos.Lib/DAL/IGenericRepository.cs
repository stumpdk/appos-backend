using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appos.Lib.DAL
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int Id);
        int Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
    }
}
