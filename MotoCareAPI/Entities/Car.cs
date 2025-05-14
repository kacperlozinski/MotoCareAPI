namespace MotoCareAPI.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }       
        public string Model { get; set; }      
        public string LicensePlate { get; set; }
        public int Year { get; set; }
        public int CustomerId { get; set; }
    }
}
