using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
using MotoCareAPI.MotoCareDTO;
using System.Collections.Generic;
using System.Linq;
using MotoCareAPI.Infrastructure.Repositories;
using MotoCareAPI.Infrastructure.Interfaces;

namespace MotoCareAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository; 
        }
        private readonly IAppointmentRepository _appointmentRepository;

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            

            return Ok(appointments);

            
        }

        /// <summary>
        /// Retrieves a specific appointment by ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>An appointment DTO if found; otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<AppointmentDto> GetAppointment(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentByIdAsync(id);

            if (appointment == null)
                return NotFound();

            return Ok(appointment);
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
           
            _appointmentRepository.CreateAppointmentAsync(appointment);

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
        {/*
            var index = _appointmentRepository.FirstOrDefault(a => a.Id == id);

            if (index is null)
                return NotFound();*/

            var updated = ToEntity(dto);
            updated.Id = id;

            

            return Ok();
        }

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        /// <param name="id">The appointment ID to delete.</param>
        /// <returns>No content on success; NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
           


            _appointmentRepository.DeleteAppointmentAsync(id);

            return Ok();
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
