using System.Web;
using Cobra.App.Infrastructure.Contracts;
using Cobra.App.Infrastructure.Services;
using Cobra.Identity;
using Cobra.Infrastructure;
using Cobra.Infrastructure.Contracts;
using Cobra.Infrastructure.Data;
using Cobra.Infrastructure.Services;
using Cobra.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Cobra.Configuration
{
    public class AppInfrastructureNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<CobraEntities>().ToMethod(ctx => new CobraEntities()).InRequestScope();
            Bind<IUnitOfWork>().To<UnitOfWork>();

            Bind<ApplicationUserManager>()
                .ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
            Bind<ApplicationRoleManager>()
                .ToMethod(x => HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>());
            Bind<ApplicationSignInManager>()
                .ToMethod(x => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());

            Bind<IOwinContext>()
                .ToMethod(x => HttpContext.Current.GetOwinContext())
                .InRequestScope();
            Bind<IAuthenticationManager>()
                .ToMethod(x => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();

            Bind<IAccountService>().To<AccountService>();
            Bind<IAuthenticationService>().To<AuthenticationService>();
            Bind<IEmailSendingService>().To<EmailSendingService>();
            Bind<ISmsSendingService>().To<SmsSendingService>();

            Bind<IRepository<Person>>().To<Repository<Person>>();
            Bind<IRepository<Profile>>().To<Repository<Profile>>();
            Bind<IRepository<Organisation>>().To<Repository<Organisation>>();
            // Author: Aaron Bhardwaj
            Bind<IRepository<Phone>>().To<Repository<Phone>>();
            Bind<IRepository<Address>>().To<Repository<Address>>();
            Bind<IRepository<Email>>().To<Repository<Email>>();
            Bind<IRepository<EmergencyContact>>().To<Repository<EmergencyContact>>();
            Bind<IRepository<AddressType>>().To<Repository<AddressType>>();
            Bind<IRepository<EmailType>>().To<Repository<EmailType>>();
            Bind<IRepository<Country>>().To<Repository<Country>>();
            Bind<IRepository<SocialMediaType>>().To<Repository<SocialMediaType>>();
            Bind<IRepository<PhoneType>>().To<Repository<PhoneType>>();
            Bind<IRepository<Relationship>>().To<Repository<Relationship>>();
            Bind<IRepository<EventType>>().To<Repository<EventType>>();
            Bind<IRepository<AlertType>>().To<Repository<AlertType>>();
            Bind<IRepository<ResourceType>>().To<Repository<ResourceType>>();

            Bind<ILogService>().To<LogService>();

            Bind<IPersonService>().To<PersonService>();
            Bind<IOrganisationService>().To<OrganisationService>();
            Bind<IProfileService>().To<ProfileService>();
            Bind<IAttributeTypeService>().To<AttributeTypeService>();
            Bind<IUserManagementService>().To<UserManagementService>();
        }
    }
}