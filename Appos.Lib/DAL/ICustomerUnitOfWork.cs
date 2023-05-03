using Appos.Lib.Models;

namespace Appos.Lib.DAL
{
    public interface ICustomerUnitOfWork
    {
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<CustomerLog> CustomerLogRepository { get; }
        void Commit();
        void Dispose();
        void Rollback();
    }
}
