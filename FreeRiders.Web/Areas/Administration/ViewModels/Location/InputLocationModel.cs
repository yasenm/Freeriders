namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using FreeRiders.Models;
    using System.Web;

    public class InputLocationModel
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public Location GetLocationForDb()
        {
            var result = new Location
            {
                Name = this.Name,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Description = this.Description,
                CategoryID = this.CategoryID,
            };

            return result;
        }
    }
}