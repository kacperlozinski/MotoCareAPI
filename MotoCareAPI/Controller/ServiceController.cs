using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using Microsoft.EntityFrameworkCore;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly MotoCareDbContext _context;

        public ServiceController(MotoCareDbContext context)
        {
            _context = context;
        }

        
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
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await _context.Services.Include(s => s.Category).ToListAsync();
            return Ok(services.Select(ToDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _context.Services.Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
                return NotFound();
            return Ok(ToDto(service));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto>> CreateService(ServiceDto serviceDto)
        {
            var service = ToEntity(serviceDto);
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            var createdDto = ToDto(service);
            return CreatedAtAction(nameof(GetService), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, ServiceDto serviceDto)
        {
            if (id != serviceDto.Id)
                return BadRequest();

            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            service.Name = serviceDto.Name;
            service.Description = serviceDto.Description;
            service.Price = serviceDto.Price;
            service.LastPriceUpdate = serviceDto.LastPriceUpdate;
            service.ServiceCategoryId = serviceDto.ServiceCategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
