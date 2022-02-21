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

namespace LibraryPetrov_2ISP1117.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        EF.Book editBook = new EF.Book();
        bool isEdit = true;

        public AddBookWindow()
        {
            InitializeComponent();

            cmbSection.ItemsSource = AppData.Context.Section.ToList();
            cmbSection.DisplayMemberPath = "SectionName";
            cmbSection.SelectedIndex = 0;

            cmbPublishHouse.ItemsSource = AppData.Context.PublishHouse.ToList();
            cmbPublishHouse.DisplayMemberPath = "Name";
            cmbPublishHouse.SelectedIndex = 0;

            cmbLastName.ItemsSource = AppData.Context.Author.ToList();
            cmbLastName.DisplayMemberPath = "LastName";
            cmbLastName.SelectedIndex = 0;

            cmbFirstName.ItemsSource = AppData.Context.Author.ToList();
            cmbFirstName.DisplayMemberPath = "FirstName";
            cmbFirstName.SelectedIndex = 0;
            isEdit = false;
        }

        public AddBookWindow(EF.Book book)
        {
            InitializeComponent();

            editBook = book;
            //add combobox
            cmbSection.ItemsSource = AppData.Context.Section.ToList();
            cmbSection.DisplayMemberPath = "SectionName";

            cmbPublishHouse.ItemsSource = AppData.Context.PublishHouse.ToList();
            cmbPublishHouse.DisplayMemberPath = "Name";

            cmbLastName.ItemsSource = AppData.Context.Author.ToList();
            cmbLastName.DisplayMemberPath = "LastName";

            cmbFirstName.ItemsSource = AppData.Context.Author.ToList();
            cmbFirstName.DisplayMemberPath = "FirstName";

            //edit Title and content into button
            tbTitle.Text = "Изменения данных о книге";
            btnAdd.Content = "Изменить";

            //get value
            txtNameBook.Text = editBook.Name;
            cmbLastName.SelectedIndex = editBook.AuthorID - 1;
            cmbFirstName.SelectedIndex = editBook.AuthorID - 1;
            cmbSection.SelectedIndex = editBook.SectionID - 1;
            cmbPublishHouse.SelectedIndex = editBook.PublishHouseID;
            txtDateRelease.Text = editBook.DateRelease.ToString();
            txtNumberPages.Text = editBook.PageQty.ToString();
           
            isEdit = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
            #region
            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtNameBook.Text))
            {
                MessageBox.Show("Поле «Название книги» не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка на количество символов
            if (txtNameBook.Text.Length > 100)
            {
                MessageBox.Show("В поле «Название книги» недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion


            if (isEdit)
            {
                try
                {
                    editBook.Name = txtNameBook.Text;
                    editBook.AuthorID = cmbLastName.SelectedIndex + 1;
                    editBook.AuthorID = cmbFirstName.SelectedIndex + 1;
                    editBook.SectionID = cmbSection.SelectedIndex + 1;
                    editBook.PublishHouseID = cmbPublishHouse.SelectedIndex + 1;

                    AppData.Context.SaveChanges();
                    MessageBox.Show("Информация о книге успешно изменена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                //Проверка на ошибки в БД
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        //Добавление нового читателя
                        EF.Book book = new EF.Book();
                        book.Name = txtNameBook.Text;
                        book.AuthorID = cmbLastName.SelectedIndex + 1;
                        book.AuthorID = cmbFirstName.SelectedIndex + 1;
                        book.SectionID = cmbSection.SelectedIndex + 1;
                        book.PublishHouseID = cmbPublishHouse.SelectedIndex + 1;

                        AppData.Context.Book.Add(book);
                        AppData.Context.SaveChanges();
                        MessageBox.Show("Книга успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
