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
        private static List<Appointment> _appointments = new List<Appointment>();
        private static int _nextId = 1;

        private static AppointmentDto ToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
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
                Id = dto.Id,
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
            var dtos = _appointments.Select(ToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<AppointmentDto> GetAppointment(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
                return NotFound();

            return Ok(ToDto(appointment));
        }

        [HttpPost]
        public ActionResult<AppointmentDto> CreateAppointment([FromBody] AppointmentDto dto)
        {
            var appointment = ToEntity(dto);
            appointment.Id = _nextId++;
            _appointments.Add(appointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, ToDto(appointment));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] AppointmentDto dto)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
                return NotFound();

            appointment.Title = dto.Title;
            appointment.Description = dto.Description;
            appointment.CreatedDate = dto.CreatedDate;
            appointment.CustomerId = dto.CustomerId;
            appointment.CarId = dto.CarId;
            appointment.Status = dto.Status;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
                return NotFound();

            _appointments.Remove(appointment);
            return NoContent();
        }
    }
}
