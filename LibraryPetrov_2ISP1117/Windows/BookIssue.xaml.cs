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
    /// Логика взаимодействия для BookIssue.xaml
    /// </summary>
    public partial class BookIssue : Window
    {

        List<Issue> issueBookList = new List<Issue>();
        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии читателя", "По имени читателя", "По названию книги" };

        public BookIssue()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();

        }

        private void Filter()
        {
            issueBookList = AppData.Context.Issue.ToList();
            issueBookList = issueBookList.
                            Where(i => i.Client.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.Client.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.Book.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    issueBookList = issueBookList.OrderBy(i => i.ClientID).ToList();
                    break;
                case 1:
                    issueBookList = issueBookList.OrderBy(i => i.Client.LastName).ToList();
                    break;
                case 2:
                    issueBookList = issueBookList.OrderBy(i => i.Client.FirstName).ToList();
                    break;
                case 3:
                    issueBookList = issueBookList.OrderBy(i => i.Book.Name).ToList();
                    break;
                default:
                    issueBookList = issueBookList.OrderBy(i => i.ClientID).ToList();
                    break;
            }

            listRentBook.ItemsSource = issueBookList;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();

        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void listRentBook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listRentBook.SelectedItem is EF.Book)
                {
                    try
                    {
                        var item = listRentBook.SelectedItem as EF.Book;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppData.Context.Book.Remove(item);
                            AppData.Context.SaveChanges();
                            MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void btnAddRentBook_Click(object sender, RoutedEventArgs e)
        {
            AddEditIssueBook addIssueBookWindow = new AddEditIssueBook();
            this.Opacity = 0.2;
            addIssueBookWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

    }
}
