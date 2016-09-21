using Cobra.App.Infrastructure.Contracts;
using Cobra.Filters;
using Cobra.Identity.IdentityModel;
using Cobra.Infrastructure.Contracts;
using Cobra.Infrastructure.Data;
using Cobra.Model;
using Cobra.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;


namespace Cobra.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        #region Data 
        private IOrganisationService _organisationService;
        private IProfileService _profileService;
        private ILogService _logService;
        private IAccountService _accountService;
        private IUserManagementService _userManagementService;
        #endregion
        public AdminController(IOrganisationService organisationService,
            IProfileService profileService, ILogService logService, IAccountService accountService, IUserManagementService userManagementService)
        {
            _organisationService = organisationService;
            _profileService = profileService;
            _logService = logService;
            _accountService = accountService;
            _userManagementService = userManagementService;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region OrganisationAdministration

        // Author: Aaron Bhardwaj
        // GET: Admin/OrganisationAdministration
        public ActionResult OrganisationAdministration()
        {
            return View();
        }

        // Author: Aaron Bhardwaj
        // GET: Admin/OrganisationAdministrationData
        [HttpGet]
        public ActionResult OrganisationAdministrationData()
        {
            // by craig
            // disabled this version as it filters inactive organisations. wuth the activate button in view not required currently.
            //var model = _organisationService.GetAllOrganisation()
            //    .Where(x => x.IsActive)
            //    .Select(x => new Admin.OrganisationViewModel { Id = x.Id, OrgName = x.OrgName, WebsiteUrl = x.WebsiteUrl, lastUpdate = x.UpdatedOn.ToString("yyyy-MM-dd HH:mm:ss"), isActive = x.IsActive })
            //    .ToList();

            var model = _organisationService.GetAllOrganisation()
                .Select(x => new Admin.OrganisationViewModel { Id = x.Id, OrgName = x.OrgName, WebsiteUrl = x.WebsiteUrl, lastUpdate = x.UpdatedOn.ToString("yyyy-MM-dd HH:mm:ss"), isActive = x.IsActive })
                .ToList();


            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/CreateOrganisation
        public ActionResult CreateOrganisation()
        {
            return View();
        }

        // Author: Aaron Bhardwaj
        // POST: Admin/CreateOrganisation
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult CreateOrganisation(Admin.OrganisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var created = _organisationService.CreateOrganisation(
                    new Organisation
                    {
                        OrgName = model.OrgName,
                        WebsiteUrl = model.WebsiteUrl,
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                        UpdatedBy = 0,
                        CreatedBy = 0,
                        UpdatedOn = DateTime.Now
                    }
                );
                if (created != 0)
                {
                    return Json(new
                    {
                        Success = true,
                        MsgText = "New organisation created.",
                        RedirectUrl = "OrganisationAdministration",
                        Id = created
                    });
                }
            }
            return View(model);
        }

        // Author: Aaron Bhardwaj
        // POST: Admin/EditOrganisation
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult EditOrganisation(Admin.OrganisationViewModel orgViewModel)
        {
            if (ModelState.IsValid)
            {
                if (orgViewModel.Id > 0)
                {

                    
                    var model = _organisationService.GetById(orgViewModel.Id);
                    if (!ReferenceEquals(model, null))
                    {
                        model.OrgName = orgViewModel.OrgName;
                        model.WebsiteUrl = orgViewModel.WebsiteUrl;
                        model.UpdatedBy = 1;
                        model.UpdatedOn = DateTime.Now;
                        model.IsActive = orgViewModel.isActive;
                        _organisationService.Update(model);

                        return Json(new
                        {
                            Success = true,
                            MsgText = "Organisation edited.",
                            RedirectUrl = "OrganisationAdministration"
                        });

                    }
                }
               
            }

            return Json(new
            {
                Success = false,
                MsgText = "Organisation was not edited.",
                RedirectUrl = "OrganisationAdministration"
            });
        }

        // Author: Aaron Bhardwaj
        // POST: Admin/DeleteOrganisation

        // currently commented out as not use but not removing as may be used in the future?
        // craig 9/9/2016

        //[HttpPost]
        //[ValidateCustomAntiForgeryToken]
        //public ActionResult DeleteOrganisation(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var model = _organisationService.GetById(id);
        //        if (!ReferenceEquals(model, null))
        //        {
        //            model.IsActive = false;
        //            model.UpdatedBy = 0;
        //            model.UpdatedOn = DateTime.Now;
        //            _organisationService.Update(model);
        //            return Json(new
        //            {
        //                Success = true,
        //                MsgText = "Organisation deleted.",
        //                RedirectUrl = "OrganisationAdministration"
        //            });
        //        }
        //    }
        //    return Json(new
        //    {
        //        Success = false,
        //        MsgText = "Organisation was not deleted.",
        //        RedirectUrl = "OrganisationAdministration"
        //    });
        //}

        #endregion

        // GET: Admin/CreateProfile
        public ActionResult CreateProfile()
        {
            return View();
        }

        // POST: Admin/CreateProfile
        [HttpPost]
        public ActionResult CreateProfile(Profile model)
        {
            if (ModelState.IsValid)
            {
                var created = _profileService.CreateProfile(
                        new Profile
                        {
                            FirstName = model.FirstName,
                            MiddleName = model.MiddleName,
                            LastName = model.LastName,
                            IsActive = true,
                            CreatedBy = 0,
                            CreatedOn = DateTime.Now
                        }
                    );
                if (created > 0)
                {
                    _logService.Info(string.Format("Success create user {0}, created on {1}, and created by {2}", model.FirstName + " " + model.MiddleName + " " + model.LastName, DateTime.Now.AddYears(-3), 0)); // 
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/CreatePerson 
        public ActionResult CreatePerson()
        {
            return View();
        }

        // POST: Admin/CreatePerson 
        [HttpPost]
        public ActionResult CreatePerson(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //helen Todo: to be improve method
        public ActionResult UserManagement()
        {
            return View();
        }

        //helen
        //Lavesh
        public ActionResult PopulateDB()
        {
            for (int i = 0; i <= 200; i++)
            {
                var EmailAddr = "Newuser" + i.ToString() + "@" + i.ToString() + ".test";
                _accountService.CreateUserLogin(EmailAddr, EmailAddr);

            };
            return View("UserManagement");
        }
        public JsonResult GetUserManagementCount()
        {
            return Json(_userManagementService.GetAllUserCount());
        }
        public JsonResult GetUserManagement(int startRec = 0, int displaySize = 10, string sortBy = "id", bool reverseSort = false, string sort = "", string filterBy = "")
        {

            IQueryable<ApplicationUser> allUsers = _userManagementService.GetAllUsers();
            Dictionary<string, string> filters = JsonConvert.DeserializeObject<Dictionary<string, string>>(filterBy);

            // apply filters
            if (filters != null)
            {
                if (filters.Count > 0)
                {
                    // cycle through filters list(if any) and apply to allUsers


                    foreach (var fieldToFilter in filters)
                    {

                        switch (fieldToFilter.Key)
                        {
                            case "$":
                                if (fieldToFilter.Value != "")
                                {
                                    var searchValue = fieldToFilter.Value;
                                    var searchResult = allUsers.Where(x => x.UserName.Contains(searchValue) || x.Email.Contains(searchValue) || x.PhoneNumber.Contains(searchValue));
                                    allUsers = searchResult;
                                }
                                break;

                            case "EmailConfirmed":
                                if (fieldToFilter.Value != "")
                                {
                                    var searchValue = Convert.ToBoolean(fieldToFilter.Value);
                                    var searchResult = allUsers.Where(x => x.EmailConfirmed.Equals(searchValue));
                                    allUsers = searchResult;
                                }
                                break;

                            case "PhoneNumberConfirmed":
                                if (fieldToFilter.Value != "")
                                {
                                    var searchValue = Convert.ToBoolean(fieldToFilter.Value);
                                    var searchResult = allUsers.Where(x => x.PhoneNumberConfirmed.Equals(searchValue));
                                    allUsers = searchResult;
                                }
                                break;

                            case "LockoutEnabled":
                                if (fieldToFilter.Value != "")
                                {
                                    var searchValue = Convert.ToBoolean(fieldToFilter.Value);
                                    var searchResult = allUsers.Where(x => x.LockoutEnabled.Equals(searchValue));
                                    allUsers = searchResult;
                                }
                                break;

                            case "IsActive":
                                if (fieldToFilter.Value != "")
                                {
                                    var searchValue = Convert.ToBoolean(fieldToFilter.Value);
                                    var searchResult = allUsers.Where(x => x.IsActive.Equals(searchValue));
                                    allUsers = searchResult;
                                }
                                break;

                        }
                    }

                };

            };

            sortBy = sortBy.ToUpper();
            if (sortBy == "USERNAME")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.UserName);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.UserName);

                }
            };
            if (sortBy == "EMAIL")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.Email);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.Email);

                }
            };

            if (sortBy == "ACCESSFAILEDCOUNT")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.AccessFailedCount);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.AccessFailedCount);

                }
            };
            if (sortBy == "PHONENUMBER")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.PhoneNumber);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.PhoneNumber);

                }
            };
            if (sortBy == "PHONENUMBERCONFIRMED")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.PhoneNumberConfirmed);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.PhoneNumberConfirmed);
                }
            };

            if (sortBy == "EMAILCONFIRMED")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.EmailConfirmed);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.EmailConfirmed);
                }
            };
            if (sortBy == "LOCKOUTENABLED")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.LockoutEnabled);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.LockoutEnabled);
                }
            };
            if (sortBy == "ISACTIVE")
            {
                if (reverseSort == false)
                {
                    allUsers = allUsers.OrderBy(x => x.IsActive);
                }
                else
                {
                    allUsers = allUsers.OrderByDescending(x => x.IsActive);
                }
            };
            if (allUsers.Count() < displaySize)
            {
                startRec = 0;
            };



            var retUsers = allUsers.AsEnumerable().Skip(startRec).Take(displaySize);
            var totalUsers = allUsers.ToList().Count();

            return Json(new { users = retUsers.ToList(), totalUsers = totalUsers }, JsonRequestBehavior.AllowGet);

        }
        //Lavesh - 

        //public ActionResult UpdateUserRecord(ApplicationUser user)
        //{
        //    //Update changed user record 
        //    var result = _userManagementService.UpdateUser(user);
        //    return Json(user); // this is not correct - will change to return true or false (updated successfully?)
        //}
        public ActionResult ToggleUserLockout(ApplicationUser user)
        {
            var result = _userManagementService.ToggleUserLockout(user);
            return Json(result);
        }
        public ActionResult ToggleEmailConfirmed(ApplicationUser user)
        {
            var result = _userManagementService.ToggleEmailConfirmed(user);
            return Json(result);
        }
        public ActionResult TogglePhoneNumberConfirmed(ApplicationUser user)
        {
            var result = _userManagementService.TogglePhoneNumberConfirmed(user);
            return Json(result);
        }
        public ActionResult UpdateEmail(ApplicationUser user)
        {
            var result = _userManagementService.UpdateEmail(user);
            return Json(result);
        }
        public ActionResult UpdatePhone(ApplicationUser user)
        {
            var result = _userManagementService.UpdatePhone(user);
            return Json(result);
        }
        public ActionResult ToggleActive(ApplicationUser user)
        {
            var result = _userManagementService.ToggleIsActive(user);
            return Json(result);
        }
        public ActionResult UpdateFailedAccessCount(ApplicationUser user)
        {
            var result = _userManagementService.UpdateFailedAccessCount(user);
            return Json(result);
        }
    }
}