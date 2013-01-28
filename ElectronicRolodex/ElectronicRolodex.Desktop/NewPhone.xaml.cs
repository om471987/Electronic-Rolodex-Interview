using System;
using System.Linq;
using System.Text;
using System.Windows;
using ElectronicRolodex.Data;

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
            var db = new dbEntities();
            var type = db.PhoneTypes.ToList();
            PhoneType.ItemsSource = type;
        }

        private void AddNewPhoneClick(object sender, RoutedEventArgs e)
        {
            int intout;
            var phoneType = (PhoneType.SelectedItem as PhoneType).Id;
            if (AreaCode.Text.Length < 3 || MiddleNumber.Text.Length < 3 || LastNumber.Text.Length < 4 ||
                !int.TryParse(AreaCode.Text, out intout) || !int.TryParse(MiddleNumber.Text, out intout) || !int.TryParse(LastNumber.Text, out intout))
            {
                ErrorLabel.Content = "Please enter correct phone number.";
            }
            else if (phoneType == 1 && (AreaCode.Text.Length < 1 || !int.TryParse(ExtensionNumber.Text, out intout)))
            {
                ErrorLabel.Content = "Please enter correct extension.";
            }
            else
            {
                var phone = new Phone
                    {
                        Id = Guid.NewGuid(),
                        PhoneType_Id = phoneType,
                        AreaCode = int.Parse(AreaCode.Text),
                        Middle = int.Parse(MiddleNumber.Text),
                        Last = int.Parse(LastNumber.Text),
                        Extension = phoneType == 1 ? int.Parse(ExtensionNumber.Text) : 0
                    };
                var db = new dbEntities();
                db.Phones.Add(phone);
                db.SaveChanges();

                var message = new StringBuilder(PhoneType.Text);
                message.Append(" Phone ");
                message.Append(AreaCode.Text);
                message.Append("-");
                message.Append(MiddleNumber.Text);
                message.Append("-");
                message.Append(LastNumber.Text);
                message.Append((phoneType == 1 ? " Ex." + ExtensionNumber.Text : ""));
                message.Append(" is saved.");

                MessageBox.Show(message.ToString());
                Close();
            }
        }

        private void ToggleExtension(object sender, RoutedEventArgs e)
        {
            var phoneType = PhoneType.SelectedItem as PhoneType;
            if (phoneType.Id == 1) //office phone
            {
                ExtensionNumber.Visibility = Visibility.Visible;
                ExtensionLabel.Visibility = Visibility.Visible;
                ExtensionNumber.Text = "";
            }
            else
            {
                ExtensionNumber.Visibility = Visibility.Hidden;
                ExtensionLabel.Visibility = Visibility.Hidden;
                ExtensionNumber.Text = "";
            }
        }
    }
}
