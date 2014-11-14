namespace FreeRiders.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class Message : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string AuthorID { get; set; }

        public virtual User Author { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20)]
        public string Text { get; set; }

        public int EventID { get; set; }

        public virtual Event Event { get; set; }
    }
}
