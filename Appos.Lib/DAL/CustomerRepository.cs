using Appos.Lib.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Appos.Lib.DAL
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public override IEnumerable<Customer> GetAll()
        {
            return SqlMapper.Query<Customer>(_dbConnection, "SELECT Id, Name, Phone, Email FROM Customer WHERE 1");
        }

        public override Customer? Get(int id)
        {
            return SqlMapper.Query<Customer>(_dbConnection, "SELECT * FROM Customer WHERE Id = @Id", new { Id = id }).FirstOrDefault();
        }
        public override int Add(Customer customer)
        {
            return SqlMapper.ExecuteScalar<int>(_dbConnection, "INSERT INTO Customer (Name, Email, Phone) VALUES (@Name,@Email,@Phone);select LAST_INSERT_ID();", customer);
        }

        public override Customer Update(Customer customer)
        {
            SqlMapper.Execute(_dbConnection, @"UPDATE Customer SET Name = @Name, Email = @Email, Phone = @Phone WHERE id = @Id", customer);

            return Get(customer.Id);
        }

        public override void Delete(Customer customer)
        {
            SqlMapper.Execute(_dbConnection, @"DELETE FROM Customer WHERE id = @Id LIMIT 1", customer);
        }
    }
}
