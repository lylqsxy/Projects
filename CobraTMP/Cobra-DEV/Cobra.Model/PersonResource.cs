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
    
    public partial class PersonResource
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ResourceId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Resource Resource { get; set; }
    }
}