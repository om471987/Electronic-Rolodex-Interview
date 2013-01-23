using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (First.Text != "" && Last.Text != "")
            {
                var user = new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = First.Text,
                        LastName = Last.Text
                    };
                var db = new dbEntities();
                db.Users.Add(user);
                db.SaveChanges();
                MessageBox.Show("User" + First.Text + " " + First.Text + " is saved.");

                var userList = new UserList();
                userList.Show();
                Close();
            }
        }
    }
}
