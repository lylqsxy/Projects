using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IProfileService
    {
        #region CRUD 
        int CreateProfile(Profile profile); //To return ProfileId
        Profile GetProfileById(int id);
        void UpdateProfile(Profile update);
        void DeleteProfile(int id);

        bool CreatePerson(Person person);
        Person GetPersonById(int id);
        void UpdatePerson(Person person);
        void DeletePerson(int id);

        bool CreatePhone(Phone phone);
        Phone GetPhoneById(int id);
        IEnumerable<Phone> GetPhoneByProfileId(int id);
        void UpdatePhone(Phone phone);
        void DeletePhone(int id);

        bool CreateEmail(Email email);
        Email GetEmailById(int id);
        IEnumerable<Email> GetEmailByProfileId(int id);
        void UpdateEmail(Email email);
        void DeleteEmail(int id);

        bool CreateAddress(Address address);
        Address GetAddressById(int id);
        IEnumerable<Address> GetAddressByProfileId(int id);
        void UpdateAddress(Address address);
        void DeleteAddress(int id);

        bool CreateEmergencyContact(EmergencyContact emergencyContact);
        EmergencyContact GetEmergencyContactById(int id);
        void UpdateEmergencyContact(EmergencyContact emergencyContact);
        void DeleteEmergencyContact(int id);
        IEnumerable<EmergencyContact> GetEmergencyContactByPersonId(int id);

        // service for social media type
        //bool CreateSocialMediaType(SocialMediaType socialMediaType);


        // service for social media 
        bool CreateSocialMedia(SocialMedia socialMedia);
        SocialMedia GetSocialMediaById(int id);
        IEnumerable<SocialMedia> GetSocialMediaByProfileId(int id);
        void DeleteSocialMedia(int id);
       // IEnumerable<SocialMedia> GetAllSocialMedia();
     //   SocialMedia GetSocialMediaByProfileId() 

        #endregion

        #region Other Method
        bool ValidateProfile(Profile profile);
        IEnumerable<Profile> GetAllProfile();
        #endregion
    }
}
