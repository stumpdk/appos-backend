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
    public class CustomerLogRepository : GenericRepository<CustomerLog>
    {
        readonly IDbConnection _dbConnection;

        public CustomerLogRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public override IEnumerable<CustomerLog> GetAll()
        {
            return SqlMapper.Query<CustomerLog>(_dbConnection, "SELECT * FROM CustomerLog");
        }

        public override CustomerLog Get(int id)
        {
            var list = SqlMapper.Query<CustomerLog>(_dbConnection, "SELECT * FROM CustomerLog WHERE Id = @Id", new { Id = id });
            return list.First();
        }
        public override int Add(CustomerLog customerLog)
        {
            return SqlMapper.ExecuteScalar<int>(_dbConnection, "INSERT INTO CustomerLog (Event, Details, Created, CustomerId) VALUES (@Event,@Details, NOW(), @CustomerId);select LAST_INSERT_ID();", customerLog);
        }

        public override CustomerLog Update(CustomerLog archiveversion)
        {
            throw new NotImplementedException("Update is not implemented for CustomerLog");
        }

        public override void Delete(CustomerLog entity)
        {
            throw new NotImplementedException("Deletion is not implemented for CustomerLog");
        }
    }
}
