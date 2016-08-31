using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMembership.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyLog.LogManager.Log(MyLog.LogType.Info, "我要开始记日志了");
            return View();
        }
    }
}
