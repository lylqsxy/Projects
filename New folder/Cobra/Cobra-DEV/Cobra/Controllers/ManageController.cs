using Cobra.App.Infrastructure.Contracts;
using Cobra.Filters;
using Cobra.Infrastructure.Contracts;
using Cobra.Model;
using Cobra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Cobra.Controllers
{
    //[RequireHttps]
    //[Authorize]
    public class ManageController : Controller
    {
        private IProfileService _profileService;
        private ILogService _logService;
        private IAccountService _accountService; //Required to GetCurrentUser-Id & Email
        private IAttributeTypeService _attributeTypeService;

        // Author: Aaron Bhardwaj
        public ManageController(IProfileService profileService, ILogService logService, IAccountService accountService, IAttributeTypeService attributeTypeService)
        {
            _profileService = profileService;
            _logService = logService;
            _accountService = accountService;
            _attributeTypeService = attributeTypeService;
        }

        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        #region Profile

        public ActionResult UserProfile()
        {
            return View();
        }

        // GET: /Manage/CreateProfile
        [HttpGet]
        public ActionResult CreateProfile()
        {
            var user = _accountService.GetCurrentUser();
            var person = _profileService.GetPersonById(user.Id);
            if (ReferenceEquals(person, null))
            {
                //Create blank profile
                int newProfile = _profileService.CreateProfile(
                    new Profile
                    {
                        IsActive = user.IsActive,
                        CreatedOn = DateTime.Now,
                        CreatedBy = 0
                    });

                //Create blank person
                var newPerson = _profileService.CreatePerson(
                    new Person
                    {
                        Id = user.Id,
                        OrgId = 3,                  //This needs to be updated from Organisation Table
                        ProfileId = newProfile,
                        CreatedOn = DateTime.Now,
                        CreatedBy = 0,
                        IsActive = user.IsActive
                    });

                //Create blank phone
                var newPhone = _profileService.CreatePhone(
                    new Phone
                    {
                        PhoneTypeId = _attributeTypeService.GetAllPhoneType().FirstOrDefault().Id,
                        ProfileId = newProfile,
                        CountryId = _attributeTypeService.GetAllCountry().FirstOrDefault().Id,
                        IsMobile = true,
                        IsPrimary = true
                    });

                //Create blank email
                var newEmail = _profileService.CreateEmail(
                    new Email
                    {
                        ProfileId = newProfile,
                        EmailTypeId = _attributeTypeService.GetAllEmailType().FirstOrDefault().Id,
                        Email1 = user.Email,
                        CreatedOn = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedOn = DateTime.Now,
                        UpdatedBy = 0,
                        IsPrimary = true
                    });

                //Create blank address
                var newAddress = _profileService.CreateAddress(
                    new Address
                    {
                        AddressTypeId = _attributeTypeService.GetAllAddressType().FirstOrDefault().Id,
                        ProfileId = newProfile,
                        IsPrimary = true
                    });
            }

            //Get latest profile
            var profileId = _profileService.GetPersonById(user.Id).ProfileId;
            var latestProfile = _profileService.GetProfileById(profileId);
            var latestPerson = _profileService.GetPersonById(user.Id);
            List<PhoneModel> latestPhone = _profileService.GetPhoneByProfileId(profileId)
                                                        .Select(x => new PhoneModel
                                                        {
                                                            Number = x.Number,
                                                            PhoneTypeId = x.PhoneTypeId,
                                                            CountryId = (int)x.CountryId,
                                                            IsMobile = x.IsMobile,
                                                            IsPrimary = x.IsPrimary,
                                                            Id = x.Id
                                                        }).ToList();
            List<EmailModel> latestEmail = _profileService.GetEmailByProfileId(profileId)
                                                        .Select(x => new EmailModel
                                                        {
                                                            Email = x.Email1,
                                                            EmailTypeId = x.EmailTypeId,
                                                            IsPrimary = x.IsPrimary,
                                                            Id = x.Id
                                                        }).ToList();
            List<AddressModel> latestAddress = _profileService.GetAddressByProfileId(profileId)
                                                        .Select(x => new AddressModel
                                                        {
                                                            AddressTypeId = x.AddressTypeId,
                                                            City = x.City,
                                                            Country = x.Country,
                                                            IsPrimary = (bool)x.IsPrimary,
                                                            ProfileId = x.ProfileId,
                                                            State = x.State,
                                                            StreetName = x.StreetName,
                                                            StreetNumber = x.StreetNumber,
                                                            Suburb = x.Suburb,
                                                            ZipCode = x.ZipCode,
                                                            Id = x.Id
                                                        }).ToList();

            var model = new ProfileViewModel()
            {
                ProfileModel = new ProfileModel
                {
                    Id = latestProfile.Id,
                    FirstName = latestProfile.FirstName,
                    MiddleName = latestProfile.MiddleName,
                    LastName = latestProfile.LastName
                },
                PersonModel = new PersonModel
                {
                    Id = latestPerson.Id,
                    DoB = latestPerson.DoB,
                    Gender = latestPerson.Gender
                },
                PhoneModel = new List<PhoneModel>(latestPhone),
                EmailModel = new List<EmailModel>(latestEmail),
                AddressModel = new List<AddressModel>(latestAddress)
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: /Manage/UpdateProfile
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult UpdateProfile(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                //Update Profile
                var profile = _profileService.GetProfileById(profileViewModel.ProfileModel.Id);
                profile.FirstName = profileViewModel.ProfileModel.FirstName;
                profile.LastName = profileViewModel.ProfileModel.LastName;
                profile.MiddleName = profileViewModel.ProfileModel.MiddleName;
                _profileService.UpdateProfile(profile);

                //Update Person
                var person = _profileService.GetPersonById(profileViewModel.PersonModel.Id);
                person.Gender = profileViewModel.PersonModel.Gender;
                person.DoB = profileViewModel.PersonModel.DoB;
                person.UpdatedOn = DateTime.Now;
                person.UpdateBy = 0;
                _profileService.UpdatePerson(person);

                //Update Phone(s)
                var phone = new Phone();
                foreach (var item in profileViewModel.PhoneModel)
                {
                    //if id == 0 : CREATE
                    if (item.Id == 0)
                    {
                        var newPhone = _profileService.CreatePhone(
                            new Phone
                            {
                                PhoneTypeId = item.PhoneTypeId,
                                CountryId = item.CountryId,
                                ProfileId = profileViewModel.ProfileModel.Id,
                                Number = item.Number,
                                IsMobile = item.IsMobile,
                                IsPrimary = item.IsPrimary
                            });
                    }
                    //Update
                    else
                    {
                        phone = _profileService.GetPhoneById(item.Id);
                        phone.PhoneTypeId = item.PhoneTypeId;
                        phone.CountryId = item.CountryId;
                        phone.Number = item.Number;
                        phone.IsMobile = item.IsMobile;
                        phone.IsPrimary = item.IsPrimary;
                        _profileService.UpdatePhone(phone);
                    }
                }

                //Update Email(s)
                var email = new Email();
                foreach (var item in profileViewModel.EmailModel)
                {
                    //if id == 0 : CREATE
                    if (item.Id == 0)
                    {
                        var newEmail = _profileService.CreateEmail(
                            new Email
                            {
                                EmailTypeId = item.EmailTypeId,
                                ProfileId = profileViewModel.ProfileModel.Id,
                                Email1 = item.Email,
                                CreatedOn = DateTime.Now,
                                CreatedBy = 0,
                                UpdatedOn = DateTime.Now,
                                UpdatedBy = 0,
                                IsPrimary = item.IsPrimary
                            });
                    }
                    //Update
                    else
                    {
                        email = _profileService.GetEmailById(item.Id);
                        email.EmailTypeId = item.EmailTypeId;
                        email.Email1 = item.Email;
                        email.UpdatedOn = DateTime.Now;
                        email.UpdatedBy = 0;
                        email.IsPrimary = item.IsPrimary;
                        _profileService.UpdateEmail(email);
                    }
                }

                //Update Address(es)
                var address = new Address();
                foreach (var item in profileViewModel.AddressModel)
                {
                    //if id == 0 : CREATE
                    if (item.Id == 0)
                    {
                        var newAddress = _profileService.CreateAddress(
                            new Address
                            {
                                AddressTypeId = item.AddressTypeId,
                                ProfileId = profileViewModel.ProfileModel.Id,
                                StreetNumber = item.StreetNumber,
                                StreetName = item.StreetName,
                                Suburb = item.Suburb,
                                City = item.City,
                                State = item.State,
                                Country = item.Country,
                                ZipCode = item.ZipCode,
                                IsPrimary = item.IsPrimary
                            });
                    }
                    //Update
                    else
                    {
                        address = _profileService.GetAddressById(item.Id);
                        address.AddressTypeId = item.AddressTypeId;
                        address.StreetNumber = item.StreetNumber;
                        address.StreetName = item.StreetName;
                        address.Suburb = item.Suburb;
                        address.City = item.City;
                        address.State = item.State;
                        address.Country = item.Country;
                        address.ZipCode = item.ZipCode;
                        address.IsPrimary = item.IsPrimary;
                        _profileService.UpdateAddress(address);
                    }
                }
                return Json(new
                {
                    Success = true,
                    MsgText = "Profile updated.",
                    RedirectUrl = "Index"
                });
            }
            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
            return Json(new
            {
                Success = false,
                MsgText = "Profile was not updated.",
                Errors = errorList
            });
        }

        //TODO
        // POST: /Manage/DeleteProfile
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult DeleteProfile(ProfileViewModel profileViewModel)
        {
            //Update Login
            var user = _accountService.GetCurrentUser();


            //Update Profile
            var profile = _profileService.GetProfileById(profileViewModel.ProfileModel.Id);
            profile.IsActive = false;
            _profileService.UpdateProfile(profile);

            //Update Person
            var person = _profileService.GetPersonById(profileViewModel.PersonModel.Id);
            person.IsActive = false;
            _profileService.UpdatePerson(person);

            //Delete Phone
            //foreach (var phone in profileViewModel.PhoneModel)
            //{
            //    _profileService.DeletePhone(phone.Id);
            //}

            //Delete Email
            //foreach (var email in profileViewModel.EmailModel)
            //{
            //    _profileService.DeletePhone(email.Id);
            //}

            //Delete Address
            //foreach (var address in profileViewModel.AddressModel)
            //{
            //    _profileService.DeletePhone(address.Id);
            //}

            //Update EmergencyContact, and Profile. Delete Phone, Email, and Address
            var eContact = _profileService.GetEmergencyContactByPersonId(profileViewModel.PersonModel.Id).ToList();
            foreach (var item in eContact)
            {
                item.IsActive = false;
                
                var eProfile = _profileService.GetProfileById(item.ProfileId);
                profile.IsActive = false;

                //Delete Phone
                //var ePhone = _profileService.GetPhoneByProfileId(item.ProfileId).ToList();
                //foreach (var phone in ePhone)
                //{
                //    _profileService.DeletePhone(phone.Id);
                //}

                //Delete Email
                //var eEmail = _profileService.GetEmailByProfileId(item.ProfileId).ToList();
                //foreach (var email in eEmail)
                //{
                //    _profileService.DeleteEmail(email.Id);
                //}

                //Delete Address
                //var eAddress = _profileService.GetAddressByProfileId(item.ProfileId).ToList();
                //foreach (var address in eAddress)
                //{
                //    _profileService.DeleteAddress(address.Id);
                //}

                _profileService.UpdateProfile(eProfile);
                _profileService.UpdateEmergencyContact(item);                
            }

            return RedirectToAction("Account", "LogOff");
        }

        // POST: /Manage/DeletePhone
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult DeletePhone(int id)
        {
            var phone = _profileService.GetPhoneById(id);
            if (!ReferenceEquals(phone, null))
            {
                _profileService.DeletePhone(id);
                return Json(new
                {
                    Success = true,
                    MsgText = "Phone deleted."
                });
            }
            return Json(new
            {
                Success = false,
                MsgText = "No phone was deleted."
            });
        }

        // POST: /Manage/DeleteEmail
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult DeleteEmail(int id)
        {
            var email = _profileService.GetEmailById(id);
            if (!ReferenceEquals(email, null))
            {
                _profileService.DeleteEmail(id);
                return Json(new
                {
                    Success = true,
                    MsgText = "Email deleted."
                });
            }
            return Json(new
            {
                Success = false,
                MsgText = "No email was deleted."
            });
        }

        // POST: /Manage/DeleteAddress
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult DeleteAddress(int id)
        {
            var address = _profileService.GetAddressById(id);
            if (!ReferenceEquals(address, null))
            {
                _profileService.DeleteAddress(id);
                return Json(new
                {
                    Success = true,
                    MsgText = "Address deleted."
                });
            }
            return Json(new
        {
                Success = false,
                MsgText = "No address was deleted."
            });
        }

        #endregion

        #region EmergencyContact

        [HttpGet]
        public ActionResult EmergencyContact()
        {
            return View(); 
        }
     
        [HttpGet]
        public JsonResult GetEmergencyContacts()
        {
            int loginUserId = _accountService.GetCurrentUserId();
            Person loginPerson = _profileService.GetPersonById(loginUserId);

            List<EmergencyContactViewModel> ecViewList = _profileService.GetEmergencyContactByPersonId(loginPerson.Id)
                                                        .Where(p => p.Profile.IsActive == true)
                                                        .Select(ec => new EmergencyContactViewModel
                                                        {
                                                            Id = ec.Id,
                                                            Firstname = ec.Profile.FirstName,
                                                            Middlename = ec.Profile.MiddleName,
                                                            Lastname = ec.Profile.LastName,
                                                            RelationshipID = ec.RelationshipId,
                                                            Priority = ec.Priority,
                                                            Reason = ec.ReasonContact,
                                                            PhoneList = ec.Profile.Phones.
                                                                        Select(p => new PhoneViewModel
                                                                        {
                                                                            Id = p.Id,
                                                                            Number = p.Number,
                                                                            PhoneTypeID = p.PhoneTypeId,
                                                                            IsMobile = p.IsMobile,
                                                                            IsPrimary = p.IsPrimary,
                                                                            CountryID = p.CountryId
                                                                        }).ToList()
                                                        }).ToList();


            return Json(ecViewList, JsonRequestBehavior.AllowGet);
        }
         
        [HttpGet]
        public ActionResult CreateEmergencyContact()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public JsonResult CreateEmergencyContact(EmergencyContactViewModel newEmergencyContact)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(newEmergencyContact);
            //}
            var user = _accountService.GetCurrentUser();
            Person appPerson = _profileService.GetPersonById(user.Id);

            //Given Profile has been created

            ////PHONE(S)
            List<Phone> phoneModel = new List<Phone>();
            //try
            //{
            //    foreach (var ecPhone in newEmergencyContact.PhoneList)
            //    {
            //        Phone phone = new Phone
            //        {
            //            //  Profile = ecProfile,
            //            PhoneTypeId = ecPhone.PhoneTypeID,
            //            Number = ecPhone.Number,
            //            CountryId = ecPhone.CountryID,
            //            IsMobile = ecPhone.IsMobile,
            //            IsPrimary = ecPhone.IsPrimary,
            //        };
            //        phoneModel.Add(phone);
            //    }
            //    //TODO: add emails, Adresses... 
            //}catch(Exception ex)
            //{
            //    _logService.Error(ex.Message);
            //    HttpStatusCodeResult error = new HttpStatusCodeResult(HttpStatusCode.Forbidden, "exeption when in phoneList"); //Nicky
            //    return Json(error, JsonRequestBehavior.AllowGet);                               //Nicky
            //}

            //Create new emergency contact mode
            //Emergency contact profile
            Profile ecProfile = new Profile
            {
                FirstName = newEmergencyContact.Firstname,
                MiddleName = newEmergencyContact.Middlename,
                LastName = newEmergencyContact.Lastname,
                IsActive = true,
                CreatedOn = DateTime.Now,
                CreatedBy = appPerson.Id,
                Phones = phoneModel
            };

            //Emergency Contact 
            EmergencyContact newEC = new EmergencyContact
            {
                Profile = ecProfile,
                PersonId = appPerson.Id,
                RelationshipId = newEmergencyContact.RelationshipID,
                Priority = newEmergencyContact.Priority,
                ReasonContact = newEmergencyContact.Reason,
                CreatedOn = DateTime.Now,
                CreatedBy = appPerson.Id,
                IsActive = true
            };

            bool created = _profileService.CreateEmergencyContact(newEC);

            //wrap emergency contact model to send it back to the view
            //get id of the created Emergency Contact model, and attached to the view
            newEmergencyContact.Id = newEC.Id;
            var createdPhoneList = newEC.Profile.Phones.Select(p => new PhoneViewModel
            {
                Id = p.Id,
                PhoneTypeID = p.PhoneTypeId,
                Number = p.Number,
                CountryID = p.CountryId,
                IsMobile = p.IsMobile,
                IsPrimary = p.IsPrimary
            }).ToList();

            newEmergencyContact.PhoneList = createdPhoneList;
            HttpStatusCodeResult success = new HttpStatusCodeResult(HttpStatusCode.OK);         //Nicky
            return Json(new { newEmergencyContact, success }, JsonRequestBehavior.AllowGet);    //Nicky
            //return new HttpStatusCodeResult(HttpStatusCode.OK); 
        }

        [HttpGet] 
        public ViewResult UpdateEmergencyContact()
        {
            return View(); 
        }

        [HttpPost]
        public JsonResult EditEmergencyContact(EmergencyContactViewModel contactToUpdate)
        {
            //int loginID = _accountService.GetCurrentUserId();
            //Person loginUser = _profileService.GetPersonById(loginID);

            EmergencyContact ecModel = _profileService.GetEmergencyContactById(contactToUpdate.Id);
            //**emergency contact update
            try
            {
                ecModel.RelationshipId = contactToUpdate.RelationshipID;
            } catch(Exception ex)
            {
                //todo log exception 
                HttpStatusCodeResult error = new HttpStatusCodeResult(HttpStatusCode.Forbidden);        //Nicky
                return Json(error, JsonRequestBehavior.AllowGet);                                       //Nicky
            }

            ecModel.ReasonContact = contactToUpdate.Reason;
            ecModel.Priority = contactToUpdate.Priority;
            //TODO: add missing updatedOn and updateBy to emergency contact table

            //**update profile 
            ecModel.Profile.FirstName = contactToUpdate.Firstname;
            ecModel.Profile.MiddleName = contactToUpdate.Middlename;
            ecModel.Profile.LastName = contactToUpdate.Lastname;

            //**update profile phones
            foreach (PhoneViewModel phoneView in contactToUpdate.PhoneList)
            {
                Phone phoneToUpdate = _profileService.GetPhoneById(phoneView.Id);

                if (phoneToUpdate != null)
                {
                    //update phone
                    phoneToUpdate.PhoneTypeId = phoneView.PhoneTypeID;
                    phoneToUpdate.Number = phoneView.Number;
                    phoneToUpdate.IsMobile = phoneView.IsMobile;
                    phoneToUpdate.IsPrimary = phoneView.IsPrimary;
                    phoneToUpdate.CountryId = phoneView.CountryID;
                }
                else
                {
                    //add new phone 
                    Phone newPhone = new Phone
                    {
                       Id = phoneView.Id,
                       Number = phoneView.Number,
                       IsPrimary = phoneView.IsPrimary,
                       IsMobile = phoneView.IsMobile,
                       PhoneTypeId = phoneView.PhoneTypeID,
                       CountryId = phoneView.CountryID
                    };
                    ecModel.Profile.Phones.Add(newPhone); 
                }
            }

            _profileService.UpdateEmergencyContact(ecModel);

            //wrap emergency contact model to send it back to the view
            //get id of the created Emergency Contact model, and attached to the view
            
            var updatedPhoneList = ecModel.Profile.Phones.Select(p => new PhoneViewModel          //Nicky
            {
                Id = p.Id,
                PhoneTypeID = p.PhoneTypeId,
                Number = p.Number,
                CountryID = p.CountryId,
                IsMobile = p.IsMobile,
                IsPrimary = p.IsPrimary
            }).ToList();

            contactToUpdate.PhoneList = updatedPhoneList;                                       //Nicky
            HttpStatusCodeResult success = new HttpStatusCodeResult(HttpStatusCode.OK);         //Nicky
            return Json(new { contactToUpdate, success }, JsonRequestBehavior.AllowGet);        //Nicky

            //return new HttpStatusCodeResult(HttpStatusCode.OK);  //:)                         //Nicky
        }

        [HttpPost]
        public JsonResult DeleteEmergencyContact(EmergencyContactViewModel contactToDelete)
        {
            EmergencyContact ecModel = _profileService.GetEmergencyContactById(contactToDelete.Id);
            //deactive profile and the emergency record itself 
            ecModel.Profile.IsActive = false;
            _profileService.UpdateEmergencyContact(ecModel); 

            return Json(new { Success = true, Msg = "pass" }); 
        }

        #endregion

        #region GetDropDownLists

        [HttpGet]
        public ActionResult GetAddressType()
        {
            var model = _attributeTypeService.GetAllAddressType().Select(x => new { Id = x.Id, Name = x.Name }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEmailType()
        {
            var model = _attributeTypeService.GetAllEmailType().Select(x => new { Id = x.Id, Name = x.Name }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCountry()
        {
            var model = _attributeTypeService.GetAllCountry().Select(x => new { Id = x.Id, Name = x.Name, CountryCode = x.CountryCode, PhoneCode = x.PhoneCode }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
       
        /// <summary>
        /// Get all predefined dropdown list for CreateEmergencyContact view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEmergencyContactAttributes()
        {
            ECDropdownViewModel dropdown = new ECDropdownViewModel();
             dropdown.PhoneTypes = _attributeTypeService.GetAllPhoneType().
                Select(p => new PhoneTypeViewModel { ID = p.Id, PhoneType = p.Name }).
                ToList(); 

            dropdown.Relationships = _attributeTypeService.GetAllRelationship().
                Select(r => new RelationshipViewModel { ID = r.Id, Relationship = r.RelationshipToYou }).
                ToList();

            dropdown.PhoneCountries = _attributeTypeService.GetAllCountry()
               .Select(p => new PhoneCountryViewModel
               {
                   ID = p.Id,
                   Name = p.Name,
                   CountryCode = p.CountryCode,
                   PhoneCode = p.PhoneCode
               }).ToList(); 
            return Json(dropdown, JsonRequestBehavior.AllowGet); 
        }

        [HttpGet]
        public ActionResult GetPhoneType()
        {
            var model = _attributeTypeService.GetAllPhoneType().Select(x => new { Id = x.Id, Name = x.Name }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRelationship()
        {
            var model = _attributeTypeService.GetAllRelationship().Select(x => new { Id = x.Id, RelationshipToYou = x.RelationshipToYou }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}