using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoCareAPI.Entities;
using MotoCareAPI.Infrastructure.Interfaces;

namespace MotoCareAPI.Infrastructure.Repositories
{
   public class AppointmentRepository : IAppointmentRepository
    {
        private readonly List<Appointment> _appointments = new()
    {
        new Appointment { Id = 1, Description = "Oil change", CreatedDate = DateTime.Now.AddDays(1) },
        new Appointment { Id = 2, Description = "Tire replacement", CreatedDate = DateTime.Now.AddDays(2) },
    };
        public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            var appointments = _appointments.ToList();
            return Task.FromResult<IEnumerable<Appointment>>(appointments);
        }
        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            return appointment;
        }
        public Task CreateAppointmentAsync(Appointment appointment)
        {
            appointment.Id = _appointments.Any() ? _appointments.Max(s => s.Id) + 1 : 1;
            var appointmentC = new Appointment
            {
                Title = appointment.Title,
                Description = appointment.Description,
                CreatedDate = DateTime.Now,
                CustomerId = appointment.CustomerId,
                CarId = appointment.CarId,
                ServiceId = appointment.ServiceId,
                Status = appointment.Status,

            };
           _appointments.Add(appointmentC);
            return Task.CompletedTask;

        }
       public Task UpdateAppointmentAsync(Appointment appointment)
        {


            var appointmentC = new Appointment
            {
                Title = appointment.Title,
                Description = appointment.Description,
                CreatedDate = DateTime.Now,
                CustomerId = appointment.CustomerId,
                CarId = appointment.CarId,
                ServiceId = appointment.ServiceId,
                Status = appointment.Status,

            };
            var index = _appointments.FindIndex(a => a.Id == appointment.Id);
            if (index >= 0)
            {
                _appointments[index] = appointmentC;
            }
            return Task.CompletedTask;
        }
        public Task DeleteAppointmentAsync(int id)
        {
             var appointment = _appointments.FirstOrDefault(a => a.Id == id);
               if (appointment != null)
               {
                  _appointments.Remove(appointment);
               }

            return Task.CompletedTask;

        }

    }
}
