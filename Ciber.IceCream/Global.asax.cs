﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CiberIs.Badges;
using CiberIs.Models;
using EmptyMvc4.Models;

namespace CiberIs
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new CreateDatabaseIfNotExists<UsersContext>());
            var badges = new List<Type>
                {
                    typeof (MorningGloryBadge),
                    typeof (MedalsBadge),
                    typeof (ConsistentBadge),
                    typeof (OneFavouriteBadge),
                    typeof (MixItUpBadge)
                };
            var mongoDb = (IMongoDb)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IMongoDb));
            badges.ForEach(x => Activator.CreateInstance(x, mongoDb));
        }
    }
}