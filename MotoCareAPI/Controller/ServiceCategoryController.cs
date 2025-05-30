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

        /// <summary>
        /// Retrieves all service categories.
        /// </summary>
        /// <returns>List of all service categories.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ServiceCategoryDto>> GetServiceCategories()
        {
            return Ok(_categories.Select(ToDto));
        }

        /// <summary>
        /// Retrieves a specific service category by ID.
        /// </summary>
        /// <param name="id">The ID of the service category.</param>
        /// <returns>The service category if found; otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<ServiceCategoryDto> GetServiceCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            return Ok(ToDto(category));
        }

        /// <summary>
        /// Creates a new service category.
        /// </summary>
        /// <param name="categoryDto">The service category data to create.</param>
        /// <returns>The created service category.</returns>
        [HttpPost]
        public ActionResult<ServiceCategoryDto> CreateServiceCategory([FromBody] ServiceCategoryDto categoryDto)
        {
            var category = ToEntity(categoryDto);
            category.Id = _categories.Any() ? _categories.Max(s => s.Id) + 1 : 1;
            _categories.Add(category);
            var createdDto = ToDto(category);
            return CreatedAtAction(nameof(GetServiceCategory), new { id = category.Id }, createdDto);
        }

        /// <summary>
        /// Updates an existing service category.
        /// </summary>
        /// <param name="id">The ID of the service category to update.</param>
        /// <param name="categoryDto">The updated service category data.</param>
        /// <returns>NoContent on success; NotFound if not found.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateServiceCategory(int id, [FromBody] ServiceCategoryDto categoryDto)
        {
           
            var index = _categories.FindIndex(a => a.Id == id);

            if (index == -1)
                return NotFound();

            var updated = ToEntity(categoryDto);
            updated.Id = id;

            _categories[index] = updated;

            return NoContent();
        }

        /// <summary>
        /// Deletes a service category by ID.
        /// </summary>
        /// <param name="id">The ID of the service category to delete.</param>
        /// <returns>NoContent if deleted; NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteServiceCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            _categories.Remove(category);
            return NoContent();
        }

        private static ServiceCategoryDto ToDto(ServiceCategory category)
        {
            return new ServiceCategoryDto
            {
                categoryName = category.categoryName,
                Description = category.Description,
                PriorityLevel = category.PriorityLevel,
                DiscountAvailability = category.DiscountAvailability
            };
        }

        private static ServiceCategory ToEntity(ServiceCategoryDto dto)
        {
            return new ServiceCategory
            {
                categoryName = dto.categoryName,
                Description = dto.Description,
                PriorityLevel = dto.PriorityLevel,
                DiscountAvailability = dto.DiscountAvailability
            };
        }
    }
}