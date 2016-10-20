using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IAttributeTypeService
    {
        #region AddressType

        bool CreateAddressType(AddressType addressType);
        AddressType GetAddressTypeById(int id);
        void UpdateAddressType(AddressType addressType);
        void DeleteAddressType(int id);
        IEnumerable<AddressType> GetAllAddressType();

        #endregion

        #region EmailType

        bool CreateEmailType(EmailType emailType);
        EmailType GetEmailTypeById(int id);
        void UpdateEmailType(EmailType emailType);
        void DeleteEmailType(int id);
        IEnumerable<EmailType> GetAllEmailType();

        #endregion

        #region Country

        bool CreateCountry(Country country);
        Country GetCountryById(int id);
        void UpdateCountry(Country country);
        void DeleteCountry(int id);
        IEnumerable<Country> GetAllCountry();

        #endregion

        #region SocialMediaType

        bool CreateSocialMediaType(SocialMediaType socialMediaType);
        SocialMediaType GetSocialMediaTypeById(int id);
        void UpdateSocialMediaType(SocialMediaType socialMediaType);
        void DeleteSocialMediaType(int id);
        IEnumerable<SocialMediaType> GetAllSocialMediaType();
        int GetIdByProvider(string provider);

        #endregion

        #region PhoneType

        bool CreatePhoneType(PhoneType phoneType);
        PhoneType GetPhoneTypeById(int id);
        void UpdatePhoneType(PhoneType phoneType);
        void DeletePhoneType(int id);
        IEnumerable<PhoneType> GetAllPhoneType();

        #endregion

        #region Relationship

        bool CreateRelationship(Relationship relationship);
        Relationship GetRelationshipById(int id);
        void UpdateRelationship(Relationship relationship);
        void DeleteRelationship(int id);
        IEnumerable<Relationship> GetAllRelationship();

        #endregion

        #region EventType

        bool CreateEventType(EventType eventType);
        EventType GetEventTypeById(int id);
        void UpdateEventType(EventType eventType);
        void DeleteEventType(int id);
        IEnumerable<EventType> GetAllEventType();

        #endregion

        #region AlertType

        bool CreateAlertType(AlertType AlertType);
        AlertType GetAlertTypeById(int id);
        void UpdateAlertType(AlertType AlertType);
        void DeleteAlertType(int id);
        IEnumerable<AlertType> GetAllAlertType();

        #endregion

        #region ResourceType

        bool CreateResourceType(ResourceType resourceType);
        ResourceType GetResourceTypeById(int id);
        void UpdateResourceType(ResourceType resourceType);
        void DeleteResourceType(int id);
        IEnumerable<ResourceType> GetAllResourceType();

        #endregion
    }
}