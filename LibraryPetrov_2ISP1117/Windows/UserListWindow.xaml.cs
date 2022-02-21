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
using System.Windows.Shapes;
using LibraryPetrov_2ISP1117.EF;
using LibraryPetrov_2ISP1117.ClassHelper;

namespace LibraryPetrov_2ISP1117.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window
    {
        List<Client> readerList = new List<Client>();
        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии", "По имени", "По адресу"};
        public UserListWindow()
        {
            InitializeComponent();

            ListReader.ItemsSource = AppData.Context.Client.ToList();
            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            readerList = AppData.Context.Client.ToList();
            readerList = readerList.Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) || i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0: readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    readerList = readerList.OrderBy(i => i.LastName).ToList();
                    break;

                case 2:
                    readerList = readerList.OrderBy(i => i.FirstName).ToList();
                    break;

                case 3:
                    readerList = readerList.OrderBy(i => i.Address).ToList();
                    break;
                default:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
            }
            ListReader.ItemsSource = readerList;
        }


        private void btnAddReader_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();

            addUserWindow.ShowDialog();
            Filter();


        }

        private void ListReader_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var item = ListReader.SelectedItem as EF.Client;
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppData.Context.Client.Remove(item);
                        AppData.Context.SaveChanges();
                        MessageBox.Show("Пользователь успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ListReader_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editClient = new EF.Client();

            if (ListReader.SelectedItem is EF.Client)
            {
                editClient = ListReader.SelectedItem as EF.Client;
            }
            AddUserWindow editReaderWindow = new AddUserWindow(editClient);
            this.Opacity = 0.2;
            editReaderWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }
    }
}
