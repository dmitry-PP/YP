using System;
using System.Collections.Generic;
using System.Data;
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
        private bool AddFlag = false;
        private bool ChangeFlag = false;
        private bool UpdateFlag = false;
        private LibraryDBEntities library = new LibraryDBEntities();
        public NewPage2()
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
            library.Authors.Remove(Lib.SelectedItem as Author);

            Reload(true);
            SetFalse();

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            Author author = (!UpdateFlag) ? new Author() : Lib.SelectedItem as Author;

            try
            {
                if(surname.Text == ""  || name.Text == "" || nationality.Text == "")
                {
                    throw new FormatException();
                }

                author.SureName = surname.Text;
                author.FirstName = name.Text;
                author.LastName = lastname.Text;
                author.Birthday = (DateTime)bitrh.SelectedDate;
                author.Nationality = nationality.Text;

                
                int index = Lib.SelectedIndex;
                if (!UpdateFlag)
                {
                    library.Authors.Add(author);
                    index = Lib.Items.Count;
                }


                Reload(true, index);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильный ввод данных");

            }
        }

        private void Reload(bool save = false, int current = -1)
        {
            if (save) library.SaveChanges();

            Lib.ItemsSource = library.Authors.ToList();
            tb.Text = "Authors";
            Console.WriteLine(current);
            Lib.SelectedIndex = current;

        }

        private void SetMenuParams(bool clear = false)
        {
            Author author = Lib.SelectedItem as Author;

            surname.Text = (clear) ? "" : author.SureName;
            name.Text = (clear) ? "" : author.FirstName;
            lastname.Text = (clear) ? "" : author.LastName;
            bitrh.SelectedDate = (clear) ? DateTime.Now : (DateTime)author.Birthday;
            nationality.Text = (clear) ? "" : author.Nationality;
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
