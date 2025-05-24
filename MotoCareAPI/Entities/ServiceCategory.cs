using MotoCareAPI.Enums;

namespace MotoCareAPI.Entities
{
    public class ServiceCategory
    {        
            public int Id { get; set; }
            public CategoryName categoryName { get; set; }
            public string Description { get; set; } 
            public PriorityLevel PriorityLevel { get; set; } 
            public DiscountAvailability  DiscountAvailability{ get; set; } 
        public ICollection<Service> Services { get; set; }  

    }
}
