using System.Linq;

namespace ElectronicRolodex.Data
{
    public interface IDataSource
    {
        IQueryable<Address> Addresses { get; set; }
        IQueryable<AddressType> AddressTypes { get; set; }
        IQueryable<ContactType> ContactTypes { get; set; }
        IQueryable<Country> Countries { get; set; }
        IQueryable<Phone> Phones { get; set; }
        IQueryable<PhoneType> PhoneTypes { get; set; }
        IQueryable<State> States { get; set; }
        IQueryable<User> Users { get; set; }
        IQueryable<UserContact> UserContacts { get; set; }
    }
}
