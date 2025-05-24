using MotoCareAPI.Enums;

namespace MotoCareAPI.MotoCareDTO
{
    public class ServiceCategoryDto
    {
        public CategoryName categoryName { get; set; }
        public string Description { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
        public DiscountAvailability DiscountAvailability { get; set; }
    }
}
