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
        private static int _nextId = 1;

        private static CarDto ToDto(Car car)
        {
            return new CarDto
            {
                Id = car.Id,
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
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                LicensePlate = dto.LicensePlate,
                Year = dto.Year,
                CustomerId = dto.CustomerId
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetCars()
        {
            var dtos = _cars.Select(ToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CarDto> GetCar(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            return Ok(ToDto(car));
        }

        [HttpPost]
        public ActionResult<CarDto> CreateCar([FromBody] CarDto dto)
        {
            var car = ToEntity(dto);
            car.Id = _nextId++;
            _cars.Add(car);

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, ToDto(car));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] CarDto dto)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.LicensePlate = dto.LicensePlate;
            car.Year = dto.Year;
            car.CustomerId = dto.CustomerId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            _cars.Remove(car);
            return NoContent();
        }
    }
}
