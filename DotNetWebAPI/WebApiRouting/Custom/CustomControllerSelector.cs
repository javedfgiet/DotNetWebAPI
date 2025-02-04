using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApiRouting.Custom
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;

        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //Get all available web api controller mapping
            var controllers = GetControllerMapping();

            // Get the controller name parameter value from the request URI
            var routeData = request.GetRouteData();

            //Get the contoller name from data.
            //The name of controller in our case is "Students"
            var controllerName = routeData.Values["controller"].ToString();

            string versionNumber = "1";
            var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            if (versionQueryString["v"] != null)
            {
                versionNumber = versionQueryString["v"];
            }
            if (versionNumber == "1")
            {
                controllerName = controllerName + "V1";
            }
            else
            {
                controllerName = controllerName + "V2";
            }
            HttpControllerDescriptor controllerDescriptior;
            if (controllers.TryGetValue(controllerName, out controllerDescriptior))
            {
                return controllerDescriptior;
            }

            return null;
        }
    }
}