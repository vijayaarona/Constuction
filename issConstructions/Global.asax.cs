using issConstructions.App_Start;
using issConstructions.Custom;
using issDomain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace issConstructions
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // WebApiConfig.Register(GlobalConfiguration.Configuration(WebApiConfig.))

            //need to install Install-Package Microsoft.AspNet.WebApi.WebHost
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies["issContrucations"];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                    CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                    principal.Id = serializeModel.id;
                    principal.Name = serializeModel.name;
                    principal.Email = serializeModel.email;
                    principal.Role = serializeModel.role;
                    principal.EId = serializeModel.EId;
                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception ex)
            {


            }


        }
    }
}
