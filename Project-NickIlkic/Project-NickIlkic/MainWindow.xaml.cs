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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_NickIlkic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LibraryEntities5 libDB = new LibraryEntities5();
        Service.Service s = new Service.Service();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = String.Empty;
            txtAuthor.Text = String.Empty;
            txtISDN.Text = String.Empty;
            cbBooks.SelectedItem = cbBooks.Items[0];
            cbUsers.SelectedItem = cbBooks.Items[0];

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string author = txtAuthor.Text;
            string isdn =  txtISDN.Text;

            Book b = new Book() { Title = title, Author = author, ISDN = isdn };

            s.AddBook(b);


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from book in libDB.Books
                        select new { book.Id, book.Title, book.Author, book.ISDN };

            BooksDataGrid.ItemsSource = query.ToList();
            cbBooks.ItemsSource = query.ToList();

            var VWBorrowed = from user in libDB.VWBorroweds
                        select new { user.FirstName, user.LastName, user.Title };
 

            UsersDataGrid.ItemsSource = VWBorrowed.ToList();
            

            var comboBox = from user in libDB.Users
                         select new { user.FirstName, user.LastName };

            cbUsers.ItemsSource = comboBox.ToList();
        }

        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BooksDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object item = BooksDataGrid.SelectedItem;
            string ID = (BooksDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            txtTitle.Text = ID;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int bookId = cbBooks.SelectedIndex + 1;
            int userId = cbUsers.SelectedIndex + 1;
            Console.WriteLine(bookId + " " + userId);
            s.Lendbook(bookId, userId);
        }
    }
}
