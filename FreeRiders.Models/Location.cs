namespace FreeRiders.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class Location : DeletableEntity
    {
        private ICollection<Album> albums;
        private ICollection<Review> reviews;
        private ICollection<Event> events;

        public Location()
        {
            this.albums = new HashSet<Album>();
            this.reviews = new HashSet<Review>();
            this.events = new HashSet<Event>();
        }

        [Key]
        public int ID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 50)]
        public string Description { get; set; }

        [Required]
        public int PictureID { get; set; }

        public virtual Picture Picture { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public virtual LocationCategory Category { get; set; }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public virtual ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }

        public virtual ICollection<Event> Events
        {
            get { return this.events; }
            set { this.events = value; }
        }
    }
}
