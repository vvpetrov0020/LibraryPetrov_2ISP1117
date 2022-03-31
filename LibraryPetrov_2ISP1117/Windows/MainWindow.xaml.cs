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
using LibraryPetrov_2ISP1117.Windows;

namespace LibraryPetrov_2ISP1117
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnBookIssue_Click(object sender, RoutedEventArgs e)
        {
            BookIssue bookIssue = new BookIssue();
            bookIssue.ShowDialog();
            this.Close();
        }

        private void btnUserList_Click(object sender, RoutedEventArgs e)
        {
            UserListWindow userListWindow = new UserListWindow();
            userListWindow.Show();
            
        }

        private void btnBookList_Click(object sender, RoutedEventArgs e)
        {
            BookListWindow bookListWindow = new BookListWindow();
            bookListWindow.Show();
        }
    }
}
