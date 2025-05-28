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

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<AppointmentDto>> GetAppointments()
        {
            var dtos = _appointments.Select(ToDto).ToList();

            return Ok(dtos);
        }

        /// <summary>
        /// Retrieves a specific appointment by ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>An appointment DTO if found; otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<AppointmentDto> GetAppointment(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);

            if (appointment == null)
                return NotFound();

            return Ok(ToDto(appointment));
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="dto">Appointment data.</param>
        /// <returns>Newly created appointment with route info.</returns>
        [HttpPost]
        public ActionResult<AppointmentDto> CreateAppointment([FromBody] AppointmentDto dto)
        {
            var appointment = ToEntity(dto);
            appointment.Id = _appointments.Any() ? _appointments.Max(s => s.Id) + 1 : 1;
            _appointments.Add(appointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, ToDto(appointment));
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">ID of the appointment to update.</param>
        /// <param name="dto">Updated appointment data.</param>
        /// <returns>No content on success; NotFound if not found.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] AppointmentDto dto)
        {
            var index = _appointments.FirstOrDefault(a => a.Id == id);

            if (index is null)
                return NotFound();

            var updated = ToEntity(dto);
            updated.Id = id;

            var i = _appointments.FindIndex(a => a.Id == id);

            if (i >= 0)
                _appointments[i] = updated;

            return NoContent();
        }

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        /// <param name="id">The appointment ID to delete.</param>
        /// <returns>No content on success; NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);

            if (appointment == null)
                return NotFound();

            _appointments.Remove(appointment);

            return NoContent();
        }

        private static AppointmentDto ToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
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
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate,
                CustomerId = dto.CustomerId,
                CarId = dto.CarId,
                Status = dto.Status
            };
        }
    }
}
