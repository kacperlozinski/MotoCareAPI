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
            service.Id = _services.Any() ? _services.Max(s => s.Id) + 1: 1;
            _services.Add(service);
            var createdDto = ToDto(service);
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, [FromBody] ServiceDto serviceDto)
        {
            var index = _services.FindIndex(a => a.Id == id);

            if (index == -1)
                return NotFound();

            var updated = ToEntity(serviceDto);
            updated.Id = id;

            _services[index] = updated;

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

        private static ServiceDto ToDto(Service service)
        {
            return new ServiceDto
            {
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
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                LastPriceUpdate = dto.LastPriceUpdate,
                ServiceCategoryId = dto.ServiceCategoryId
            };
        }
    }
}
