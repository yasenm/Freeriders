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
        public string Text { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
