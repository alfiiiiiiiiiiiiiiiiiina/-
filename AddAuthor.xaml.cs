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
    /// Логика взаимодействия для AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : Window
    {
        DataBase _db;
        public Authors NewAuthors { get; set; }
        public AddAuthor()
        {
            InitializeComponent();
            _db = new DataBase();
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            string firstN = fnameTB.Text;
            string lastN = lnameTB.Text;

            if(string.IsNullOrEmpty(firstN) || string.IsNullOrEmpty(lastN))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _db.AddAuthor(firstN, lastN);

            NewAuthors = new Authors
            {
                FirstName = firstN,
                LastName = lastN
            };

            this.DialogResult = true;
            Close();
        }
    }
}
