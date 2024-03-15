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
using WpfApp1.DataSetLibTableAdapters;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BooksTableAdapter books = new BooksTableAdapter();
        AuthorsTableAdapter authors = new AuthorsTableAdapter();

        public MainWindow()
        {

            InitializeComponent();
            Init();
            
        }

        private void Init()
        {
            NewPage newPage = new NewPage();
            newPage.Lib.ItemsSource = books.GetData();

            PageFrame.Content = newPage;

            back.IsEnabled = !(forward.IsEnabled = true);


        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            NewPage2 newPage2 = new NewPage2();
            newPage2.Lib.ItemsSource = authors.GetData();

            PageFrame.Content = newPage2;

            
            forward.IsEnabled = !(back.IsEnabled = true);
        }
    }
}
