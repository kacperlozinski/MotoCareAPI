namespace MotoCareAPI.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }       
        public string Model { get; set; }      
        public string LicensePlate { get; set; }
        public int Year { get; set; }
        public int CustomerId { get; set; }
    }
}
