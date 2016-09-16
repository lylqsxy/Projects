using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Cobra.App.Infrastructure.Contracts;
using Cobra.Identity;
using Cobra.Identity.IdentityModel;
using Cobra.Infrastructure.Contracts;
using Cobra.Infrastructure.Data;
using Cobra.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading;


namespace Cobra.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ILogService _logService;
        public HomeController(ILogService logService)
        {
            _logService = logService;
        }
        
        [Authorize]
        public async Task<ActionResult> Index()
        {
            
            return View("Index");
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ApplicationUserManager UserManager
        //{
        //    get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        //    private set { _userManager = value; }
        //}
        //to set culture for globalisation
       

    }
}