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
    /// Логика взаимодействия для NewPage2.xaml
    /// </summary>
    public partial class NewPage2 : Page
    {
        AuthorsTableAdapter authors = new AuthorsTableAdapter();
        public NewPage2()
        {
            InitializeComponent();
            Lib.ItemsSource = authors.GetData();

            foreach (DataSetLib.AuthorsRow auth in Lib.ItemsSource)
            {
                if(!filter.Items.Contains(auth.Nationality))
                {
                    filter.Items.Add(auth.Nationality);
                }
            }
        }

        private void srch_btn_Click(object sender, RoutedEventArgs e)
        {
            Lib.ItemsSource = authors.SearchBySureName(search.Text);
        }

        private void clr_Click(object sender, RoutedEventArgs e)
        {
            search.Text = "";
            filter.SelectedItem = null;

            Lib.ItemsSource = authors.GetData();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedItem != null)
            {
                Lib.ItemsSource = authors.FilterByNationality((string)filter.SelectedItem);
            }
        }
    }
}
