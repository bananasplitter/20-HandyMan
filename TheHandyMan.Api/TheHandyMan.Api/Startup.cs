using HandyMan.Core.Domain;
using HandyMan.Core.Infrastructure;
using HandyMan.Core.Repository;
using HandyMan.Core.Services.Finance;
using HandyMan.Data.Infrastructure;
using HandyMan.Data.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TheHandyMan.Api;
using TheHandyMan.Api.Infrastructure;


[assembly : OwinStartup(typeof(TheHandyMan.Api.Startup))]
namespace TheHandyMan.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureSimpleInjector(app);
            ConfigureOAuth(app, container);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
        
        public void ConfigureOAuth(IAppBuilder app, Container container)
        {
            Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;

            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new HandyManAuthorizationServerProvider(authRepositoryFactory)
            };
            // Token Generation
            app.UseOAuthAuthorizationServer(authorizationOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public Container ConfigureSimpleInjector(IAppBuilder app)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();

            container.Register<IHandyManTicketRepository, HandyManTicketRepository>();
            container.Register<IRoleRepository, RoleRepository>();
            container.Register<IServiceRepository, ServiceRepository>();
            container.Register<ITicketRepository, TicketRepository>();
            container.Register<ITicketServiceRepository, TicketServiceRepository>();
            container.Register<ITimeEntryRepository, TimeEntryRepository>();
            container.Register<IUserStore<User, string>, UserStore>(Lifestyle.Scoped);
            container.Register<IPaymentService, StripePaymentService>();
            container.Register<IUserRepository, UserRepository>();

            container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);



            app.Use(async (context, next) =>
            {
                using(container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Verify();

            return container;
        }
    }
}