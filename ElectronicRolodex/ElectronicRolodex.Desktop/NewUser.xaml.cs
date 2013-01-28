using System;
using System.Text.RegularExpressions;
using System.Windows;
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

        private void NewUserClick(object sender, RoutedEventArgs e)
        {
            if (First.Text == "" || Last.Text == "" || !Regex.Match(First.Text, @"^[a-zA-Z]+$").Success || !Regex.Match(Last.Text, @"^[a-zA-Z]+$").Success)
            {
                ErrorLabel.Content = "Please enter valid adddress.";
            }
            else
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
                Close();
            }
        }
    }
}
