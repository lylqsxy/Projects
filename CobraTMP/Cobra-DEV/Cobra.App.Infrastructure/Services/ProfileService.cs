using Cobra.App.Infrastructure.Contracts;
using Cobra.Infrastructure;
using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {

        private IRepository<Profile> _profileRepository;
        // Author: Aaron Bhardwaj
        private IRepository<Person> _personRepository;
        private IRepository<Phone> _phoneRepository;
        private IRepository<Address> _addressRepository;
        private IRepository<Email> _emailRepository;
        private IRepository<EmergencyContact> _emergencyContactRepository;

        // Author: Aaron Bhardwaj
        public ProfileService(IRepository<Profile> profileRepository, IRepository<Person> personRepository, IRepository<Phone> phoneRepository, IRepository<Address> addressRepository, IRepository<Email> emailRepository, IRepository<EmergencyContact> emergencyContactRepository)
        {
            _profileRepository = profileRepository;
            _personRepository = personRepository;
            _phoneRepository = phoneRepository;
            _addressRepository = addressRepository;
            _emailRepository = emailRepository;
            _emergencyContactRepository = emergencyContactRepository;
        }

        #region ProfileService Members

        #region ProfileRepository Members

        public int CreateProfile(Profile profile)
        {
            _profileRepository.Add(profile, true);
            return profile.Id;
        }

        public Profile GetProfileById(int id)
        {
            return _profileRepository.GetById(id);
        }

        public void UpdateProfile(Profile profile)
        {
            _profileRepository.Update(profile, true);
            _profileRepository.Save();
        }

        public void DeleteProfile(int id)
        {
            _profileRepository.Remove(GetProfileById(id), true);
        }

        public bool ValidateProfile(Profile profile)
        {
            return GetProfileById(profile.Id) == null ? false : true;
        }

        public IEnumerable<Profile> GetAllProfile()
        {
            return _profileRepository.GetAll().ToList();
        }

        #endregion

        #region PersonRepository Members

        public bool CreatePerson(Person person)
        {
            return _personRepository.Add(person, true) == null ? false : true;
        }

        public Person GetPersonById(int id)
        {
            return _personRepository.GetById(id);
        }

        public void UpdatePerson(Person person)
        {
            _personRepository.Update(person, true);
            _personRepository.Save();
        }

        public void DeletePerson(int id)
        {
            _personRepository.Remove(GetPersonById(id), true);
        }

        #endregion

        #region PhoneRepository Members

        public bool CreatePhone(Phone phone)
        {
            return _phoneRepository.Add(phone, true) == null ? false : true;
        }

        public Phone GetPhoneById(int id)
        {
            return _phoneRepository.GetById(id);
        }

        public IEnumerable<Phone> GetPhoneByProfileId(int id)
        {
            return _phoneRepository.Get(x => x.ProfileId == id);
        }

        public void UpdatePhone(Phone phone)
        {
            _phoneRepository.Update(phone, true);
            _phoneRepository.Save();
        }

        public void DeletePhone(int id)
        {
            _phoneRepository.Remove(GetPhoneById(id), true);
            _phoneRepository.Save();
        }

        #endregion

        #region EmailRepository Members

        public bool CreateEmail(Email email)
        {
            return _emailRepository.Add(email, true) == null ? false : true;
        }

        public Email GetEmailById(int id)
        {
            return _emailRepository.GetById(id);
        }

        public IEnumerable<Email> GetEmailByProfileId(int id)
        {
            return _emailRepository.Get(x => x.ProfileId == id);
        }

        public void UpdateEmail(Email email)
        {
            _emailRepository.Update(email, true);
            _emailRepository.Save();
        }

        public void DeleteEmail(int id)
        {
            _emailRepository.Remove(GetEmailById(id), true);
            _emailRepository.Save();
        }

        #endregion

        #region AddressRepository Members

        public bool CreateAddress(Address address)
        {
            return _addressRepository.Add(address, true) == null ? false : true;
        }

        public Address GetAddressById(int id)
        {
            return _addressRepository.GetById(id);
        }

        public IEnumerable<Address> GetAddressByProfileId(int id)
        {
            return _addressRepository.Get(x => x.ProfileId == id);
        }

        public void UpdateAddress(Address address)
        {
            _addressRepository.Update(address, true);
            _addressRepository.Save();
        }

        public void DeleteAddress(int id)
        {
            _addressRepository.Remove(GetAddressById(id), true);
            _addressRepository.Save();
        }

        #endregion

        #region EmergencyContactRepository Members

        public bool CreateEmergencyContact(EmergencyContact emergencyContact)
        {
            return _emergencyContactRepository.Add(emergencyContact, true) == null ? false : true;
        }

        public EmergencyContact GetEmergencyContactById(int id)
        {
            return _emergencyContactRepository.GetById(id);
        }

        public void UpdateEmergencyContact(EmergencyContact emergencyContact)
        {
            _emergencyContactRepository.Update(emergencyContact, true);
            _emergencyContactRepository.Save();
        }

        public void DeleteEmergencyContact(int id)
        {
            _emergencyContactRepository.Remove(GetEmergencyContactById(id), true);
        }

        public IEnumerable<EmergencyContact> GetEmergencyContactByPersonId(int id)
        {
            return _emergencyContactRepository.Get(x => x.PersonId == id);
        }

        #endregion

        #endregion
    }
}
