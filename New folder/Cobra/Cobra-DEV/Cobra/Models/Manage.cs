using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Cobra.Model; //the database model

namespace Cobra.Models
{
    #region ViewModels

    public class ProfileViewModel
    {
        public ProfileModel ProfileModel { get; set; }
        public PersonModel PersonModel { get; set; }
        public List<PhoneModel> PhoneModel { get; set; }
        public List<EmailModel> EmailModel { get; set; }
        public List<AddressModel> AddressModel { get; set; }
        public List<SocialMediaViewModel> SocialMediaModel { get; set; }
    }

    #endregion

    #region Models for ViewModel

    public class ProfileModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        // [Display(Name = "First name", ResourceType = typeof(Resources.Resource))]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        // [Display(Name ="Middle name", ResourceType = typeof(Resources.Resource))]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter a dot (.), if you don't want to provide a last name.")]
        [Display(Name = "Last Name")]
        // [Display(Name = "Last name", ResourceType = typeof(Resources.Resource))]
        public string LastName { get; set; }
    }

    public class PersonModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Gender")]
        // [Display(Name = "Gender", ResourceType = typeof(Resources.Resource))]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        // [Display(Name = "Date of Birth", ResourceType = typeof(Resources.Resource))]
        public Nullable<DateTime> DoB { get; set; }
    }

    public class PhoneModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PhoneTypeId { get; set; }

        public int CountryId { get; set; }

        // \(?\d{3}\)?-? *\d{3}-? *-?\d{4} - Accept any 10 digit pattern; with or without (), -, and whitespace
        // Specific to the USA, eg. (XXX) XXX-XXXX
        //[RegularExpression(@"^(\(\d{3}\) |\d{3}-)\d{3}-\d{4}$")]
        [Phone]
        [Display(Name = "Number")]
       
        // [Display(Name = "Number", ResourceType = typeof(Resources.Resource))]
        public string Number { get; set; }

        [Required]
        public bool IsMobile { get; set; }

        [Required]
        public bool IsPrimary { get; set; }
    }

    public class EmailModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmailTypeId { get; set; }

        [Required]
        [EmailAddress]
     [Display(Name = "Email")]
       //[Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        [Required]
        public bool IsPrimary { get; set; }
    }

    public class AddressModel
    {
        [Key]
        public int Id { get; set; }

        public int AddressTypeId { get; set; }
        public int ProfileId { get; set; }

        [Display(Name = "Street Number")]
        // [Display(Name = "Street Number", ResourceType = typeof(Resources.Resource))]
        public string StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        // [Display(Name = "Street Name", ResourceType = typeof(Resources.Resource))]
        public string StreetName { get; set; }

        [Display(Name = "Suburb")]
        // [Display(Name = "Suburb", ResourceType = typeof(Resources.Resource))]
        public string Suburb { get; set; }

        [Display(Name = "City")]
        // [Display(Name = "City", ResourceType = typeof(Resources.Resource))]
        public string City { get; set; }

        [Display(Name = "State")]
        // [Display(Name = "State", ResourceType = typeof(Resources.Resource))]
        public string State { get; set; }

        [Display(Name = "Country")]
        // [Display(Name = "Country", ResourceType = typeof(Resources.Resource))]
        public string Country { get; set; }

        //[RegularExpression(@"^\d{4,12}$")]
        [Display(Name = "Zip Code")]
        // [Display(Name = "Zip Code", ResourceType = typeof(Resources.Resource))]
        public string ZipCode { get; set; }

        public bool IsPrimary { get; set; }
    }





    #endregion

    #region Emergency Contact
    public class EmergencyContactViewModel
    {
        public int Id { get; set; }
        [Required]
        // [Display(Name = "First name",ResourceType = typeof(Resources.Resource))]
        public string Firstname { get; set; }
      // [Display(Name = "Middle name",ResourceType = typeof(Resources.Resource))]
        public string Middlename { get; set; }
        [Required]
        //[Display(Name = "Last name",ResourceType = typeof(Resources.Resource))]
        public string Lastname { get; set; }
        [Required]
        public int RelationshipID { get; set; }
        [Required]
        public int Priority { get; set; }
        public string Reason { get; set; }
        [Required]
        public List<PhoneViewModel> PhoneList { get; set; }
    }

    public class PhoneViewModel {
        public int Id { get; set; }
        public string Number { get; set; }
        public int PhoneTypeID { get; set; }
        public bool IsMobile { get; set; }
        public bool IsPrimary { get; set; }
        public int? CountryID { get; set; }
    }

    /// <summary>
    /// Emergency contact attribute types view model such as 
    /// PhoneTypes, Relationship dropdown list
    /// </summary>
    public class ECDropdownViewModel
    {
        public List<PhoneTypeViewModel> PhoneTypes { get; set; }
        public List<RelationshipViewModel> Relationships { get; set; }
        public List<PhoneCountryViewModel> PhoneCountries { get; set; }
    }

    public class PhoneCountryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string PhoneCode { get; set; }
    }

    public class PhoneTypeViewModel
    {
        public int ID { get; set; }
        public string PhoneType { get; set; }
    }

    public class RelationshipViewModel
    {
        public int ID { get; set; }
        public string Relationship { get; set; }
    }
    #endregion

    #region Social Media
    public class SocialMediaViewModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string SocialMediaTypeId { get; set; }
        public string AuthenticationToken { get; set; }
    }
    #endregion

    //#region  Social Media type
    //public class SocialMediaTypeViewModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //}
    //#endregion
}