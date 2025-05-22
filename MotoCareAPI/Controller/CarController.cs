using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly MotoCareDbContext _context;

        public CarController(MotoCareDbContext context)
        {
            _context = context;
        }

       
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
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return Ok(cars.Select(ToDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();
            return Ok(ToDto(car));
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCar([FromBody] CarDto carDto)
        {
            var car = ToEntity(carDto);
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            var createdDto = ToDto(car);
            return CreatedAtAction(nameof(GetCar), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarDto carDto)
        {
            if (id != carDto.Id)
                return BadRequest();

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            car.Brand = carDto.Brand;
            car.Model = carDto.Model;
            car.LicensePlate = carDto.LicensePlate;
            car.Year = carDto.Year;
            car.CustomerId = carDto.CustomerId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
