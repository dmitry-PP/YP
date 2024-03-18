using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        private bool AddFlag = false;
        private bool ChangeFlag = false;
        private bool UpdateFlag = false;
        private LibraryDBEntities library = new LibraryDBEntities();
        
        public NewPage()
        {
            InitializeComponent();
            Reload();
        }


        private void cnl_Click(object sender, RoutedEventArgs e)
        {
            SetFalse();
            Menu.Visibility = Visibility.Collapsed;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (AddFlag) return;

            OpenMenu(true, false);
            SetMenuParams(true); //clear = true
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeFlag) return;

            OpenMenu(false, true);
            SetMenuParams();


        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            library.Books.Remove(Lib.SelectedItem as Book);

            Reload(true);
            SetFalse();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            

            Book book = (!UpdateFlag)? new Book(): Lib.SelectedItem as Book;

            try
            {
                book.Title = name.Text;
                book.Pages = int.Parse(pages.Text);
                book.Price = int.Parse(price.Text);
                book.DateRelease = (DateTime)release.SelectedDate;
                book.Amount = int.Parse(amount.Text);

                if (author.SelectedValue == null) throw new FormatException();
                book.Author_ID = (int)author.SelectedValue;

                int index = Lib.SelectedIndex;
                if (!UpdateFlag)
                {
                    library.Books.Add(book);
                    index = Lib.Items.Count;
                }

                Reload(true, index);                

            }
            catch (Exception ex) when (ex is FormatException || ex is NullReferenceException)
            {
                MessageBox.Show("Неправильный ввод данных");

            }

        }

        private void Reload(bool save = false, int current = -1)
        {
            if (save) library.SaveChanges();

            Lib.ItemsSource = library.Books.ToList();
            tb.Text = "Books";
            Lib.SelectedIndex = current;


        }

        private void SetMenuParams(bool clear = false)
        {

            Book book = Lib.SelectedItem as Book;

            name.Text = (clear) ? "" : book.Title;
            pages.Text = (clear) ? "" : Convert.ToString(book.Pages);
            price.Text = (clear) ? "" : Convert.ToString(book.Price);
            release.SelectedDate = (clear) ? DateTime.Now : book.DateRelease;
            amount.Text = (clear) ? "" : Convert.ToString(book.Amount);

            int auth_id = (clear) ? -1 : book.Author_ID;
            author.ItemsSource = GetRelated(auth_id);
            

        }

        private void Lib_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            change.IsEnabled = cancel.IsEnabled = del.IsEnabled = true;


            if ((AddFlag || ChangeFlag) && Lib.SelectedItem != null)
            {
                SetMenuParams();
                OpenMenu(false, true);
            }

        }

        private List<Item> GetRelated( int id )
        {
            var list = new List<Item>();

            foreach (Author auth in library.Authors)
            {
                string name = "";

                name += auth.SureName + " " + auth.FirstName.Substring(0, 1) + ". " +
                    auth.LastName + "  - " +auth.Birthday.ToString("yyyy") + $" ({auth.Nationality})";

                Item item = new Item(auth.ID, name);

                if (id == auth.ID) author.SelectedItem = item;

                list.Add(item);
            }

            return list;

        }

        private void SetFalse()
        {
            Lib.SelectedItem = null;
            ChangeFlag = UpdateFlag = AddFlag = false;
            change.IsEnabled = cancel.IsEnabled = del.IsEnabled = false;

            Menu.Visibility = Visibility.Collapsed;


        }

        private void OpenMenu(bool add, bool update, bool opened = true)
        {
            Menu.Visibility = Visibility.Visible;
            cancel.IsEnabled = true;

            UpdateFlag = ChangeFlag = update;
            AddFlag = add;
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
