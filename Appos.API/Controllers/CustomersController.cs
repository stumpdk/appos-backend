using Appos.Lib.DAL;
using Appos.Lib.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerUnitOfWork customerUnitOfWork;

        public CustomersController(ICustomerUnitOfWork customerUnitOfWork)
        {
            this.customerUnitOfWork = customerUnitOfWork;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return customerUnitOfWork.CustomerRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return customerUnitOfWork.CustomerRepository.Get(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public StatusCodeResult Post([FromBody] Customer customer)
        {
            //TODO: Validate input
            try
            {
                var newId = customerUnitOfWork.CustomerRepository.Add(customer);
                customerUnitOfWork.CustomerLogRepository.Add(new CustomerLog() { Event = "Add", Details = customer.ToString(), Created = DateTime.Now, CustomerId = newId });
                customerUnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                customerUnitOfWork.Rollback();
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public StatusCodeResult Put(int id, [FromBody] Customer customer)
        {
            //TODO: Validate input
            //TODO: Validate/use id
            var customerToUpdate = customerUnitOfWork.CustomerRepository.Get(customer.Id);
            if (customerToUpdate == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            try
            {
                customerUnitOfWork.CustomerRepository.Update(customer);
                customerUnitOfWork.CustomerLogRepository.Add(new CustomerLog() { Event = "Update", Details = customer.ToString(), Created = DateTime.Now, CustomerId = id });
                customerUnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                customerUnitOfWork.Rollback();
            }

            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var customer = customerUnitOfWork.CustomerRepository.Get(id);

            //TODO: Validate if customer exists

            try
            {
                customerUnitOfWork.CustomerRepository.Delete(customer);
                customerUnitOfWork.CustomerLogRepository.Add(new CustomerLog() { Event = "Delete", Details = customer.ToString(), Created = DateTime.Now, CustomerId = id });
                customerUnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                customerUnitOfWork.Rollback();
            }
        }
    }
}
