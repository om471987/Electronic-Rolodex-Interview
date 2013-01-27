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
                var currentRow = new TableRow();
                i++;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(i.ToString(CultureInfo.InvariantCulture)))) { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(t.FirstName))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(t.LastName))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });

                var viewButton = new Button { Content = "Add Address" };
                viewButton.Click += NewAddressClick;
                var viewParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                viewParagraph.Inlines.Add(viewButton);
                currentRow.Cells.Add(new TableCell(viewParagraph));

                var addButton = new Button { Content = "Add Phone" };
                addButton.Click += NewPhoneClick;
                var addParagraph = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                addParagraph.Inlines.Add(addButton);
                currentRow.Cells.Add(new TableCell(addParagraph));

                UserTable.Rows.Add(currentRow);
            }
        }

        private void NewUserClick(object sender, RoutedEventArgs e)
        {
            var newUser = new NewUser();
            newUser.ShowDialog();
        }

        private void NewAddressClick(object sender, RoutedEventArgs e)
        {
            var newAddress = new NewAddress();
            newAddress.ShowDialog();
        }

        private void NewPhoneClick(object sender, RoutedEventArgs e)
        {
            var newPhone = new NewPhone();
            newPhone.ShowDialog();
        }
    }
}
