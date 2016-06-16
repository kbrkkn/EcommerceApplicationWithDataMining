using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApplicationDbContext context=new ApplicationDbContext();
            CreateRole(context);
        }

        private static void CreateRole(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("User"))//AspNetRoles tablosunda user role'ı yoksa,ekle.
            {
                IdentityRole userRole = new IdentityRole("User");
                roleManager.Create(userRole);
            }
            if (!roleManager.RoleExists("Admin"))//AspNetRoles tablosunda admin role'ı yoksa,ekle.
            {
                IdentityRole adminRole = new IdentityRole("Admin");
                roleManager.Create(adminRole);
            }
        }
    }
}
