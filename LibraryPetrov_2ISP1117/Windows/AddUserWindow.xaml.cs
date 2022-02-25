using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LibraryPetrov_2ISP1117.ClassHelper;
using Microsoft.Win32;

namespace LibraryPetrov_2ISP1117.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        EF.Client editClient = new EF.Client();

        bool isEdit = true;

        string pathPhoto = null;


        public AddUserWindow()
        {
            InitializeComponent();
            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "Gender1";
            cmbGender.SelectedIndex = 0;

            isEdit = false;
        }

        public AddUserWindow(EF.Client client)
        {
            InitializeComponent();

            if (client.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(client.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    imgUser.Source = bitmapImage;

                }
            }

            editClient = client;

            //edit Combobox
            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "Gender1";

            //edit button
            tbTitle.Text = "Изменить данные читателя";
            btnAdd.Content = "Изменить";



            //edit values
            txtLastName.Text = editClient.LastName;
            txtFirstName.Text = editClient.FirstName;
            txtPhone.Text = editClient.Phone;
            txtEmail.Text = editClient.Email;
            txtAddress.Text = editClient.Address;
            cmbGender.SelectedIndex = editClient.GenderID - 1;

            isEdit = true;
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            #region
            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Поле Email не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле Адрес не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка на количество символов

            if (txtLastName.Text.Length > 100)
            {
                MessageBox.Show("В поле Фамилия недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstName.Text.Length > 100)
            {
                MessageBox.Show("В поле Имя недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Length > 20)
            {
                MessageBox.Show("В поле Телефон недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtEmail.Text.Length > 100)
            {
                MessageBox.Show("В поле Email недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtAddress.Text.Length > 100)
            {
                MessageBox.Show("В поле Адрес недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            //Проверка на ошибки в БД
            if (isEdit)
            {
                try
                {
                    //изменение читателя
                    editClient.LastName = txtLastName.Text;
                    editClient.FirstName = txtFirstName.Text;
                    editClient.Phone = txtPhone.Text;
                    editClient.Email = txtEmail.Text;
                    editClient.Address = txtAddress.Text;
                    editClient.GenderID = cmbGender.SelectedIndex + 1;

                    if (pathPhoto != null)
                    {
                        editClient.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppData.Context.SaveChanges();
                    MessageBox.Show("Данные пользователя успешно изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        EF.Client client = new EF.Client();
                        client.LastName = txtLastName.Text;
                        client.FirstName = txtFirstName.Text;
                        client.Email = txtEmail.Text;
                        client.Address = txtAddress.Text;
                        client.Phone = txtPhone.Text;
                        client.GenderID = cmbGender.SelectedIndex + 1;

                        if (pathPhoto != null)
                        {
                            client.Photo = File.ReadAllBytes(pathPhoto);
                        }

                        AppData.Context.Client.Add(client);
                        AppData.Context.SaveChanges();
                        MessageBox.Show("Успех", "Пользователь успешно добавлен", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox txtPhone)
            {
                txtPhone.Text = new string(txtPhone.Text.Where(ch => ch == '+' || ch == '-' || (ch >= '0' && ch <= '9') || ch == '(' || ch == ')').ToArray());
                txtPhone.SelectionStart = e.Changes.First().Offset + 1;
                txtPhone.SelectionLength = 0;
            }
        }

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathPhoto = openFileDialog.FileName;
            }
        }
    }
}
