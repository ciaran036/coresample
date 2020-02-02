//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Services.Abstract.Factories;
//using Web.Extensions;
//using Web.Filters;

//namespace Web.Controllers
//{
//    [PopulateTitles, PopulateCountries]
//    public class BaseController : Controller
//    {
//        public IErrorFactory ErrorFactory { get; set; }

//        protected T CreateModel<T>() where T : Models.GenericModel
//        {
//            var model = DependencyResolver.Current.GetService<T>();
//            Models.GenericModel genericModel = model;
//            genericModel.InitialiseControllerContext(ControllerContext);
//            return model;
//        }

//        public bool GetUpdateModel<T>(T entity) where T : class
//        {
//            return TryUpdateModel(entity);
//        }

//        protected override void OnException(ExceptionContext filterContext)
//        {
//            var exception = filterContext.Exception;
//            ErrorFactory.LogError(exception);

//            if (filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                filterContext.Result = new HttpStatusCodeResult(400, "An unhandled error occurred");
//                filterContext.ExceptionHandled = true;
//            }
//            else if (!Request.IsLocal)
//            {
//                var result = View("_Error", new HandleErrorInfo(exception,
//                    filterContext.RouteData.Values["controller"].ToString(),
//                    filterContext.RouteData.Values["action"].ToString()
//                ));

//                filterContext.Result = result;
//                filterContext.ExceptionHandled = true;
//            }
//        }

//#if DEBUG
//        /// <summary>
//        /// To assist with identifying Model validation errors when debugging
//        /// </summary>
//        public List<ModelError> ValidationSummary => ModelState.Values.SelectMany(v => v.Errors).ToList();
//#endif
//    }
//}