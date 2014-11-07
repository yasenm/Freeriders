namespace FreeRiders.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Location
    {
        private ICollection<Album> albums;
        private ICollection<Review> reviews;

        public Location()
        {
            this.albums = new HashSet<Album>();
            this.reviews = new HashSet<Review>();
        }

        [Key]
        public int ID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int PictureID { get; set; }

        public virtual Picture Picture { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public virtual LocationCategory Category { get; set; }

        public ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }
    }
}
