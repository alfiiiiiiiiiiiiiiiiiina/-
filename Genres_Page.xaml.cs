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
    /// Логика взаимодействия для Genres_Page.xaml
    /// </summary>
    public partial class Genres_Page : Page
    {
        DataBase db = new DataBase();
        public ObservableCollection<genres> genress { get; set; }
        public Genres_Page()
        {
            InitializeComponent();
            genress = new ObservableCollection<genres>();
            LoadData();
        }

        private void LoadData()
        {
            var genreslist = db.Getgenres();

            foreach (var genree in genreslist)
            {
                genress.Add(genree);
            }

            genres_LB.ItemsSource = genress;
        }

        private void books_LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddGenre addGenre = new AddGenre();
            if(addGenre.ShowDialog() == true)
            {
                if (addGenre.NewGenres != null)
                {
                    genress.Add(addGenre.NewGenres);
                }
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            genres selectedGenre = genres_LB.SelectedItem as genres;

            if (selectedGenre == null) return;

            if (MessageBox.Show($"вы уверены, что хотите удалить жанр {selectedGenre.Name}?", "подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.DelGenre(selectedGenre.Id);
                genress.Remove(selectedGenre);
            }
        }
    }
}
