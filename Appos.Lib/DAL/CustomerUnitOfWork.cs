using Appos.Lib.Models;
using Appos.Lib.DAL;
using System;
using System.Data;

namespace Appos.Lib.DAL
{
    public class CustomerUnitOfWork : ICustomerUnitOfWork, IDisposable
    {
        public IGenericRepository<Customer>? CustomerRepository { get; }
        public IGenericRepository<CustomerLog>? CustomerLogRepository { get; }

        readonly IDbTransaction _dbTransaction;

        readonly IDbConnection _dbConnection;

        public CustomerUnitOfWork(IDbConnection dbConnection, IGenericRepository<Customer> customerRepository, IGenericRepository<CustomerLog> customerLogRepository)
        {
            CustomerRepository = customerRepository;
            CustomerLogRepository = customerLogRepository;
            _dbConnection = dbConnection;
            dbConnection.Open();
            _dbTransaction = dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }
    }
}
