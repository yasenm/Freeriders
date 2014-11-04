namespace FreeRiders.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string AuthorID { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
