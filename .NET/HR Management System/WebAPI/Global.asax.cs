using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

using System.Web.Mvc;

namespace CapstoneAlpha.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
        }

        // need to add this for the POST to work
        public void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "OPTIONS")
            {
                Response.StatusCode = 200;
                var httpApplication = sender as HttpApplication;
                httpApplication.CompleteRequest();
                Response.End();
            }
        }
    }
}
