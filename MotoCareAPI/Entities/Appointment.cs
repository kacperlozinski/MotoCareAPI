using MotoCareAPI.Enums;
namespace MotoCareAPI.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
