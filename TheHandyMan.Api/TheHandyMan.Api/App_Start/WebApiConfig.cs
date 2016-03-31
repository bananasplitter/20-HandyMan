using AutoMapper;
using HandyMan.Core.Domain;
using HandyMan.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TheHandyMan.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            CreateMaps();
        }

        public static void CreateMaps()
        {
            Mapper.CreateMap<Service, ServiceModel>();
            Mapper.CreateMap<Ticket, TicketModel>();
            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<TicketService, TicketServiceModel>();
            Mapper.CreateMap<HandyManTicket, HandyManTicketModel>();
            Mapper.CreateMap<TimeEntry, TimeEntry>();

        }
    }
}
