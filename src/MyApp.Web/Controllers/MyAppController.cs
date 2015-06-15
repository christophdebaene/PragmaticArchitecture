using FluentValidation;
using System.Globalization;
using System.Web.Mvc;

namespace MyApp.Web.Controllers
{
    public abstract class MyAppController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            var validationException = filterContext.Exception as ValidationException;

            if (validationException != null)
            {
                filterContext.ExceptionHandled = true;

                foreach (var error in validationException.Errors)
                {
                    this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    this.ModelState.SetModelValue(error.PropertyName, new ValueProviderResult(error.AttemptedValue ?? "", (error.AttemptedValue ?? "").ToString(), CultureInfo.CurrentCulture));
                }

                filterContext.Result = new ViewResult
                {
                    ViewName = filterContext.RouteData.Values["action"].ToString(),
                    TempData = filterContext.Controller.TempData,
                    ViewData = filterContext.Controller.ViewData
                };
            }

            base.OnException(filterContext);
        }
    }
}