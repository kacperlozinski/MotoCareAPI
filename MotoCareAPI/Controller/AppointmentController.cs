using Microsoft.AspNetCore.Mvc;
using MotoCareAPI.Entities;
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

        // GET: api/Appointment
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return Ok(_context.Appointments.ToList());
        }

        // GET: api/Appointment/{id}
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        // POST: api/Appointment
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        // PUT: api/Appointment/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] Appointment updatedAppointment)
        {
            if (id != updatedAppointment.Id)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Title = updatedAppointment.Title;
            appointment.Description = updatedAppointment.Description;
            appointment.CreatedDate = updatedAppointment.CreatedDate;
            appointment.CustomerId = updatedAppointment.CustomerId;
            appointment.CarId = updatedAppointment.CarId;
            appointment.Status = updatedAppointment.Status;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Appointment/{id}
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
