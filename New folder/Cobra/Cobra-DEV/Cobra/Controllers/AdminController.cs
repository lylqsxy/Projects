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
        private IAttributeTypeService _attributeTypeService;
        #endregion
        public AdminController(IOrganisationService organisationService,
            IProfileService profileService, ILogService logService, IAccountService accountService, IUserManagementService userManagementService, IAttributeTypeService attributeTypeService)
        {
            _organisationService = organisationService;
            _profileService = profileService;
            _logService = logService;
            _accountService = accountService;
            _userManagementService = userManagementService;
            _attributeTypeService = attributeTypeService;
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
        public ActionResult OrganisationAdministrationData(int startRec = 0, int displaySize = 10)
        {
            // by craig
            // disabled this version as it filters inactive organisations. wuth the activate button in view not required currently.
            //var model = _organisationService.GetAllOrganisation()
            //    .Where(x => x.IsActive)
            //    .Select(x => new Admin.OrganisationViewModel { Id = x.Id, OrgName = x.OrgName, WebsiteUrl = x.WebsiteUrl, lastUpdate = x.UpdatedOn.ToString("yyyy-MM-dd HH:mm:ss"), isActive = x.IsActive })
            //    .ToList();
            //IEnumerable<Organisation> allPeople = _organisationService.GetAllOrganisation();

            var modelOrgs = _organisationService.GetAllOrganisation()
                .Select(x => new Admin.OrganisationViewModel { Id = x.Id, OrgName = x.OrgName, WebsiteUrl = x.WebsiteUrl, lastUpdate = x.UpdatedOn.ToString("yyyy-MM-dd HH:mm:ss"), isActive = x.IsActive })
                .ToList();

            if (modelOrgs.Count() < displaySize)
            {
                startRec = 0;
            };

            var retOrgs = modelOrgs.AsEnumerable().Skip(startRec).Take(displaySize);
            var totalOrgs = modelOrgs.ToList().Count();



            return Json(new { Orgs = retOrgs.ToList(), totalOrgs = totalOrgs }, JsonRequestBehavior.AllowGet);
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

        //Show Attribute Type list ,and Create/Edit the types 
        public ActionResult AttributeTypeManagement()
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
                    // craig
                    // exactMatch is used to determin how username and email input boxes are filtered
                    // if exactmatch is true use equals, else use contains. see below at ***
                    var exactMatch = false;

                    if (filters.Any(x => x.Key.Equals("ExactMatch") && x.Value.Equals("true")))
                    {
                        exactMatch = true;
                    }
                    else
                    {
                        exactMatch = false;
                    }

                    foreach (var fieldToFilter in filters)
                    {

                        switch (fieldToFilter.Key)
                        {

                            case "$":
                                if (fieldToFilter.Value != "")
                                {
                                    if (exactMatch)   // if exactmatch is ticked use this to filter else use else statement
                                    {
                                        var searchValue = fieldToFilter.Value;
                                        var searchResult = allUsers.Where(x => x.UserName.Equals(searchValue) || x.Email.Equals(searchValue) || x.PhoneNumber.Equals(searchValue));
                                        allUsers = searchResult;
                                    }
                                    else
                                    {
                                        var searchValue = fieldToFilter.Value;
                                        var searchResult = allUsers.Where(x => x.UserName.Contains(searchValue) || x.Email.Contains(searchValue) || x.PhoneNumber.Contains(searchValue));
                                        allUsers = searchResult;
                                    }
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

        //get all the attributes type lists
        public JsonResult GetAllType()
        {
            IEnumerable<AddressType> allAddressType = _attributeTypeService.GetAllAddressType();
            IEnumerable<EmailType> allEmailType = _attributeTypeService.GetAllEmailType();
            IEnumerable<Country> allCountryType = _attributeTypeService.GetAllCountry();
            IEnumerable<PhoneType> allPhoneType = _attributeTypeService.GetAllPhoneType();
            IEnumerable<SocialMediaType> allSocialMediaType = _attributeTypeService.GetAllSocialMediaType();
            IEnumerable<Relationship> allRelationshipType = _attributeTypeService.GetAllRelationship();
            IEnumerable<EventType> allEventType = _attributeTypeService.GetAllEventType();
            IEnumerable<AlertType> allAlertType = _attributeTypeService.GetAllAlertType();
            IEnumerable<ResourceType> allResourceType = _attributeTypeService.GetAllResourceType();
            var AllType = new {allAddressType = allAddressType,
                               allEmailType = allEmailType,
                               allCountryType = allCountryType,
                               allPhoneType = allPhoneType,
                               allSocialMediaType = allSocialMediaType,
                               allRelationshipType = allRelationshipType,
                               allEventType = allEventType,
                               allAlertType = allAlertType,
                               allResourceType = allResourceType
                               };
            return Json(AllType, JsonRequestBehavior.AllowGet);
        }

        //// get attribute type list
        //public JsonResult GetAllAddressType()
        //{
        //    IEnumerable<AddressType> allAddressType = _attributeTypeService.GetAllAddressType();
        //    return Json(allAddressType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllEmailType()
        //{
        //    IEnumerable<EmailType> allEmailType = _attributeTypeService.GetAllEmailType();
        //    return Json(allEmailType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllCountryType()
        //{
        //    IEnumerable<Country> allCountryType = _attributeTypeService.GetAllCountry();
        //    return Json(allCountryType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllPhoneType()
        //{
        //    IEnumerable<PhoneType> allPhoneType = _attributeTypeService.GetAllPhoneType();
        //    return Json(allPhoneType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllSocialMediaType()
        //{
        //    IEnumerable<SocialMediaType> allSocialMediaType = _attributeTypeService.GetAllSocialMediaType();
        //    return Json(allSocialMediaType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllRelationshipType()
        //{
        //    IEnumerable<Relationship> allRelationshipType = _attributeTypeService.GetAllRelationship();
        //    return Json(allRelationshipType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllEventType()
        //{
        //    IEnumerable<EventType> allEventType = _attributeTypeService.GetAllEventType();
        //    return Json(allEventType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllAlertType()
        //{
        //    IEnumerable<AlertType> allAlertType = _attributeTypeService.GetAllAlertType();
        //    return Json(allAlertType, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllResourceType()
        //{
        //    IEnumerable<ResourceType> allResourceType = _attributeTypeService.GetAllResourceType();
        //    return Json(allResourceType, JsonRequestBehavior.AllowGet);
        //}
        //update attributes type
        [HttpPost]
        public JsonResult UpdateAddressType(AddressType addresstype)
        {
            var status = false;
            if (addresstype != null)
            {
                var atype = _attributeTypeService.GetAddressTypeById(addresstype.Id);
                atype.Name = addresstype.Name;
                _attributeTypeService.UpdateAddressType(atype);
                status = true;
                return Json(new { success = status, addresstype = atype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdateEmailType(EmailType emailtype)
        {
            var status = false;
            if (emailtype != null)
            {
                var etype = _attributeTypeService.GetEmailTypeById(emailtype.Id);
                etype.Name = emailtype.Name;
                _attributeTypeService.UpdateEmailType(etype);
                status = true;
                return Json(new { success = status, emailtype = etype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdateCountryType(Country countrytype)
        {
            var status = false;
            if (countrytype != null)
            {
                var ctype = _attributeTypeService.GetCountryById(countrytype.Id);
                ctype.Name = countrytype.Name;
                ctype.CountryCode = countrytype.CountryCode;
                ctype.PhoneCode = countrytype.PhoneCode;
                _attributeTypeService.UpdateCountry(ctype);
                status = true;
                return Json(new { success = status, countrytype = ctype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdatePhoneType(PhoneType phonetype)
        {
            var status = false;
            if (phonetype != null)
            {
                var ptype = _attributeTypeService.GetPhoneTypeById(phonetype.Id);
                ptype.Name = phonetype.Name;
                _attributeTypeService.UpdatePhoneType(ptype);
                status = true;
                return Json(new { success = status, phonetype = ptype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdateSocialMediaType(SocialMediaType socialmediatype)
        {
            var status = false;
            if (socialmediatype != null)
            {
                var stype = _attributeTypeService.GetSocialMediaTypeById(socialmediatype.Id);
                stype.Name = socialmediatype.Name;
                _attributeTypeService.UpdateSocialMediaType(stype);
                status = true;
                return Json(new { success = status, socialmediatype = stype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdateRelationshipType(Relationship relationshiptype)
        {
            var status = false;
            if (relationshiptype != null)
            {
                var rtype = _attributeTypeService.GetRelationshipById(relationshiptype.Id);
                rtype.RelationshipToYou = relationshiptype.RelationshipToYou;
                _attributeTypeService.UpdateRelationship(rtype);
                status = true;
                return Json(new { success = status, relationshiptype = rtype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdateEventType(EventType eventtype)
        {
            var status = false;
            if (eventtype != null)
            {
                var etype = _attributeTypeService.GetEventTypeById(eventtype.Id);
                etype.Name = eventtype.Name;
                etype.Description = eventtype.Description;
                _attributeTypeService.UpdateEventType(etype);
                status = true;
                return Json(new { success = status, eventtype = etype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdateAlertType(AlertType alerttype)
        {
            var status = false;
            if (alerttype != null)
            {
                var atype = _attributeTypeService.GetAlertTypeById(alerttype.Id);
                atype.Name = alerttype.Name;
                _attributeTypeService.UpdateAlertType(atype);
                status = true;
                return Json(new { success = status, alerttype = atype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult UpdateResourceType(ResourceType resourcetype)
        {
            var status = false;
            if (resourcetype != null)
            {
                var rtype = _attributeTypeService.GetResourceTypeById(resourcetype.Id);
                rtype.Name = resourcetype.Name;
                _attributeTypeService.UpdateResourceType(rtype);
                status = true;
                return Json(new { success = status, resourcetype = rtype });
            }
            return Json(new { success = status });
        }

        //Create attributes type
        [HttpPost]
        public JsonResult CreateAddressType(AddressType addresstype)
        {
            var status = false;
            if (addresstype != null)
            {
                _attributeTypeService.CreateAddressType(addresstype);
                status = true;
                //return Json(new { success = status, Id = addresstype.Id});
                return Json(new { success = status, addresstype = addresstype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreateEmailType(EmailType emailtype)
        {
            var status = false;
            if (emailtype != null)
            {
                _attributeTypeService.CreateEmailType(emailtype);
                status = true;
                return Json(new { success = status, emailtype = emailtype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreateCountryType(Country countrytype)
        {
            var status = false;
            if (countrytype != null)
            {
                _attributeTypeService.CreateCountry(countrytype);
                status = true;
                return Json(new { success = status, countrytype = countrytype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreatePhoneType(PhoneType phonetype)
        {
            var status = false;
            if (phonetype != null)
            {
                _attributeTypeService.CreatePhoneType(phonetype);
                status = true;
                return Json(new { success = status, phonetype = phonetype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreateSocialMediaType(SocialMediaType socialmediatype)
        {
            var status = false;
            if (socialmediatype != null)
            {
                _attributeTypeService.CreateSocialMediaType(socialmediatype);
                status = true;
                return Json(new { success = status, socialmediatype = socialmediatype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreateRelationshipType(Relationship relationshiptype)
        {
            var status = false;
            if (relationshiptype != null)
            {
                _attributeTypeService.CreateRelationship(relationshiptype);
                status = true;
                return Json(new { success = status, relationshiptype = relationshiptype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreateEventType(EventType eventtype)
        {
            var status = false;
            if (eventtype != null)
            {
                _attributeTypeService.CreateEventType(eventtype);
                status = true;
                return Json(new { success = status, eventtype = eventtype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreateAlertType(AlertType alerttype)
        {
            var status = false;
            if (alerttype != null)
            {
                _attributeTypeService.CreateAlertType(alerttype);
                status = true;
                return Json(new { success = status, alerttype = alerttype });
            }
            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult CreateResourceType(ResourceType resourcetype)
        {
            var status = false;
            if (resourcetype != null)
            {
                _attributeTypeService.CreateResourceType(resourcetype);
                status = true;
                return Json(new { success = status, resourcetype = resourcetype });
            }
            return Json(new { success = status });
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