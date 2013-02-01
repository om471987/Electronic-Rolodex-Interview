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
        public User user { get; set; }
        public bool IsUserAdded { get; set; }

        public NewUser()
        {
            InitializeComponent();
        }

        private void AddNewAddressClick(object sender, RoutedEventArgs e)
        {
            if (First.Text == "" || Last.Text == "" || !Regex.Match(First.Text, @"^[a-zA-Z]+$").Success || !Regex.Match(Last.Text, @"^[a-zA-Z]+$").Success)
            {
                ErrorLabel.Content = "Please enter valid adddress.";
            }
            else
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = First.Text,
                    LastName = Last.Text
                };
                var db = new dbEntities();
                db.Users.Add(user);
                db.SaveChanges();
                IsUserAdded = true;
                MessageBox.Show("User " + First.Text + " " + Last.Text + " is added.");
                Close();
            }
        }

        private void BackToListClick(object sender, RoutedEventArgs e)
        {
            

            var userList = new UserList();
            userList.Show();
            Close();
        }
    }
}
