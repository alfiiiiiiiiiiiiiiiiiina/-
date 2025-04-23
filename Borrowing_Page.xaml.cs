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
    /// Логика взаимодействия для Borrowing_Page.xaml
    /// </summary>
    public partial class Borrowing_Page : Page
    {
        DataBase db = new DataBase();
        public ObservableCollection<borrowing> borrowingColl { get; set; }
        public Borrowing_Page()
        {
            InitializeComponent();
            borrowingColl = new ObservableCollection<borrowing>();
            LoadData();
        }

        private void LoadData()
        {
            var borrowinglist = db.Getborrowing();

            foreach (var borrowingi in borrowinglist)
            {
                borrowingColl.Add(borrowingi);
            }

            borrowings_LB.ItemsSource = borrowingColl;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBorrowing addBorrowing = new AddBorrowing();

            if(addBorrowing.ShowDialog() == true)
            {
                if(addBorrowing.newBorrowing != null)
                {
                    borrowingColl.Add(addBorrowing.newBorrowing);
                }
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            borrowing selectedBorrowing = borrowings_LB.SelectedItem as borrowing;

            if(selectedBorrowing == null)
            {
                MessageBox.Show("Пожалуйста, выберите книгу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Читатель {selectedBorrowing.FullReaderName} действительно вернул книгу {selectedBorrowing.Title}?", 
                "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.UPDBorrowing(selectedBorrowing.Id);

                selectedBorrowing.ReturnDate = DateTime.Today;
                borrowings_LB.Items.Refresh();

            }
        }
    }
}