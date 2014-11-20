namespace FreeRiders.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.Infrastructure.Services.Base;

    public class DDLServices : BaseServices
    {
        public DDLServices(IFreeRidersData data)
            :base (data)
        {
        }

        public IEnumerable<SelectListItem> LocationsDDL
        {
            get
            {
                return this.GetLocations();
            }
        }

        public IEnumerable<SelectListItem> LocationsCategoriesDDL
        {
            get
            {
                return this.GetLocationsCategories();
            }
        }

        public IEnumerable<SelectListItem> AlbumsCategoriesDDL
        {
            get
            {
                return this.GetAlbumsCategories();
            }
        }

        private IEnumerable<SelectListItem> GetAlbumsCategories()
        {
            var albumCategories = this.Data
                .AlbumCategories
                .All()
                .ToList();

            List<SelectListItem> categories = new List<SelectListItem>();
            categories.AddRange(new SelectList(albumCategories, "ID", "Name"));
            var result = new SelectList(categories, "Value", "Text");

            return result;
        }

        public IEnumerable<SelectListItem> GetLocations()
        {
            var locations = this.Data
                .Locations
                .All()
                .ToList();

            List<SelectListItem> ddlLocations = new List<SelectListItem>();
            ddlLocations.AddRange(new SelectList(locations, "ID", "Name"));
            var result = new SelectList(ddlLocations, "Value", "Text");

            return result;
        }

        public IEnumerable<SelectListItem> GetLocationsCategories()
        {
            var locationCategories = this.Data
                .LocationCategories
                .All()
                .ToList();

            List<SelectListItem> categories = new List<SelectListItem>();
            categories.AddRange(new SelectList(locationCategories, "ID", "Name"));
            var result = new SelectList(categories, "Value", "Text");

            return result;
        }
    }
}