using System.Windows;

namespace ElectronicRolodex.Desktop
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            //var userList = new UserList();
            //userList.Show();
            //Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (User.Text == "admin" && Password.Password == "dolphin")
            {
                var userList = new UserList();
                userList.Show();
                Close();
            }
            else
            {
                Error.Content = "Please check username/Password and try again.";
            }
        }
    }
}
