using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ElectronicRolodex.Data;
using System.Text;

namespace ElectronicRolodex.Desktop
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList
    {
        private int _count = 1;

        public UserList()
        {
            InitializeComponent();
            var db = new dbEntities();
            var users = db.Users.ToList();
            
            foreach (var t in users)
            {
                var currentRow = new TableRow();

                currentRow.Cells.Add(GetTableCell((_count++).ToString(CultureInfo.InvariantCulture), new Thickness(1)));

                var displayName = t.FirstName + " " + t.LastName;
                currentRow.Cells.Add(GetTableCell(displayName, new Thickness(0, 0, 1, 1)));

                var phones = from p in db.Phones
                             join o in db.UserContacts on p.Id equals o.Contact_Id
                             where o.contactType_Id == (int)ContactCollection.Phone && o.User_Id == t.Id
                             select p;
                if (phones.Any(p => p.PhoneType_Id == (int)PhoneCollection.Office))
                {
                    var officePhone = phones.FirstOrDefault(p => p.PhoneType_Id == (int)PhoneCollection.Office);
                    var displayOfficePhone = officePhone.AreaCode + "-" + officePhone.Middle + "-" + officePhone.Last + " Ext. " + officePhone.Extension;
                    currentRow.Cells.Add(GetTableCell(displayOfficePhone, new Thickness(0, 0, 1, 1)));
                }
                else
                {
                    var button = GetButtonByType(ButtonType.OfficeButton, t.Id);
                    currentRow.Cells.Add(GetTableCell(button, new Thickness(0, 0, 1, 1)));
                }

                if (phones.Any(p => p.PhoneType_Id == (int)PhoneCollection.Home))
                {
                    var homePhone = phones.FirstOrDefault(p => p.PhoneType_Id == (int)PhoneCollection.Home);
                    var displayOfficePhone = homePhone.AreaCode + "-" + homePhone.Middle + "-" + homePhone.Last;
                    currentRow.Cells.Add(GetTableCell(displayOfficePhone, new Thickness(0, 0, 1, 1)));
                }
                else
                {
                    var button = GetButtonByType(ButtonType.HomeButton, t.Id);
                    currentRow.Cells.Add(GetTableCell(button, new Thickness(0, 0, 1, 1)));
                }

                if (db.UserContacts.Any(v => v.User_Id == t.Id && v.ContactType.Id == (int)ContactCollection.Address))
                {
                    var contactId = db.UserContacts.FirstOrDefault(u => u.User_Id == t.Id && u.ContactType.Id == (int)ContactCollection.Address).Contact_Id;
                    var address = db.Addresses.FirstOrDefault(w => w.Id == contactId);
                    var state = db.States.FirstOrDefault(w => w.Id == address.State_Id);
                    var country = db.Countries.FirstOrDefault(w => w.Id == address.Country_Id);

                    var addressString = GetAddressString(address);
                    currentRow.Cells.Add(GetTableCell(addressString, new Thickness(0, 0, 1, 1)));
                }
                else
                {
                    var button = GetButtonByType(ButtonType.AddressButton, t.Id);
                    currentRow.Cells.Add(GetTableCell(button, new Thickness(0, 0, 1, 1)));
                }
                UserTable.Rows.Add(currentRow);
            }
        }

        private string GetAddressString(Address address)
        {
            var output = new StringBuilder(address.HouseNumber);
            output.Append(", ");
            output.Append(address.Street);
            output.Append(" ");
            output.Append(address.ApartmentNumber);
            output.Append(", ");
            output.Append(address.City);
            output.Append(", ");
            output.Append(address.State.Name);
            output.Append(", ");
            output.Append(address.Country.Name);
            output.Append(", ");
            output.Append(address.Zipcode);
            return output.ToString();
        }

        private void NewUserClick(object sender, RoutedEventArgs e)
        {
            var newUser = new NewUser();
            newUser.ShowDialog();
            var user = newUser.user;
            if (newUser.IsUserAdded)
            {
                var currentRow = new TableRow();

                currentRow.Cells.Add(GetTableCell((_count++).ToString(CultureInfo.InvariantCulture), new Thickness(1)));
                currentRow.Cells.Add(GetTableCell(user.FirstName + " " + user.LastName, new Thickness(1)));
               
                var button = GetButtonByType(ButtonType.OfficeButton, user.Id);
                currentRow.Cells.Add(GetTableCell(button, new Thickness(0, 0, 1, 1)));

                button = GetButtonByType(ButtonType.HomeButton, user.Id);
                currentRow.Cells.Add(GetTableCell(button, new Thickness(0, 0, 1, 1)));

                button = GetButtonByType(ButtonType.AddressButton, user.Id);
                currentRow.Cells.Add(GetTableCell(button, new Thickness(0, 0, 1, 1)));

                UserTable.Rows.Add(currentRow);
            }
        }

        private void NewAddressClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var newAddress = new NewAddress(Guid.Parse(button.Uid));
            newAddress.ShowDialog();
            if (newAddress.IsAddressAdded)
            {
                var address = newAddress.Address.HouseNumber + " " + newAddress.Address.Street + " " + newAddress.Address.ApartmentNumber + " " + newAddress.Address.City;
                ReplaceButtonWithText(button, address);
            }
        }

        private void NewHomePhoneClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var newPhone = new NewPhone(Guid.Parse(button.Uid), PhoneCollection.Home);
            newPhone.ShowDialog();
            if (newPhone.IsPhoneAdded)
            {
                var phone = newPhone.Phone.AreaCode + "-" + newPhone.Phone.Middle + "-" + newPhone.Phone.Last;
                ReplaceButtonWithText(button, phone);
            }
        }

        private void NewOfficePhoneClick(object sender, RoutedEventArgs e)
        {
            var a = sender as Button;
            var newPhone = new NewPhone(Guid.Parse(a.Uid), PhoneCollection.Office);
            newPhone.ShowDialog();
            if (newPhone.IsPhoneAdded)
            {
                var inline = a.Parent as Inline;
                var paragraph = inline.Parent as Paragraph;
                var tableCell = paragraph.Parent as TableCell;
                tableCell.Blocks.Clear();
                var phone = newPhone.Phone.AreaCode + "-" + newPhone.Phone.Middle + "-" + newPhone.Phone.Last;
                tableCell.Blocks.Add(new Paragraph(new Run(phone)));
            }
        }

        private TableCell GetTableCell(string input, Thickness thickness)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(input));
            return new TableCell(paragraph)
            {
                BorderThickness = thickness,
                BorderBrush = Brushes.Black
            };
        }

        private TableCell GetTableCell(Button input, Thickness thickness)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(input);
            return new TableCell(paragraph)
            {
                BorderThickness = thickness,
                BorderBrush = Brushes.Black
            };
        }

        private Button GetButtonByType(ButtonType type, Guid userId)
        {
            var button = new Button
            {
                Uid = userId.ToString()
            };
            switch (type)
            {
                case ButtonType.OfficeButton:
                    button.Content = "Add Office Phone";
                    button.Click += NewOfficePhoneClick;
                    break;
                case ButtonType.HomeButton:
                    button.Content = "Add Home Phone";
                    button.Click += NewHomePhoneClick;
                    break;
                case ButtonType.AddressButton:
                    button.Content = "Add Address";
                    button.Click += NewAddressClick;
                    break;
            }
            return button;
        }

        private void ReplaceButtonWithText(Button button, string text)
        {
            var inline = button.Parent as Inline;
            var paragraph = inline.Parent as Paragraph;
            var tableCell = paragraph.Parent as TableCell;
            tableCell.Blocks.Clear();
            tableCell.Blocks.Add(new Paragraph(new Run(text)));
        }
    }
}
