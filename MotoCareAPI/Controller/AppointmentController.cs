using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly MotoCareDbContext _context;

        public AppointmentController(MotoCareDbContext context)
        {
            _context = context;
        }

        private static AppointmentDto ToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
               // Id = appointment.Id,
                Title = appointment.Title,
                Description = appointment.Description,
                CreatedDate = appointment.CreatedDate,
                CustomerId = appointment.CustomerId,
                CarId = appointment.CarId,
                Status = appointment.Status
            };
        }

        private static Appointment ToEntity(AppointmentDto dto)
        {
            return new Appointment
            {
              //  Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate,
                CustomerId = dto.CustomerId,
                CarId = dto.CarId,
                Status = dto.Status
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppointmentDto>> GetAppointments()
        {
            var appointments = _context.Appointments.ToList();
            var dtos = appointments.Select(ToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<AppointmentDto> GetAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(ToDto(appointment));
        }

        [HttpPost]
        public ActionResult<AppointmentDto> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = ToEntity(appointmentDto);
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            var createdDto = ToDto(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] AppointmentDto updatedDto)
        {
            if (id != updatedDto.Id)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Title = updatedDto.Title;
            appointment.Description = updatedDto.Description;
            appointment.CreatedDate = updatedDto.CreatedDate;
            appointment.CustomerId = updatedDto.CustomerId;
            appointment.CarId = updatedDto.CarId;
            appointment.Status = updatedDto.Status;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
