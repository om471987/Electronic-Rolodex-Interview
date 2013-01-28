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
    public partial class NewAddress : Window
    {
        public NewAddress()
        {
            InitializeComponent();
            var db = new dbEntities();

            var countries = db.Countries.ToList();
            Country.ItemsSource = countries;

            var type = db.AddressTypes.ToList();
            AddressType.ItemsSource = type;
        }

        private void AddNewAddressClick(object sender, RoutedEventArgs e)
        {
            var addressType = AddressType.SelectedItem as AddressType;
            var state = State.SelectedItem as State;
            var country = Country.SelectedItem as Country;
            int intout;
            if (HouseNumber.Text == "" || StreetName.Text == "" || City.Text == "" || ZipCode.Text == "" ||
                !int.TryParse(HouseNumber.Text, out intout) || !int.TryParse(ZipCode.Text, out intout) ||
                !Regex.Match(StreetName.Text, @"^[a-zA-Z ]+$").Success || !Regex.Match(HouseNumber.Text, "^[a-zA-Z0-9]*$").Success ||
                !Regex.Match(City.Text, @"^[a-zA-Z ]+$").Success)
            {
                ErrorLabel.Content = "Please enter valid adddress.";
            }
            else
            {
                var address = new Address
                {
                    Id = Guid.NewGuid(),
                    AddressType_Id = addressType.Id,
                    HouseNumber =  int.Parse(HouseNumber.Text),
                    Street = StreetName.Text,
                    ApartmentNumber = AptOfficeRoom.Text,
                    City = City.Text,
                    State_Id = state.Id,
                    Country_Id = country.Id,
                    Zipcode = int.Parse(ZipCode.Text)
                };
                var db = new dbEntities();
                db.Addresses.Add(address);
                db.SaveChanges();

                var message = new StringBuilder(AddressType.Text);
                message.Append("  address, ");
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
                message.Append(state.Name);
                message.Append(", ");
                message.Append(country.Name);
                message.Append(", ");
                message.Append(ZipCode.Text);
                message.Append(" address is saved.");
                MessageBox.Show(message.ToString());
                Close();
            }
        }

        private void UpdateStates(object sender, RoutedEventArgs e)
        {
            var country = Country.SelectedItem as Country;
            var db = new dbEntities();
            var states = db.States.Where(t => t.Country_Id == country.Id).OrderBy(t => t.Name).ToList();
            State.ItemsSource = states;
            State.SelectedIndex = 0;
        }
    }
}
