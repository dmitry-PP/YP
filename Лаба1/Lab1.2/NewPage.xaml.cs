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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для NewPage.xaml
    /// </summary>
    public partial class NewPage : Page
    {

        private LibraryDBEntities library = new LibraryDBEntities();
        public NewPage()
        {
            InitializeComponent();
            Reload();
        }

        private void Reload()
        {
            Lib.ItemsSource = library.Books.ToList();
            tb.Text = "Books";
        }
    }
}

