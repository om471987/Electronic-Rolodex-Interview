//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectronicRolodex.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Phone
    {
        public System.Guid Id { get; set; }
        public string Number { get; set; }
        public int PhoneType_Id { get; set; }
    
        public virtual PhoneType PhoneType { get; set; }
    }
}