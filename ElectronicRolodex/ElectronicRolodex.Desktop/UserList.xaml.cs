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
using ElectronicRolodex.Data;

namespace ElectronicRolodex.Desktop
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        public UserList()
        {
            InitializeComponent();

            var currentRow = new TableRow();
            var db = new dbEntities();
            int i = 0;
            foreach (var t in db.Users)
            {
                i++;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(i.ToString()))) { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(t.FirstName))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(t.LastName))) { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black });

                var sad = new Button { Height = 23, Width = 60, Content = "View" };
                sad.Click += ViewDetails;
                var para=new Paragraph{ BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                para.Inlines.Add(sad);
                currentRow.Cells.Add(new TableCell(para));

                var sad1 = new Button { Height = 23, Width = 60, Content = "Add" };
                sad1.Click += EditUser;
                var para1 = new Paragraph { BorderThickness = new Thickness(0, 0, 1, 1), BorderBrush = Brushes.Black };
                para1.Inlines.Add(sad1);
                currentRow.Cells.Add(new TableCell(para1));

                UserTable.Rows.Add(currentRow);
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newUser = new NewUser();
            newUser.Show();
            Close();
        }

        private void ViewDetails(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }
    }
}
