using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ElectronicRolodex.Data;

namespace ElectronicRolodex.Desktop
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList
    {
        public UserList()
        {
            InitializeComponent();
            var db = new dbEntities();
            var i = 0;
            var users = db.Users.ToList();
            
            foreach (var t in users)
            {
                var homePhonePresent = false;
                var officePhone = false;
                var currentRow = new TableRow();
                i++;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(i.ToString(CultureInfo.InvariantCulture)))) { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(t.FirstName + " " + t.LastName))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });

                if (db.UserContacts.Any(v => v.User_Id == t.Id && v.ContactType.Id == (int) ContactCollection.Phone))
                {
                    var contacts = db.UserContacts.Where(u => u.User_Id == t.Id && u.ContactType.Id == (int) ContactCollection.Phone);
                    foreach (var u in contacts)
                    {
                        var phone = db.Phones.FirstOrDefault(v => v.Id == u.Contact_Id);
                        if (phone.PhoneType_Id == (int) PhoneCollection.Office)
                        {
                            officePhone = true;
                            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(phone.AreaCode + "-" + phone.Middle + "-" + phone.Last + " Ext. " + phone.Extension))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
                        }
                        else if (phone.PhoneType_Id == (int) PhoneCollection.Home)
                        {
                            homePhonePresent = true;
                            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(phone.AreaCode + "-" + phone.Middle + "-" + phone.Last))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
                        }
                    }
                }
                if (!officePhone)
                {
                    var PhoneButton = new Button { Content = "Add Office Phone", Uid = t.Id.ToString()};
                    PhoneButton.Click += NewOfficePhoneClick;
                    var detailsParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                    detailsParagraph.Inlines.Add(PhoneButton);
                    currentRow.Cells.Add(new TableCell(detailsParagraph));
                }
                if (!homePhonePresent)
                {
                    var PhoneButton = new Button { Content = "Add Home Phone", Uid = t.Id.ToString()};
                    PhoneButton.Click += NewHomePhoneClick;
                    var detailsParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                    detailsParagraph.Inlines.Add(PhoneButton);
                    currentRow.Cells.Add(new TableCell(detailsParagraph));
                }


                if (db.UserContacts.Any(v => v.User_Id == t.Id && v.ContactType.Id == (int) ContactCollection.Address))
                {
                    var contactId = db.UserContacts.FirstOrDefault(u => u.User_Id == t.Id && u.ContactType.Id == (int) ContactCollection.Address).Contact_Id;
                    var address = db.Addresses.FirstOrDefault(w => w.Id == contactId);
                    var state = db.States.FirstOrDefault(w => w.Id == address.State_Id);
                    var addressString = address.HouseNumber + " " + address.Street + " " + address.ApartmentNumber + " " + address.City;
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(addressString))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
                }
                else
                {
                    var addressButton = new Button { Content = "Add Address", Uid = t.Id.ToString() };
                    addressButton.Click += NewAddressClick;
                    var addressParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                    addressParagraph.Inlines.Add(addressButton);
                    currentRow.Cells.Add(new TableCell(addressParagraph));

                }
                UserTable.Rows.Add(currentRow);
            }
        }

        private void NewUserClick(object sender, RoutedEventArgs e)
        {
            var newUser = new NewUser();
            newUser.ShowDialog();
            var user = newUser.user;
            if (newUser.IsUserAdded)
            {
                var currentRow = new TableRow();
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(15.ToString(CultureInfo.InvariantCulture)))) { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(user.FirstName + " " + user.LastName))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });

                var PhoneButton = new Button { Content = "Add Office Phone", Uid = user.Id.ToString() };
                PhoneButton.Click += NewOfficePhoneClick;
                var detailsParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                detailsParagraph.Inlines.Add(PhoneButton);
                currentRow.Cells.Add(new TableCell(detailsParagraph));

                var homePhoneButton = new Button { Content = "Add Home Phone", Uid = user.Id.ToString() };
                homePhoneButton.Click += NewHomePhoneClick;
                var detailsHomeParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                detailsHomeParagraph.Inlines.Add(homePhoneButton);
                currentRow.Cells.Add(new TableCell(detailsHomeParagraph));


                var addressButton = new Button { Content = "Add Address", Uid = user.Id.ToString() };
                addressButton.Click += NewAddressClick;
                var addressParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                addressParagraph.Inlines.Add(addressButton);
                currentRow.Cells.Add(new TableCell(addressParagraph));

                UserTable.Rows.Add(currentRow);
            }
        }

        private void NewAddressClick(object sender, RoutedEventArgs e)
        {
            var a = sender as Button;

            var newAddress = new NewAddress(Guid.Parse(a.Uid));
            newAddress.ShowDialog();

            if (newAddress.IsAddressAdded)
            {
                var inline = a.Parent as Inline;
                var paragraph = inline.Parent as Paragraph;
                var tableCell = paragraph.Parent as TableCell;
                tableCell.Blocks.Clear();
                var addressString = newAddress.Address.HouseNumber + " " + newAddress.Address.Street + " " + newAddress.Address.ApartmentNumber + " " + newAddress.Address.City;
                tableCell.Blocks.Add(new Paragraph(new Run(addressString)) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
            }
        }

        private void NewHomePhoneClick(object sender, RoutedEventArgs e)
        {
            var a = sender as Button;
            var newPhone = new NewPhone(Guid.Parse(a.Uid), PhoneCollection.Home);
            newPhone.ShowDialog();
            if (newPhone.IsPhoneAdded)
            {
                var phone = newPhone.Phone.AreaCode + "-" + newPhone.Phone.Middle + "-" + newPhone.Phone.Last;
                var inline = a.Parent as Inline;
                var paragraph = inline.Parent as Paragraph;
                var tableCell = paragraph.Parent as TableCell;
                tableCell.Blocks.Clear();
                tableCell.Blocks.Add(new Paragraph(new Run(phone)) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
            }
        }

        private void NewOfficePhoneClick(object sender, RoutedEventArgs e)
        {
            var a = sender as Button;
            var newPhone = new NewPhone(Guid.Parse(a.Uid), PhoneCollection.Office);
            newPhone.ShowDialog();
            if (newPhone.IsPhoneAdded)
            {
                var phone = newPhone.Phone.AreaCode + "-" + newPhone.Phone.Middle + "-" + newPhone.Phone.Last;
                var inline = a.Parent as Inline;
                var paragraph = inline.Parent as Paragraph;
                var tableCell = paragraph.Parent as TableCell;
                tableCell.Blocks.Clear();
                tableCell.Blocks.Add(new Paragraph(new Run(phone)) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
            }
        }
    }
}
