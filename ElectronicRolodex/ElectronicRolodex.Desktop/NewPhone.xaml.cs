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

namespace ElectronicRolodex.Desktop
{
    /// <summary>
    /// Interaction logic for NewPhone.xaml
    /// </summary>
    public partial class NewPhone
    {
        public NewPhone()
        {
            InitializeComponent();
        }

        private void AddNewPhoneClick(object sender, RoutedEventArgs e)
        {
            if (PhoneTypeComboBox.Text != "" && Number.Text != "")
            {
                //var user = new User
                //{
                //    Id = Guid.NewGuid(),
                //    FirstName = First.Text,
                //    LastName = Last.Text
                //};
                //var db = new dbEntities();
                //db.Users.Add(user);
                //db.SaveChanges();
                //Close();
                MessageBox.Show("User" + PhoneTypeComboBox.Text + " " + Number.Text + " is saved.");
                Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}
