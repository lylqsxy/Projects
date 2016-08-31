using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyMembership.Models;

namespace MyMembership.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            // Membership.ValidateUser 判断用户名和密码是否正确
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                // 调用Forms 的登录 User.Identity.IsAuthenticated 将设置为True
                // User.Identity.Name 会设置成我们下面的UserName
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Manage()
        {
            return View();
        }

        /**************************************************/
        /*注意我们为Manage这两个Action加上了Authorize标签，只有登录过的用户才能访问
          如果用户没有登录直接访问/Account/Manage会被跳到/Account/Login要求登录*/

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    var user = Membership.GetUser(User.Identity.Name);
                    changePasswordSucceeded = user.ChangePassword(model.OldPassword,
                        model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("Manage", new { Message = "修改密码成功！" });
                }
                else
                {
                    ModelState.AddModelError("", "老密码不正确或新密码不符合要求");
                }
            }
            return View(model);
        }






        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Membership.CreateUser去创建用户
                    var user = Membership.CreateUser(model.UserName, model.Password);
                    if (user != null)
                    {
                        //注册完成之后直接登录用户
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                    }
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
