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
        LibraryDBEntities library = new LibraryDBEntities();

        public NewPage()
        {
            InitializeComponent();
            Lib.ItemsSource = library.Books.ToList();


            filter.DisplayMemberPath = "Name";
            filter.SelectedValuePath = "ID";

            filter.ItemsSource = GetRelated();
        }

        private List<Item> GetRelated()
        {
            var list = new List<Item>();

            foreach (Author auth in library.Authors)
            {
                string name = "";

                name += auth.SureName + " " + auth.FirstName.Substring(0, 1) + ". " +
                    ((auth.LastName == "") ? "" : (auth.LastName + ".")) + "  - " +
                    auth.Birthday.ToString("yyyy") + $" ({auth.Nationality})";

                list.Add(new Item(auth.ID, name));

            }

            return list;


        }

        private void srch_btn_Click(object sender, RoutedEventArgs e)
        {
            Lib.ItemsSource = library.Books.ToList().Where(item => item.Title.Contains(search.Text));
        }

        private void clr_Click(object sender, RoutedEventArgs e)
        {
            search.Text = "";
            filter.SelectedItem = null;

            Lib.ItemsSource = Lib.ItemsSource = library.Books.ToList();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedItem != null)
            {
                Lib.ItemsSource = Lib.ItemsSource = library.Books.ToList().Where( item => (int)filter.SelectedValue == item.Author_ID);
            }
        }
    }
}

class Item
{
    public int ID { get; }
    public string Name { get; }

    public Item(int id, string name)
    {
        this.ID = id;
        this.Name = name;
    }
}