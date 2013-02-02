using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using ElectronicRolodex.Data;

namespace ElectronicRolodex.Desktop
{
    /// <summary>
    /// Interaction logic for NewAddress.xaml
    /// </summary>
    public partial class NewAddress
    {
        private readonly Guid _user;
        private State _state;
        private Country _country;

        public Address Address { get; set; }
        public bool IsAddressAdded { get; set; }

        public NewAddress(Guid user)
        {
            _user = user;
            InitializeComponent();
            var db = new dbEntities();
            var countries = db.Countries.ToList();
            Country.ItemsSource = countries;
        }

        private void AddNewAddressClick(object sender, RoutedEventArgs e)
        {
            _state = State.SelectedItem as State;
            _country = Country.SelectedItem as Country;
            int intout;
            if (HouseNumber.Text == "" || StreetName.Text == "" || City.Text == "" || ZipCode.Text == "" ||
                !int.TryParse(HouseNumber.Text, out intout) || !int.TryParse(ZipCode.Text, out intout) ||
                !Regex.Match(StreetName.Text, @"^[a-zA-Z ]+$").Success || !Regex.Match(HouseNumber.Text, "^[a-zA-Z0-9]*$").Success ||
                !Regex.Match(City.Text, @"^[a-zA-Z ]+$").Success)
            {
                ErrorLabel.Content = Properties.Resources.EnterValidEmail;
            }
            else
            {
                IsAddressAdded = SaveAddress();
                MessageBox.Show(BuildSuccessMessage());
                Close();
            }
        }

        private bool SaveAddress()
        {
            Address = new Address
            {
                Id = Guid.NewGuid(),
                AddressType_Id = (int)AddressCollection.Home,
                HouseNumber = int.Parse(HouseNumber.Text),
                Street = StreetName.Text,
                ApartmentNumber = AptOfficeRoom.Text,
                City = City.Text,
                State_Id = _state.Id,
                Country_Id = _country.Id,
                Zipcode = int.Parse(ZipCode.Text)
            };
            var userContacts = new UserContact
            {
                Id = Guid.NewGuid(),
                User_Id = _user,
                contactType_Id = (int)ContactCollection.Address,
                Contact_Id = Address.Id
            };

            try
            {
                using (var db = new dbEntities())
                {
                    db.Addresses.Add(Address);
                    db.UserContacts.Add(userContacts);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        private void UpdateStates(object sender, RoutedEventArgs e)
        {
            var country = Country.SelectedItem as Country;
            var db = new dbEntities();
            var states = db.States.Where(t => t.Country_Id == country.Id).OrderBy(t => t.Name).ToList();
            State.ItemsSource = states;
            State.SelectedIndex = 0;
        }

        private string BuildSuccessMessage()
        {
            var message = new StringBuilder("Address, ");
            message.Append(HouseNumber.Text);
            message.Append(", ");
            message.Append(StreetName.Text);
            message.Append(", ");
            message.Append(AptOfficeRoom.Text);
            message.Append(", ");
            message.Append(City.Text);
            message.Append(", ");
            message.Append(AptOfficeRoom.Text);
            message.Append(", ");
            message.Append(_state.Name);
            message.Append(", ");
            message.Append(_state.Name);
            message.Append(", ");
            message.Append(ZipCode.Text);
            message.Append(" address is saved.");
            return message.ToString();
        }
    }
}
