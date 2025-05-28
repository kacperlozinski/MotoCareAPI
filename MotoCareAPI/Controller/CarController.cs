using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private static List<Car> _cars = new List<Car>();

        /// <summary>
        /// Retrieves all cars.
        /// </summary>
        /// <returns>List of all cars.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetCars()
        {
            var dtos = _cars.Select(ToDto).ToList();
            return Ok(dtos);
        }

        /// <summary>
        /// Retrieves a car by its ID.
        /// </summary>
        /// <param name="id">The ID of the car.</param>
        /// <returns>The car with the given ID or NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<CarDto> GetCar(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
                return NotFound();

            return Ok(ToDto(car));
        }

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="dto">The data of the car to create.</param>
        /// <returns>The created car with its ID.</returns>
        [HttpPost]
        public ActionResult<CarDto> CreateCar([FromBody] CarDto dto)
        {
            var car = ToEntity(dto);
            car.Id = _cars.Any() ? _cars.Max(s => s.Id) + 1 : 1;
            _cars.Add(car);

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, ToDto(car));
        }

        /// <summary>
        /// Updates an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to update.</param>
        /// <param name="dto">The new data for the car.</param>
        /// <returns>No content on success or NotFound.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] CarDto dto)
        {
            var index = _cars.FindIndex(a => a.Id == id);

            if (index == -1)
                return NotFound();

            var updated = ToEntity(dto);
            updated.Id = id;

            _cars[index] = updated;

            return NoContent();
        }

        /// <summary>
        /// Deletes a car by its ID.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>No content on success or NotFound.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
                return NotFound();

            _cars.Remove(car);
            return NoContent();
        }

        private static CarDto ToDto(Car car)
        {
            return new CarDto
            {
                Brand = car.Brand,
                Model = car.Model,
                LicensePlate = car.LicensePlate,
                Year = car.Year,
                CustomerId = car.CustomerId
            };
        }

        private static Car ToEntity(CarDto dto)
        {
            return new Car
            {
                Brand = dto.Brand,
                Model = dto.Model,
                LicensePlate = dto.LicensePlate,
                Year = dto.Year,
                CustomerId = dto.CustomerId
            };
        }
    }
}
