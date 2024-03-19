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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {

            PageFrame.Content = new NewPage();

            back.IsEnabled = !(forward.IsEnabled = true);


        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {

            PageFrame.Content = new NewPage2();

            forward.IsEnabled = !(back.IsEnabled = true);
        }
    }
}
