namespace MotoCareAPI.MotoCareDTO
{
    public class AppointmentDto
    {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public DateTime CreatedDate { get; set; }

            public int CustomerId { get; set; }
            public int CarId { get; set; }

            public string Status { get; set; }

        
    }
}
