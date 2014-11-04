namespace FreeRiders.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string AuthorID { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        [Range(0, 5)]
        public int Stars { get; set; }
    }
}
