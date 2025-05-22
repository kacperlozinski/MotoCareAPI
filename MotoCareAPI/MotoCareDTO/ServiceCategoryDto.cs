namespace MotoCareAPI.MotoCareDTO
{
    public class ServiceCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } //basic, standard, premium
        public string Description { get; set; }
        public string Priority { get; set; } //low, medium, high
        public string AvailableDiscount { get; set; } // yes/no
    }
}
