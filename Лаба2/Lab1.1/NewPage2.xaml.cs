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
using System.Windows.Markup;
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

        private bool AddFlag = false;
        private bool ChangeFlag = false;
        private bool UpdateFlag = false;

        AuthorsTableAdapter authors = new AuthorsTableAdapter();

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
            authors.DeleteQuery((int)(Lib.SelectedItem as DataRowView).Row[0]);
            
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
                    authors.InsertQuery(surname.Text, name.Text, lastname.Text, bitrh.Text, nationality.Text);
                    current = Lib.Items.Count;

                }
                else authors.UpdateQuery(surname.Text, name.Text, lastname.Text, bitrh.Text, nationality.Text, (int)(Lib.SelectedItem as DataRowView).Row[0]);

                Reload(current);
            }
            catch (Exception ex) when (ex is FormatException || ex is NullReferenceException)
            {
                MessageBox.Show("Неправильный ввод данных");

            }

        }

        private void Reload(int current = -1)
        {
            DataSetLib.AuthorsDataTable data = authors.GetData();

            Lib.ItemsSource = data;
            tb.Text = data.TableName;
            Lib.SelectedIndex = current;
        }

        private void SetMenuParams(bool clear = false)
        {
            DataRowView data = Lib.SelectedItem as DataRowView;

            surname.Text = (clear) ? "" : (string)data.Row[1];
            name.Text = (clear) ? "" : (string)data.Row[2];
            lastname.Text = (clear) ? "" : (data.Row[3] is DBNull) ? "" : (string)data.Row[3];
            bitrh.SelectedDate = (clear) ? DateTime.Now : (DateTime)data.Row[4];
            nationality.Text = (clear) ? "" : (string)data.Row[5];
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
