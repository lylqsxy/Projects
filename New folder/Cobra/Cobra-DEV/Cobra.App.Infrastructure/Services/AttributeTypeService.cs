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
    public class AttributeTypeService : IAttributeTypeService
    {
        #region Private Members 

        private IRepository<AddressType> _addressTypeRepository;
        private IRepository<EmailType> _emailTypeRepository;
        private IRepository<Country> _countryRepository;
        private IRepository<SocialMediaType> _socialMediaTypeRepository;
        private IRepository<PhoneType> _phoneTypeRepository;
        private IRepository<Relationship> _relationshipRepository;
        private IRepository<EventType> _eventTypeRepository;
        private IRepository<AlertType> _alertTypeRepository;
        private IRepository<ResourceType> _resourceTypeRepository;

        #endregion

        public AttributeTypeService(IRepository<AddressType> addressTypeRepository,
                                    IRepository<EmailType> emailTypeRepository,
                                    IRepository<Country> countryRepository,
                                    IRepository<SocialMediaType> socialMediaTypeRepository,
                                    IRepository<PhoneType> phoneTypeRepository,
                                    IRepository<Relationship> relationshipRepository,
                                    IRepository<EventType> eventTypeRepository,
                                    IRepository<AlertType> alertTypeRepository,
                                    IRepository<ResourceType> resourceTypeRepository)
        {
            _addressTypeRepository = addressTypeRepository;
            _emailTypeRepository = emailTypeRepository;
            _countryRepository = countryRepository;
            _socialMediaTypeRepository = socialMediaTypeRepository;
            _phoneTypeRepository = phoneTypeRepository;
            _relationshipRepository = relationshipRepository;
            _eventTypeRepository = eventTypeRepository;
            _alertTypeRepository = alertTypeRepository;
            _resourceTypeRepository = resourceTypeRepository;
        }

        #region AddressType

        public bool CreateAddressType(AddressType addressType)
        {
            return _addressTypeRepository.Add(addressType, true) == null ? false : true;
        }
        public AddressType GetAddressTypeById(int id)
        {
            return _addressTypeRepository.GetById(id);
        }
        public void UpdateAddressType(AddressType addressType)
        {
            _addressTypeRepository.Update(addressType, true);
            _addressTypeRepository.Save();
        }
        public void DeleteAddressType(int id)
        {
            _addressTypeRepository.Remove(id, true);
        }
        public IEnumerable<AddressType> GetAllAddressType()
        {
            return _addressTypeRepository.GetAll();
        }

        #endregion

        #region EmailType

        public bool CreateEmailType(EmailType emailType)
        {
            return _emailTypeRepository.Add(emailType, true) == null ? false : true;
        }
        public EmailType GetEmailTypeById(int id)
        {
            return _emailTypeRepository.GetById(id);
        }
        public void UpdateEmailType(EmailType emailType)
        {
            _emailTypeRepository.Update(emailType, true);
            _emailTypeRepository.Save();
        }
        public void DeleteEmailType(int id)
        {
            _emailTypeRepository.Remove(id, true);
        }
        public IEnumerable<EmailType> GetAllEmailType()
        {
            return _emailTypeRepository.GetAll();
        }

        #endregion

        #region Country

        public bool CreateCountry(Country country)
        {
            return _countryRepository.Add(country, true) == null ? false : true;
        }
        public Country GetCountryById(int id)
        {
            return _countryRepository.GetById(id);
        }
        public void UpdateCountry(Country country)
        {
            _countryRepository.Update(country, true);
            _countryRepository.Save();
        }
        public void DeleteCountry(int id)
        {
            _countryRepository.Remove(id, true);
        }
        public IEnumerable<Country> GetAllCountry()
        {
            return _countryRepository.GetAll();
        }

        #endregion

        #region SocialMediaType

        public bool CreateSocialMediaType(SocialMediaType socialMediaType)
        {
            return _socialMediaTypeRepository.Add(socialMediaType, true) == null ? false : true;
        }
        public SocialMediaType GetSocialMediaTypeById(int id)
        {
            return _socialMediaTypeRepository.GetById(id);
        }
        public void UpdateSocialMediaType(SocialMediaType socialMediaType)
        {
            _socialMediaTypeRepository.Update(socialMediaType, true);
            _socialMediaTypeRepository.Save();
        }
        public void DeleteSocialMediaType(int id)
        {
            _socialMediaTypeRepository.Remove(id, true);
        }
        public IEnumerable<SocialMediaType> GetAllSocialMediaType()
        {
            return _socialMediaTypeRepository.GetAll();
        }

        #endregion

        #region PhoneType

        public bool CreatePhoneType(PhoneType phoneType)
        {
            return _phoneTypeRepository.Add(phoneType, true) == null ? false : true;
        }
        public PhoneType GetPhoneTypeById(int id)
        {
            return _phoneTypeRepository.GetById(id);
        }
        public void UpdatePhoneType(PhoneType phoneType)
        {
            _phoneTypeRepository.Update(phoneType, true);
            _phoneTypeRepository.Save();
        }
        public void DeletePhoneType(int id)
        {
            _phoneTypeRepository.Remove(id, true);
        }
        public IEnumerable<PhoneType> GetAllPhoneType()
        {
            return _phoneTypeRepository.GetAll();
        }

        #endregion

        #region Relationship

        public bool CreateRelationship(Relationship relationship)
        {
            return _relationshipRepository.Add(relationship, true) == null ? false : true;
        }
        public Relationship GetRelationshipById(int id)
        {
            return _relationshipRepository.GetById(id);
        }
        public void UpdateRelationship(Relationship relationship)
        {
            _relationshipRepository.Update(relationship, true);
            _relationshipRepository.Save();
        }
        public void DeleteRelationship(int id)
        {
            _relationshipRepository.Remove(id, true);
        }
        public IEnumerable<Relationship> GetAllRelationship()
        {
            return _relationshipRepository.GetAll();
        }

        #endregion

        #region EventType

        public bool CreateEventType(EventType eventType)
        {
            return _eventTypeRepository.Add(eventType, true) == null ? false : true;
        }
        public EventType GetEventTypeById(int id)
        {
            return _eventTypeRepository.GetById(id);
        }
        public void UpdateEventType(EventType eventType)
        {
            _eventTypeRepository.Update(eventType, true);
            _eventTypeRepository.Save();
        }
        public void DeleteEventType(int id)
        {
            _eventTypeRepository.Remove(id, true);
        }
        public IEnumerable<EventType> GetAllEventType()
        {
            return _eventTypeRepository.GetAll();
        }

        #endregion

        #region AlertType

        public bool CreateAlertType(AlertType alertType)
        {
            return _alertTypeRepository.Add(alertType, true) == null ? false : true;
        }
        public AlertType GetAlertTypeById(int id)
        {
            return _alertTypeRepository.GetById(id);
        }
        public void UpdateAlertType(AlertType alertType)
        {
            _alertTypeRepository.Update(alertType, true);
            _alertTypeRepository.Save();
        }
        public void DeleteAlertType(int id)
        {
            _alertTypeRepository.Remove(id, true);
        }
        public IEnumerable<AlertType> GetAllAlertType()
        {
            return _alertTypeRepository.GetAll();
        }

        #endregion

        #region ResourceType

        public bool CreateResourceType(ResourceType resourceType)
        {
            return _resourceTypeRepository.Add(resourceType, true) == null ? false : true;
        }
        public ResourceType GetResourceTypeById(int id)
        {
            return _resourceTypeRepository.GetById(id);
        }
        public void UpdateResourceType(ResourceType resourceType)
        {
            _resourceTypeRepository.Update(resourceType, true);
            _resourceTypeRepository.Save();
        }
        public void DeleteResourceType(int id)
        {
            _resourceTypeRepository.Remove(id, true);
        }
        public IEnumerable<ResourceType> GetAllResourceType()
        {
            return _resourceTypeRepository.GetAll();
        }

        #endregion
    }
}
