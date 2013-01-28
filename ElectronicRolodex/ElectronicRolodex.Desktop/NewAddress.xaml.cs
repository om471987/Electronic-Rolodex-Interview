using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            if (AddressType.Text != "" && HouseNumber.Text != "" && StreetName.Text != "" && AptOfficeRoom.Text != "" && City.Text != "" && ZipCode.Text != "")
            {
                var address = new Address
                {
                    Id = Guid.NewGuid(),
                    AddressType_Id = int.Parse(AddressType.Text),
                    HouseNumber = HouseNumber.Text,
                    Street = StreetName.Text,
                    ApartmentNumber = AptOfficeRoom.Text,
                    City = City.Text,
                    State_Id = 2,
                    Country_Id = 1,
                    Zipcode = ZipCode.Text

                };
                var db = new dbEntities();
                db.Addresses.Add(address);
                db.SaveChanges();
                MessageBox.Show("User" + AddressType.Text + " is saved.");
                Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void UpdateStates(object sender, RoutedEventArgs e)
        {
            var country = Country.SelectedItem as Country;
            var db = new dbEntities();
            var states = db.States.Where(t => t.Country_Id == country.Id).OrderBy(t => t.Name).ToList();
            State.ItemsSource = states;
            Country.SelectedIndex = 0;
            State.SelectedIndex = 0;
        }
    }
}
