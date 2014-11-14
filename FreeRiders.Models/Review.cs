namespace FreeRiders.Models
{
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class Review : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string Content { get; set; }

        [Required]
        public string AuthorID { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public int AlbumID { get; set; }

        public virtual Album Album { get; set; }

        [Required]
        [Range(0, 5)]
        public double Stars { get; set; }
    }
}
