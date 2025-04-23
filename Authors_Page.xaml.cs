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
    /// Логика взаимодействия для Authors_Page.xaml
    /// </summary>
    public partial class Authors_Page : Page
    {
        DataBase db = new DataBase();
        public ObservableCollection<Authors> authors_ob { get; set; }
        public Authors_Page()
        {
            InitializeComponent();
            authors_ob = new ObservableCollection<Authors>();
            LoadData();
        }

        private void LoadData()
        {
            var authorslist = db.Getauthors();

            foreach (var authors in authorslist)
            {
                authors_ob.Add(authors);
            }

            authors_LB.ItemsSource = authors_ob;
        }

        private void books_LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAuthor addAuthor = new AddAuthor();
            if (addAuthor.ShowDialog() == true)
            {
                if (addAuthor.NewAuthors != null)
                {
                    authors_ob.Add(addAuthor.NewAuthors);
                }
            }
        }

        private void delBTN_Click(object sender, RoutedEventArgs e)
        {
            Authors selectedAuthor = authors_LB.SelectedItem as Authors;
            if (selectedAuthor == null) return;

            if (MessageBox.Show($"Вы действительно хотите удалить автора {selectedAuthor.FullName}?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.DelAuthor(selectedAuthor.Id);
                authors_ob.Remove(selectedAuthor);
            }
        }
    }
}
