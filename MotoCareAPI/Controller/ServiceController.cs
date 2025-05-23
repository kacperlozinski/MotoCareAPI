using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private static readonly List<Service> _services = new();
        private static int _nextId = 1;

        private static ServiceDto ToDto(Service service)
        {
            return new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                LastPriceUpdate = service.LastPriceUpdate,
                ServiceCategoryId = service.ServiceCategoryId
            };
        }

        private static Service ToEntity(ServiceDto dto)
        {
            return new Service
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                LastPriceUpdate = dto.LastPriceUpdate,
                ServiceCategoryId = dto.ServiceCategoryId
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServiceDto>> GetServices()
        {
            return Ok(_services.Select(ToDto));
        }

        [HttpGet("{id}")]
        public ActionResult<ServiceDto> GetService(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();
            return Ok(ToDto(service));
        }

        [HttpPost]
        public ActionResult<ServiceDto> CreateService([FromBody] ServiceDto serviceDto)
        {
            var service = ToEntity(serviceDto);
            service.Id = _nextId++;
            _services.Add(service);
            var createdDto = ToDto(service);
            return CreatedAtAction(nameof(GetService), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, [FromBody] ServiceDto serviceDto)
        {
            if (id != serviceDto.Id)
                return BadRequest();

            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();

            service.Name = serviceDto.Name;
            service.Description = serviceDto.Description;
            service.Price = serviceDto.Price;
            service.LastPriceUpdate = serviceDto.LastPriceUpdate;
            service.ServiceCategoryId = serviceDto.ServiceCategoryId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();

            _services.Remove(service);
            return NoContent();
        }
    }
}
