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
    /// Логика взаимодействия для NewPage2.xaml
    /// </summary>
    public partial class NewPage2 : Page
    {
        LibraryDBEntities library = new LibraryDBEntities();
        public NewPage2()
        {
            InitializeComponent();
            Lib.ItemsSource = library.Authors.ToList();

            foreach (Author auth in Lib.ItemsSource)
            {
                if (!filter.Items.Contains(auth.Nationality))
                {
                    filter.Items.Add(auth.Nationality);
                }
            }
        }

        private void srch_btn_Click(object sender, RoutedEventArgs e)
        {
            Lib.ItemsSource = library.Authors.ToList().Where(item => item.SureName.Contains(search.Text));
        }

        private void clr_Click(object sender, RoutedEventArgs e)
        {
            search.Text = "";
            filter.SelectedItem = null;

            Lib.ItemsSource = library.Authors.ToList();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedItem != null)
            {
                Lib.ItemsSource = library.Authors.ToList().Where(item => (string)filter.SelectedItem == item.Nationality);
            }
        }
    }
}
