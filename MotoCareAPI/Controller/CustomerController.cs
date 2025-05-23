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
        private static int _nextId = 1;

        private static CustomerDto ToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
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
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Note = dto.Note
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        {
            return Ok(_customers.Select(ToDto));
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(ToDto(customer));
        }

        [HttpPost]
        public ActionResult<CustomerDto> CreateCustomer([FromBody] CustomerDto customerDto)
        {

            var customer = ToEntity(customerDto);
            customer.Id = _nextId++;
            _customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, ToDto(customer));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.Id)
                return BadRequest();

            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();


            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.PhoneNumber = customerDto.PhoneNumber;
            customer.Email = customerDto.Email;
            customer.Note = customerDto.Note;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            _customers.Remove(customer);
            return NoContent();
        }
    }
}
