namespace FreeRiders.Web.Infrastructure.Services.Base
{
    using FreeRiders.Data.UnitsOfWork;

    public abstract class BaseServices
    {
        protected IFreeRidersData Data { get; private set; }

        public BaseServices(IFreeRidersData data)
        {
            this.Data = data;
        }
    }
}