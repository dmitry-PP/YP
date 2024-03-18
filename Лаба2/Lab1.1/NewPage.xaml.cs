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
using WpfApp1.DataSetLibTableAdapters;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для NewPage.xaml
    /// </summary>
    public partial class NewPage : Page
    {
        private bool AddFlag = false;
        private bool ChangeFlag = false;
        private bool UpdateFlag = false;

        BooksTableAdapter books = new BooksTableAdapter();
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
            books.DeleteQuery((int)(Lib.SelectedItem as DataRowView).Row[0]);
            
            Reload();
            SetFalse();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int current = Lib.SelectedIndex;

                if (!UpdateFlag)
                {
                    books.InsertQuery(name.Text, int.Parse(pages.Text), int.Parse(price.Text), release.Text, int.Parse(amount.Text), (int)author.SelectedValue);
                    current = Lib.Items.Count;
                }
                else books.UpdateQuery(name.Text, int.Parse(pages.Text), int.Parse(price.Text), release.Text, int.Parse(amount.Text), (int)author.SelectedValue, (int)(Lib.SelectedItem as DataRowView).Row[0]);
                
                Reload(current);

            }
            catch (Exception ex) when (ex is FormatException || ex is NullReferenceException)
            {
                MessageBox.Show("Неправильный ввод данных");

            }

        }

        private void Reload(int currentInd = -1)
        {
            DataSetLib.BooksDataTable data = books.GetData();

            Lib.ItemsSource = data;
            tb.Text = data.TableName;
            Lib.SelectedIndex = currentInd;
        }

        private void SetMenuParams(bool clear = false)
        {

            DataRowView data = Lib.SelectedItem as DataRowView;

            name.Text = (clear) ? "" : (string)data.Row[1];
            pages.Text = (clear) ? "" : Convert.ToString(data.Row[2]);
            price.Text = (clear) ? "" : Convert.ToString(data.Row[3]);
            release.SelectedDate = (clear) ? DateTime.Now : (DateTime)data.Row[4];
            amount.Text = (clear) ? "" : Convert.ToString(data.Row[5]);

            int auth_id = (clear) ? -1: (int)data.Row[6];

            author.SelectedIndex = GetRelated(auth_id);
  
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

        private int GetRelated(int id)
        {
            var list = new List<Item>();

            int ind = -1; int count = 0;
            foreach (DataSetLib.AuthorsRow auth in (new AuthorsTableAdapter()).GetData())
            {
                string name = "";

                name += auth.SureName + " " + auth.FirstName.Substring(0, 1) + ". " +
                    ((auth.IsLastNameNull()) ? "" : (auth.LastName + ".")) + "  - " +
                    auth.Birthday.ToString("yyyy") + $" ({auth.Nationality})";
                
                if(id == auth.ID) ind = count;

                list.Add(new Item(auth.ID,name));
                ++count;
            }

            author.ItemsSource = list;
            return ind;

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