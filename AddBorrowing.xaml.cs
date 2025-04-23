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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для AddBorrowing.xaml
    /// </summary>
    public partial class AddBorrowing : Window
    {
        DataBase db = new DataBase();
        public borrowing newBorrowing { get; set; }
        public AddBorrowing()
        {
            InitializeComponent();
            LoadBookCB();
            LoadReaderCb();
        }

        private void LoadBookCB()
        {
            List<books> booksList = db.GetBooksCB();
            BookCB.ItemsSource = booksList;
            BookCB.DisplayMemberPath = "Title";
            BookCB.SelectedValuePath = "Id";
        }

        private void LoadReaderCb()
        {
            List<readers> readersList = db.Getreaders();
            ReaderCB.ItemsSource = readersList;
            ReaderCB.DisplayMemberPath = "FullName";
            ReaderCB.SelectedValuePath = "Id";
        }

        private void borrowBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!(BookCB.SelectedItem is books selectedBook))
            {
                MessageBox.Show("Выберите книгу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!(ReaderCB.SelectedItem is readers selectedReader))
            {
                MessageBox.Show("Выберите читателя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if(MessageBox.Show($"Вы действительно хотите выдать книгу {selectedBook.Title} читателю {selectedReader.FullName}?", 
                "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.AddBorrowing(selectedBook.Id, selectedReader.Id);

                newBorrowing = new borrowing
                {
                    Title = selectedBook.Title,
                    ReaderFN = selectedReader.FirstName,
                    ReaderLN = selectedReader.LastName,
                    Expiration_date = DateTime.Today,
                    ReturnDate = DateTime.MinValue
                };

                DialogResult = true;
                Close();
            }

        }
    }
}
