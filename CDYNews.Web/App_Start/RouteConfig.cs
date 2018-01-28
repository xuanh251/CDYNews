using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CDYNews.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
            name: "TagList",
            url: "tag/{tagId}.html",
            defaults: new { controller = "Post", action = "ListPostByTag", tagId = UrlParameter.Optional },
              namespaces: new string[] { "CDYNews.Web.Controllers" }
        );
            routes.MapRoute(
             name: "Search",
             url: "tim-kiem.html",
             defaults: new { controller = "Post", action = "Search", id = UrlParameter.Optional },
               namespaces: new string[] { "CDYNews.Web.Controllers" }
         );
            routes.MapRoute(
           name: "Login",
           url: "dang-nhap.html",
           defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
             namespaces: new string[] { "CDYNews.Web.Controllers" }
       );
            routes.MapRoute(
            name: "Register",
            url: "dang-ky.html",
            defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
              namespaces: new string[] { "CDYNews.Web.Controllers" }
        );
            routes.MapRoute(
             name: "Post Category",
             url: "{alias}.pc-{id}.html",
             defaults: new { controller = "Post", action = "ListPostCategory", id = UrlParameter.Optional },
               namespaces: new string[] { "CDYNews.Web.Controllers" }
         );

            routes.MapRoute(
             name: "Post",
             url: "{alias}.p-{postId}.html",
             defaults: new { controller = "Post", action = "Detail", postId = UrlParameter.Optional },
               namespaces: new string[] { "CDYNews.Web.Controllers" }
         );
            routes.MapRoute(
            name: "Home",
            url: "home.html",
            defaults: new { controller = "Home", action = "Index", postId = UrlParameter.Optional },
              namespaces: new string[] { "CDYNews.Web.Controllers" }
        );
            routes.MapRoute(
            name: "Page",
            url: "trang/{alias}.html",
            defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
              namespaces: new string[] { "CDYNews.Web.Controllers" }
        );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "CDYNews.Web.Controllers" }
            );
        }
    }
}
