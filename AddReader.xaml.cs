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
    /// Логика взаимодействия для readers.xaml
    /// </summary>
    public partial class AddReaders : Window
    {
        DataBase db = new DataBase();
        public readers NewReader { get; set; }
        public AddReaders()
        {
            InitializeComponent();
        }

        private void add_btn_Click()
        {
            string fname = fnameTb.Text;
            string lname = lnameTb.Text;
            string phone = phoneTb.Text;
            string address = addressTb.Text;

            if (string.IsNullOrWhiteSpace(fname) ||
                string.IsNullOrWhiteSpace(lname) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address) )
            {
                MessageBox.Show("Заполните все поля.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            db.AddReader(fname, lname, phone, address);

            NewReader = new readers
            {
                FirstName = fname,
                LastName = lname,
                Phone = phone,
                Address = address
            };

            MessageBox.Show("Читатель добавлен успешно!");
            ClearFields();
            this.DialogResult = true;
            Close();
        }

        private void ClearFields()
        {
            fnameTb.Text = string.Empty;
            lnameTb.Text = string.Empty;
            phoneTb.Text = string.Empty;
            addressTb.Text = string.Empty;
        }

        private void addReaderBtn_Click(object sender, RoutedEventArgs e)
        {
            add_btn_Click();
        }
    }
}
