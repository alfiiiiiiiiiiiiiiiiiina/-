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
    /// Логика взаимодействия для Readers_Page.xaml
    /// </summary>
    public partial class Readers_Page : Page
    {
        DataBase db = new DataBase();
        public ObservableCollection<readers> readers_ob { get; set; }
        public Readers_Page()
        {
            InitializeComponent();
            readers_ob = new ObservableCollection<readers>();
            LoadData();
        }

        private void LoadData()
        {
            var readerslist = db.Getreaders();

            foreach (var readers in readerslist)
            {
                readers_ob.Add(readers);
            }

            readers_LB.ItemsSource = readers_ob;
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            AddReaders readersPage = new AddReaders();
            if(readersPage.ShowDialog() == true)
            {
                if(readersPage.NewReader != null)
                {
                    readers_ob.Add(readersPage.NewReader);
                }
            }
        }

        private void remove_btn_Click(object sender, RoutedEventArgs e)
        {
            readers selectedreaders = readers_LB.SelectedItem as readers;

            if (selectedreaders == null)
            {
                MessageBox.Show("Пожалуйста, выберите читателя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(MessageBox.Show($"Вы действительно хотите удалить читателя {selectedreaders.FullName}?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.RemoveReader(selectedreaders.Id);
                readers_ob.Remove(selectedreaders);
            }
        }
    }
}
