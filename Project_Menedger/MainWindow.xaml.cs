using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Project_Menedger
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

        private bool Check()
        {
            if(!CheckEmail())
            {
                return false;
            }
            return true;
        }
        bool CheckEmail()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            MatchCollection passN1 = regex.Matches(TextBox_Email.Text);
            if (passN1.Count <= 0)
            {
                MessageBox.Show("Введите корректный email! Например: Paha74@mail.ru", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        private void CheckBox_Password_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                SetCheckBoxImage("/Image/OpenEye.png");
                TextBox_Password.Text = PasswordBox_Password.Password;
                TextBox_Password.Visibility = Visibility.Visible;
                PasswordBox_Password.Visibility = Visibility.Hidden;
            }
            else
            {
                SetCheckBoxImage("/Image/CloseEye.png");
                PasswordBox_Password.Password = TextBox_Password.Text;
                TextBox_Password.Visibility = Visibility.Hidden;
                PasswordBox_Password.Visibility = Visibility.Visible;
            }
        }
        private void SetCheckBoxImage(string imagePath)
        {
            // замена изображения в CheckBox
            var imageElement = (Image)((CheckBox)CheckBox_Password).Template.FindName("CheckBoxImage", CheckBox_Password);
            if (imageElement != null)
            {
                // путь с помощью BitmapImage и Relative URI
                imageElement.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            }
        }

        // Метод авторизации
        private void Button_Input_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = TextBox_Email.Text;
                var password = TextBox_Password.Text;
                var passwordHide = PasswordBox_Password.Password;
                if (email == "")
                {
                    if (password == "" & passwordHide == "")
                    {
                        MessageBox.Show("Вы не заполнили поля: Email и Пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show("Вы не заполнили поле: Email", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (password == "" & passwordHide == "")
                {
                    MessageBox.Show("Вы не заполнили поле: Пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (!Check())
                {
                    return;
                }
                foreach (var user in App.db.Userr)
                {
                    if (user.id_Role == 1)
                    {
                        if (user.Email == email)
                        {
                            if (user.Passwordd == password | user.Passwordd == passwordHide)
                            {
                                Information.idUser = user.id_User;
                                Information.Name = user.Namee;
                                Information.Surname = user.Surname;
                                Information.Patronymic = user.Patronymic;
                                Information.idRole = user.id_Role;
                                MessageBox.Show("Вы успешно авторизированы!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                InformationProject informationProject = new InformationProject();
                                informationProject.Show();
                                this.Close();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Вы ввели неверный пароль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }
                    if (user.id_Role == 2)
                    {
                        if (user.Email == email)
                        {
                            if (user.Passwordd == password | user.Passwordd == passwordHide)
                            {
                                Information.idUser = user.id_User;
                                Information.Name = user.Namee;
                                Information.Surname = user.Surname;
                                Information.Patronymic = user.Patronymic;
                                Information.idRole = user.id_Role;
                                MessageBox.Show("Вы успешно авторизированы!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                MainAdministrator mainAdministrator = new MainAdministrator();
                                mainAdministrator.Show();
                                this.Close();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Вы ввели неверный пароль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }
                    if (user.id_Role == 3)
                    {
                        if (user.Email == email)
                        {
                            if (user.Passwordd == password | user.Passwordd == passwordHide)
                            {
                                Information.idUser = user.id_User;
                                Information.Name = user.Namee;
                                Information.Surname = user.Surname;
                                Information.Patronymic = user.Patronymic;
                                Information.idRole = user.id_Role;
                                MessageBox.Show("Вы успешно авторизированы!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                MainAdministrator mainAdministrator = new MainAdministrator();
                                mainAdministrator.Show();
                                this.Close();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Вы ввели неверный пароль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }
                }
                MessageBox.Show("Пользователя с таким email нет!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы хотите выйти из программы?", "Вопрос", MessageBoxButton.YesNo,
                MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_RollUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Unwrap_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
    }
}
