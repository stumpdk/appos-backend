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
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        public IResult Get()
        {
            return Results.Ok(customerUnitOfWork.CustomerRepository.GetAll());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IResult Get(int id)
        {
            return Results.Ok(customerUnitOfWork.CustomerRepository.Get(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        public IResult Post([FromBody] Customer customer)
        {
            //TODO: Validate input
            try
            {
                var newId = customerUnitOfWork.CustomerRepository.Add(customer);
                customerUnitOfWork.CustomerLogRepository.Add(new CustomerLog() { Event = "Add", Details = customer.ToString(), Created = DateTime.Now, CustomerId = newId });
                customerUnitOfWork.Commit();
                return Results.Created<Customer>($"/customers/{newId}",customerUnitOfWork.CustomerRepository.Get(newId));
            }
            catch (Exception ex)
            {
                customerUnitOfWork.Rollback();
                return Results.Problem("Could not add customer: " + ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IResult Put(int id, [FromBody] Customer customer)
        {
            //TODO: Validate input
            //TODO: Validate/use id
            var customerToUpdate = customerUnitOfWork.CustomerRepository.Get(customer.Id);
            if (customerToUpdate == null)
            {
                //TODO: 
                return Results.Problem("problem");
            }

            if (customer.Equals(customerToUpdate))
            {
                return Results.Ok(customerToUpdate);
            }

            try
            {
                var updatedCustomer = customerUnitOfWork.CustomerRepository.Update(customer);
                customerUnitOfWork.CustomerLogRepository.Add(new CustomerLog() { Event = "Update", Details = customer.ToString(), Created = DateTime.Now, CustomerId = id });
                customerUnitOfWork.Commit();
                return Results.Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                customerUnitOfWork.Rollback();
                return Results.Problem("Could not update customer: " + ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IResult Delete(int id)
        {
            var customer = customerUnitOfWork.CustomerRepository.Get(id);

            //TODO: Validate if customer exists

            try
            {
                customerUnitOfWork.CustomerRepository.Delete(customer);
                customerUnitOfWork.CustomerLogRepository.Add(new CustomerLog() { Event = "Delete", Details = customer.ToString(), Created = DateTime.Now, CustomerId = id });
                customerUnitOfWork.Commit();
                return Results.Ok();
            }
            catch (Exception ex)
            {
                customerUnitOfWork.Rollback();
                return Results.Problem("Could not delete customer: " + ex.Message);
            }
        }
    }
}
