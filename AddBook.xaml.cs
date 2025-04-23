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
    /// Логика взаимодействия для App_bookc.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        DataBase db = new DataBase();
        public books NewBooks { get; set; }
        public AddBook()
        {
            InitializeComponent();
            LoadAuthorComboBox();
            LoadGenresComboBox();

        }


        private void LoadGenresComboBox()
        {
            List<genres> genrelist = db.Getgenres();
            GenreCB.ItemsSource = genrelist;
            GenreCB.DisplayMemberPath = "Name";
            GenreCB.SelectedValuePath = "Id";
        }

        private void LoadAuthorComboBox()
        {
            List<Authors> authorlist = db.Getauthors();
            AuthorCB.ItemsSource = authorlist;
            AuthorCB.DisplayMemberPath = "FullName";
            AuthorCB.SelectedValuePath = "Id";
        }

        private void YearDP_GotFocus(object sender, RoutedEventArgs e)
        {
            if (YearDP.Text == "Введите год публикации")
            {
                YearDP.Text = "";
                YearDP.Foreground = Brushes.Black; 
            }
        }

        private void YearDP_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(YearDP.Text))
            {
                YearDP.Text = "Введите год публикации";
                YearDP.Foreground = Brushes.Gray; 
            }
        }

        private void addReaderBtn_Click(object sender, RoutedEventArgs e)
        {
            string bookTitle = titleTb.Text.Trim();
            string yearTxt = YearDP.Text.Trim();

            if (string.IsNullOrEmpty(bookTitle))
            {
                MessageBox.Show("Введите название книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(yearTxt, out int yearPub) || yearPub < 1000 || yearPub > DateTime.Now.Year)
            {
                MessageBox.Show($"Введите корректный год публикации (1000 - {DateTime.Now.Year}).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!(GenreCB.SelectedItem is genres selectedGenre))
            {
                MessageBox.Show("Выберите жанр книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!(AuthorCB.SelectedItem is Authors selectedAuthor))
            {
                MessageBox.Show("Выберите автора книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            db.AddBook(bookTitle, selectedGenre.Id, selectedAuthor.Id, yearPub);
           
            NewBooks = new books
            {
                Title = bookTitle,
                AuthorFirstName = selectedAuthor.FirstName,
                AuthorLastName = selectedAuthor.LastName, 
                Genre = selectedGenre.Name,
                Year_published = yearPub
            };

            DialogResult = true;
            Close();
        }



    }

}


