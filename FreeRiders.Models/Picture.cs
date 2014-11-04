namespace FreeRiders.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string PictureUrl { get; set; }
    }
}
