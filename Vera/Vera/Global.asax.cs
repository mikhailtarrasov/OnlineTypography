using System.Web.Http;
using System.Web.Mvc;
//using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Vera.Domain.Entity;
using Vera.Models;

//using Vera.App_Start;

namespace Vera
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            InitialiseMapping();
        }

        public void InitialiseMapping()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Job, JobViewModel>()
                   .ForMember(x => x.Id, x => x.MapFrom(j => j.Id))
                   .ForMember(x => x.JobTitle, x => x.MapFrom(j => j.JobTitle))
                   .ForMember(x => x.Cost, x => x.MapFrom(j => j.Pay.Cost))
                   .ForMember(x => x.CurrencyName, x => x.MapFrom(j => j.Pay.Currency.Name))
                   .ForMember(x => x.CurrencyRate, x => x.MapFrom(j => j.Pay.Currency.Rate)));
        }
    }
}