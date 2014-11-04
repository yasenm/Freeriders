namespace FreeRiders.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LocationCategory
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
