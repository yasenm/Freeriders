namespace FreeRiders.Models
{
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class AlbumCategory : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
