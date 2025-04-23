using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для books_Page.xaml
    /// </summary>
    public partial class books_Page : Page
    {
        DataBase db = new DataBase();
        public ObservableCollection<books> bookss {get; set;}
        public books_Page()
        {
            InitializeComponent();
            bookss = new ObservableCollection<books>();
            LoadData();
        }

        private void LoadData()
        {
            var bookslist = db.GetBooks();

            foreach (var books in bookslist)
            {
                bookss.Add(books);
            }

            books_LB.ItemsSource = bookss;
        }

        private void books_LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook();
            
            if(addBook.ShowDialog() == true)
            {
                if (addBook.NewBooks != null)
                {
                    bookss.Add(addBook.NewBooks);
                }
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            books selectedBooks = books_LB.SelectedItem as books;

            if(selectedBooks == null)
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Вы действительно хотите удалить книгу {selectedBooks.Title}?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.DeleteBook(selectedBooks.Id);

                bookss.Remove(selectedBooks);

            }
        }
    }
}
