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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void books_btn_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new books_Page());
        }

        private void readers_btn_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new Readers_Page());
        }
        private void genres_btn_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new Genres_Page());

        }
        private void autours_btn_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new Authors_Page());

        }
        private void borrowing_btn_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new Borrowing_Page());

        }
    }
}
