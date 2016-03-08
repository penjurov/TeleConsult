namespace TeleConsult.Web.Controllers.Base
{
    using Infrastructure.Unity;
    using Models;
    using System;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        protected MvcUnityDependencyResolver DependencyResolver
        {
            get
            {
                return System.Web.Mvc.DependencyResolver.Current as MvcUnityDependencyResolver;
            }
        }

        protected TModel LoadModel<TModel>()
            where TModel : IModel
        {
            var model = this.DependencyResolver.LoadModel<TModel>();
            model.Init();
            return model;
        }

        protected TModel LoadModel<TModel, TData>(TData data)
            where TModel : IModel<TData>
        {
            var model = this.DependencyResolver.LoadModel<TModel, TData>();
            model.Init(data);
            return model;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;

            // Wrap Ajax responses
            if (filterContext.HttpContext.Request.IsAjaxRequest() && ex != null)
            {
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        Message = ex.Message
                    }
                };
            }
            
            filterContext.ExceptionHandled = true;
        }
    }
}