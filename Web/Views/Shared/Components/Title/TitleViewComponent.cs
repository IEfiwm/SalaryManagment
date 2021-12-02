using Microsoft.AspNetCore.Mvc;

namespace Web.Views.Shared.Components.Title
{
    public class TitleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}