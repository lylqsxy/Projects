//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cobra.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmergencyContact
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ProfileId { get; set; }
        public int RelationshipId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public string ReasonContact { get; set; }
        public int Priority { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Relationship Relationship { get; set; }
    }
}