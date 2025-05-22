namespace MotoCareAPI.MotoCareDTO
{
    public class ServiceDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime LastPriceUpdate { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}
