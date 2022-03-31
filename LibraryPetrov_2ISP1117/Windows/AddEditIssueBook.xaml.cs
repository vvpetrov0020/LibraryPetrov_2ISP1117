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
    /// Логика взаимодействия для AddEditIssueBook.xaml
    /// </summary>
    public partial class AddEditIssueBook : Window
    {
        EF.Issue editBookIssue = new EF.Issue();

        public AddEditIssueBook()
        {
            InitializeComponent();

            cmbBook.ItemsSource = AppData.Context.Book.ToList();
            cmbBook.DisplayMemberPath = "Title";
            cmbBook.SelectedIndex = 0;

            cmbReader.ItemsSource = AppData.Context.Client.ToList();
            cmbReader.DisplayMemberPath = "LastName";
            cmbReader.SelectedIndex = 0;

            cmbEmployer.ItemsSource = AppData.Context.Worker.ToList();
            cmbEmployer.DisplayMemberPath = "LastName";
            cmbEmployer.SelectedIndex = 0;

        }

        private void btnAddIssueBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {
                    //Добавление нового читателя
                    EF.Issue bookIssue = new EF.Issue();
                    bookIssue.BookID = cmbBook.SelectedIndex + 1;
                    bookIssue.ClientID = cmbReader.SelectedIndex + 1;
                    bookIssue.WorkerID = cmbEmployer.SelectedIndex + 1;
                    bookIssue.DateIssue = dtDateStart.DisplayDate;
                    bookIssue.DateReturn = dtDateEnd.DisplayDate;

                    AppData.Context.Issue.Add(bookIssue);
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
