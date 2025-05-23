using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceCategoryController : ControllerBase
    {
        private static readonly List<ServiceCategory> _categories = new();
        private static int _nextId = 1;

        private static ServiceCategoryDto ToDto(ServiceCategory category)
        {
            return new ServiceCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Priority = category.Priority,
                AvailableDiscount = category.AvailableDiscount
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
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServiceCategoryDto>> GetServiceCategories()
        {
            return Ok(_categories.Select(ToDto));
        }

        [HttpGet("{id}")]
        public ActionResult<ServiceCategoryDto> GetServiceCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();
            return Ok(ToDto(category));
        }

        [HttpPost]
        public ActionResult<ServiceCategoryDto> CreateServiceCategory([FromBody] ServiceCategoryDto categoryDto)
        {
            var category = ToEntity(categoryDto);
            category.Id = _nextId++;
            _categories.Add(category);
            var createdDto = ToDto(category);
            return CreatedAtAction(nameof(GetServiceCategory), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateServiceCategory(int id, [FromBody] ServiceCategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest();

            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.Priority = categoryDto.Priority;
            category.AvailableDiscount = categoryDto.AvailableDiscount;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServiceCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            _categories.Remove(category);
            return NoContent();
        }
    }
}