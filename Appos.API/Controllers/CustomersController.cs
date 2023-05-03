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
        public IGenericRepository<Customer> customerRepository;
        public CustomersController(IGenericRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        public IResult Get()
        {
            return Results.Ok(customerRepository.GetAll());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IResult Get(int id)
        {
            return Results.Ok(customerRepository.Get(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        public IResult Post([FromBody] Customer customer)
        {
            //TODO: Validate input
            try
            {
                var newId = customerRepository.Add(customer);
                return Results.Created<Customer>($"/customers/{newId}",customerRepository.Get(newId));
            }
            catch (Exception ex)
            {
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
            var customerToUpdate = customerRepository.Get(customer.Id);
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
                var updatedCustomer = customerRepository.Update(customer);
                return Results.Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                return Results.Problem("Could not update customer: " + ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IResult Delete(int id)
        {
            var customer = customerRepository.Get(id);

            //TODO: Validate if customer exists
            if(customer == null)
            {
                return Results.Problem("Could not delete Customer as it does not exist");
            }

            try
            {
                customerRepository.Delete(customer);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem("Could not delete customer: " + ex.Message);
            }
        }
    }
}
