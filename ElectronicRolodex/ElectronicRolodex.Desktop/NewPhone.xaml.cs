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
        private readonly Guid _user;
        private readonly int _phoneType;

        public Phone Phone { get; set; }
        public bool IsPhoneAdded { get; set; }

        public NewPhone(Guid user, PhoneCollection phoneType)
        {
            _user = user;
            _phoneType = (int) phoneType;
            InitializeComponent();
            if (_phoneType == 1)
            {
                ExtensionLabel.Visibility = Visibility.Visible;
                ExtensionNumber.Visibility = Visibility.Visible;
            }
        }

        private void AddNewPhoneClick(object sender, RoutedEventArgs e)
        {
            int intout;
            if (AreaCode.Text.Length < 3 || MiddleNumber.Text.Length < 3 || LastNumber.Text.Length < 4 ||
                !int.TryParse(AreaCode.Text, out intout) || !int.TryParse(MiddleNumber.Text, out intout) || !int.TryParse(LastNumber.Text, out intout))
            {
                ErrorLabel.Content = Properties.Resources.EnterValidPhone;
            }
            else if (_phoneType == 1 && (AreaCode.Text.Length > 0 && !int.TryParse(ExtensionNumber.Text, out intout)))
            {
                ErrorLabel.Content = Properties.Resources.EnterValidExtension;
            }
            else
            {
                IsPhoneAdded = SavePhone();
                MessageBox.Show(BuildSuccessMessage());
                Close();
            }
        }

        private bool SavePhone()
        {
            Phone = new Phone
            {
                Id = Guid.NewGuid(),
                PhoneType_Id = _phoneType,
                AreaCode = int.Parse(AreaCode.Text),
                Middle = int.Parse(MiddleNumber.Text),
                Last = int.Parse(LastNumber.Text),
                Extension = _phoneType == 1 ? int.Parse(ExtensionNumber.Text) : 0
            };

            var userContacts = new UserContact
            {
                Id = Guid.NewGuid(),
                User_Id = _user,
                contactType_Id = (int)ContactCollection.Phone,
                Contact_Id = Phone.Id
            };

            try
            {
                using (var db = new dbEntities())
                {
                    db.Phones.Add(Phone);
                    db.UserContacts.Add(userContacts);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private string BuildSuccessMessage()
        {
            var message = new StringBuilder("Phone ");
            message.Append(AreaCode.Text);
            message.Append("-");
            message.Append(MiddleNumber.Text);
            message.Append("-");
            message.Append(LastNumber.Text);
            message.Append((_phoneType == 1 ? " Ex." + ExtensionNumber.Text : ""));
            message.Append(" is saved.");
            return message.ToString();
        }
    }
}
