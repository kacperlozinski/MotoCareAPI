using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.MotoCareDTO;
using MotoCareAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly List<Customer> _customers = new();

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>List of all customers.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        {
            return Ok(_customers.Select(ToDto));
        }

        /// <summary>
        /// Retrieves a specific customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>Customer data if found; otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(ToDto(customer));
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customerDto">The customer data to create.</param>
        /// <returns>The newly created customer with its ID.</returns>
        [HttpPost]
        public ActionResult<CustomerDto> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var customer = ToEntity(customerDto);
            customer.Id = _customers.Any() ? _customers.Max(s => s.Id) + 1 : 1;
            _customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, ToDto(customer));
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customerDto">The updated customer data.</param>
        /// <returns>NoContent if updated successfully; NotFound if the customer does not exist.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            var index = _customers.FindIndex(a => a.Id == id);

            if (index == -1)
                return NotFound();

            var updated = ToEntity(customerDto);
            updated.Id = id;

            _customers[index] = updated;

            return NoContent();
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>NoContent if deleted; NotFound if the customer does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            _customers.Remove(customer);
            return NoContent();
        }

        private static CustomerDto ToDto(Customer customer)
        {
            return new CustomerDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Note = customer.Note
            };
        }

        private static Customer ToEntity(CustomerDto dto)
        {
            return new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Note = dto.Note
            };
        }
    }
}
