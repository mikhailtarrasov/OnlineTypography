using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using NCron.Fluent;
using NCron.Fluent.Crontab;
using NCron.Fluent.Generics;
using NCron.Fluent.Reflection;
using NCron.Integration.NLog;
using NCron.Service;
using Vera.CBR;
using Vera.Domain;
using Vera.Domain.Entity;
using Vera.Models;

//using Vera.App_Start;

namespace Vera
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static SchedulingService schedulingService { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitialiseMapping();
            InitialiseSchedulingService();  
        }

        private void InitialiseSchedulingService()
        {
            schedulingService = new SchedulingService();
            schedulingService.LogFactory = new NLogFactory();
            schedulingService.At("25 16 * * *").Run<CurrenciesUpdater>(); // 16:25 UTC
            schedulingService.Start();
        }

        private void InitialiseMapping()
        {               
            Mapper.Initialize(cfg => cfg.CreateMap<Job, JobViewModel>()
                   .ForMember(x => x.Id, x => x.MapFrom(j => j.Id))
                   .ForMember(x => x.JobTitle, x => x.MapFrom(j => j.JobTitle))
                   .ForMember(x => x.Cost, x => x.MapFrom(j => j.Pay.Cost))
                   .ForMember(x => x.CurrencyName, x => x.MapFrom(j => j.Pay.Currency.Name))
                   .ForMember(x => x.CurrencyRate, x => x.MapFrom(j => j.Pay.Currency.Rate))
                   .ForMember(x => x.DependencyName, x => x.MapFrom(j => j.Dependency.Name)));
        }
    }
}