using Appos.Lib.DAL;
using Appos.Lib.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLogsController : ControllerBase
    {
        private readonly ICustomerUnitOfWork customerUnitOfWork;

        public CustomerLogsController(ICustomerUnitOfWork customerUnitOfWork)
        {
            this.customerUnitOfWork = customerUnitOfWork;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerLog>), StatusCodes.Status200OK)]
        public IResult Get()
        {
            return Results.Ok(customerUnitOfWork.CustomerRepository.GetAll());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<CustomerLog>), StatusCodes.Status200OK)]
        public IResult GetByCustomerId(int id)
        {
            return Results.Ok(customerUnitOfWork.CustomerLogRepository.GetAll().Where(cl => cl.CustomerId == id).ToList());
        }
    }
}
