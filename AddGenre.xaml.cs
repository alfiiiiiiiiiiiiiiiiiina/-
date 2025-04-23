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
    /// Логика взаимодействия для AddGenre.xaml
    /// </summary>
    public partial class AddGenre : Window
    {
        DataBase _db;
        public genres NewGenres {  get; set; }
        public AddGenre()
        {
            InitializeComponent();
            _db = new DataBase();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string gName = genreTB.Text;

            if (string.IsNullOrEmpty(gName))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _db.AddGenre(gName);

            NewGenres = new genres
            {
                Name = gName
            };

            DialogResult = true;
            Close();
            

        }
    }
}
