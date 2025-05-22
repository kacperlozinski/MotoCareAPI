using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using Microsoft.EntityFrameworkCore;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly MotoCareDbContext _context;

        public ServiceCategoryController(MotoCareDbContext context)
        {
            _context = context;
        }

        
        private static ServiceCategoryDto ToDto(ServiceCategory category)
        {
            return new ServiceCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Priority = category.Priority,
                AvailableDiscount = category.AvailableDiscount
                // Add mapping for Services if needed in DTO
            };
        }

        
        private static ServiceCategory ToEntity(ServiceCategoryDto dto)
        {
            return new ServiceCategory
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Priority = dto.Priority,
                AvailableDiscount = dto.AvailableDiscount
                // Add mapping for Services if needed in DTO
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCategoryDto>>> GetServiceCategories()
        {
            var categories = await _context.ServiceCategories.ToListAsync();
            return Ok(categories.Select(ToDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCategoryDto>> GetServiceCategory(int id)
        {
            var category = await _context.ServiceCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return NotFound();
            return Ok(ToDto(category));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceCategoryDto>> CreateServiceCategory(ServiceCategoryDto categoryDto)
        {
            var category = ToEntity(categoryDto);
            _context.ServiceCategories.Add(category);
            await _context.SaveChangesAsync();
            var createdDto = ToDto(category);
            return CreatedAtAction(nameof(GetServiceCategory), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceCategory(int id, ServiceCategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest();

            var category = await _context.ServiceCategories.FindAsync(id);
            if (category == null)
                return NotFound();

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.Priority = categoryDto.Priority;
            category.AvailableDiscount = categoryDto.AvailableDiscount;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceCategory(int id)
        {
            var category = await _context.ServiceCategories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.ServiceCategories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
