namespace FreeRiders.Web
{
    using System.Web.Mvc;

    class ViewEnginesConfiguration
    {
        internal static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            viewEngineCollection.Clear();
            viewEngineCollection.Add(new RazorViewEngine());
        }
    }
}
