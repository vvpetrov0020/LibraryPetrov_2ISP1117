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
using LibraryPetrov_2ISP1117.ClassHelper;
using LibraryPetrov_2ISP1117.EF;

namespace LibraryPetrov_2ISP1117.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookListWindow.xaml
    /// </summary>
    public partial class BookListWindow : Window
    {

        List<Book> bookList = new List<Book>();
        List<string> listSort = new List<string>() { "По умолчанию", "По книге", "По количеству страниц" };
        public BookListWindow()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            BookList.ItemsSource = AppData.Context.Book.ToList();
            BookList.DisplayMemberPath = "LastName";
            AppData.Context.SaveChanges();

            Filter();
        }

        private void Filter()
        {
            bookList = AppData.Context.Book.ToList();
            bookList = bookList.Where(i => i.Name.ToLower().Contains(txtSearch.Text.ToLower()) || i.PageQty.ToString().Contains(txtSearch.Text)).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    bookList = bookList.OrderBy(i => i.Name).ToList();
                    break;

                case 2:
                    bookList = bookList.OrderBy(i => i.PageQty).ToList();
                    break;

                default:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
            }
            BookList.ItemsSource = bookList;
        }

        private void BookList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var item = BookList.SelectedItem as EF.Book;
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppData.Context.Book.Remove(item);
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

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            this.Opacity = 0.2;
            addBookWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

    }
}
