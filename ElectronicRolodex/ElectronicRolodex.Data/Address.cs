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
    
    public partial class Address
    {
        public System.Guid Id { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public int State_Id { get; set; }
        public int Country_Id { get; set; }
        public int AddressType_Id { get; set; }
    
        public virtual AddressType AddressType { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
    }
}
