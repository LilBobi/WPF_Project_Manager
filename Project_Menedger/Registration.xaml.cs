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
using System.Windows.Shapes;

namespace Project_Menedger
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        // Общий метод проверки всех данных
        private bool check()
        {
            if (!CheckEmpty())
            {
                return false;
            }
            if (!CheckName())
            {
                return false;
            }
            if (!CheckSurname())
            {
                return false;
            }
            if (!CheckPatronomyc())
            {
                return false;
            }
            if (!CheckNumberPhone())
            {
                return false;
            }
            if(!CheckEmail())
            {
                return false;
            }
            if (!CheckPassword())
            {
                return false;
            }
            if (!ProverkaPovtornogoEmail())
            {
                return false;
            }
            return true;
        }

        bool CheckEmpty()
        {
            if (TextBox_Name.Text == "" & TextBox_Surname.Text == "" & TextBox_Patronomyc.Text == "" &
                TextBox_NumberPhone.Text == "" & TextBox_Email.Text == "" & (TextBox_Password.Text == "" &
                PasswordBox_Password.Password == ""))
            {
                MessageBox.Show("Заполните все поля для дальнейшей регистрации!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            if (TextBox_Name.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле: Имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Surname.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле: Фамилия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Patronomyc.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле: Отчество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_NumberPhone.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле: Номер телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Email.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле: Email (электронная почта)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Password.Text == "" & PasswordBox_Password.Password == "")
            {
                MessageBox.Show("Вы не заполнили поле: Пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        bool CheckName()
        {
            string name = TextBox_Name.Text.Trim();
            string Check = @"^[a-zA-Zа-яА-ЯёЁ]{2,50}$";
            if (Regex.IsMatch(name, Check))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Имя введено некорректно! Введите имя от 2 до 50 букв", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        bool CheckSurname()
        {
            string surname = TextBox_Surname.Text.Trim();
            string Check = @"^[a-zA-Zа-яА-ЯёЁ]{2,50}$";
            if (Regex.IsMatch(surname, Check))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Фамилия введена некорректно! Введите фамилию от 2 до 50 букв", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        bool CheckPatronomyc()
        {
            string patronomyc = TextBox_Patronomyc.Text.Trim();
            string Check = @"^[a-zA-Zа-яА-ЯёЁ]{2,50}$";
            if (Regex.IsMatch(patronomyc, Check))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Отчество введено некорректно! Введите отчество от 2 до 50 букв", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        bool CheckNumberPhone()
        {
            string phoneNumber = TextBox_NumberPhone.Text.Trim();
            string Check = @"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$";
            if (Regex.IsMatch(phoneNumber, Check))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Номер введен некорректно! Формат: +7(555)353-24-24", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        bool CheckEmail()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            MatchCollection passN1 = regex.Matches(TextBox_Email.Text);
            if (passN1.Count <= 0)
            {
                MessageBox.Show("Введите корректный email! Например: Paha74@mail.ru", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        // Метод проверки правильности введенного пользователем пароля
        bool CheckPassword()
        {
            if(CheckSymbol8.Text == "Не выполнено")
            {
                MessageBox.Show("Пароль должен содержать не меньше 8 символов!", "Оишбка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(CheckSymbol16.Text == "Не выполнено")
            {
                MessageBox.Show("Пароль должен содержать не больше 16 символов!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }    
            if(CheckBukva1.Text == "Не выполнено")
            {
                MessageBox.Show("Пароль должен содержать минимум 1 заглавную букву!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(CheckCifra1.Text == "Не выполнено")
            {
                MessageBox.Show("Пароль содержать минимум 1 цифру!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        // Метод проверки на пользователей с одинаковой почтой
        bool ProverkaPovtornogoEmail()
        {
            if (App.db.Userr.Any(s => s.Email == TextBox_Email.Text))
            {
                MessageBox.Show("Пользователь с такой почтой уже есть! Введите другую почту.",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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
            // Заменяем изображение в CheckBox
            var imageElement = (Image)((CheckBox)CheckBox_Password).Template.FindName("CheckBoxImage", CheckBox_Password);
            if (imageElement != null)
            {
                // Указываем путь с помощью BitmapImage и Relative URI
                imageElement.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            }
        }


        // Метод регистрации
        private void Button_AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Userr user = new Models.Userr();
                if (!check())
                {
                    return;
                }
                if (CheckBox_Password.IsChecked == true)
                {
                    PasswordBox_Password.Password = TextBox_Password.Text;
                }
                else if (CheckBox_Password.IsChecked == false)
                {
                    TextBox_Password.Text = PasswordBox_Password.Password;
                }
                user.Namee = TextBox_Name.Text;
                user.Surname = TextBox_Surname.Text;
                user.Patronymic = TextBox_Patronomyc.Text;
                user.id_Post = 1;
                user.Email = TextBox_Email.Text;
                user.Number_Phone = TextBox_NumberPhone.Text;
                if (CheckBox_Password.IsChecked == true)
                {
                    user.Passwordd = TextBox_Password.Text;
                }
                else
                {
                    user.Passwordd = PasswordBox_Password.Password;
                }
                user.id_Role = 1;
                App.db.Userr.Add(user); // Добавление всех введенных данных пользователем
                App.db.SaveChanges(); // Сохранение данных в базе данных
                MessageBox.Show("Успешное добавление пользователя!", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
                this.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordPopup.IsOpen = true;
        }

        private void TextBox_Password_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordPopup.IsOpen = false;
        }

        private void PasswordBox_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordPopup.IsOpen = true;
        }

        private void PasswordBox_Password_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordPopup.IsOpen = false;
        }

        private void TextBox_NumberPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            numberPhonePoput.IsOpen = true;
        }

        private void TextBox_NumberPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            numberPhonePoput.IsOpen = false;
        }

        private void TextBox_Email_GotFocus(object sender, RoutedEventArgs e)
        {
            emailPoput.IsOpen = true;
        }

        private void TextBox_Email_LostFocus(object sender, RoutedEventArgs e)
        {
            emailPoput.IsOpen= false;
        }

        private void PasswordBox_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Regex MaskPassNumber = new Regex(@"(\w*\d+\w*)");
            MatchCollection passN = MaskPassNumber.Matches(PasswordBox_Password.Password);
            if (passN.Count > 0)
            {
                CheckCifra1.Foreground = Brushes.Green;
                CheckCifra1.Text = "Выполнено";
            }
            else
            {
                CheckCifra1.Foreground = Brushes.Red;
                CheckCifra1.Text = "Не выполнено";
            }
            Regex MaskPassUpLet = new Regex(@"(\w*[A-Z]+\w*)");
            MatchCollection passL = MaskPassUpLet.Matches(PasswordBox_Password.Password);
            if (passL.Count > 0)
            {
                CheckBukva1.Foreground = Brushes.Green;
                CheckBukva1.Text = "Выполнено";
            }
            else
            {
                CheckBukva1.Foreground = Brushes.Red;
                CheckBukva1.Text = "Не выполнено";
            }
            if (PasswordBox_Password.Password.Length > 7)
            {
                CheckSymbol8.Foreground = Brushes.Green;
                CheckSymbol8.Text = "Выполнено";
            }
            else
            {
                CheckSymbol8.Foreground = Brushes.Red;
                CheckSymbol8.Text = "Не выполнено";
            }
            if (PasswordBox_Password.Password.Length < 17)
            {
                CheckSymbol16.Foreground = Brushes.Green;
                CheckSymbol16.Text = "Выполнено";
            }
            else
            {
                CheckSymbol16.Foreground = Brushes.Red;
                CheckSymbol16.Text = "Не выполнено";
            }
        }

        private void TextBox_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex MaskPassNumber = new Regex(@"(\w*\d+\w*)");
            MatchCollection passN = MaskPassNumber.Matches(TextBox_Password.Text);
            if (passN.Count > 0)
            {
                CheckCifra1.Foreground = Brushes.Green;
                CheckCifra1.Text = "Выполнено";
            }
            else
            {
                CheckCifra1.Foreground = Brushes.Red;
                CheckCifra1.Text = "Не выполнено";
            }
            Regex MaskPassUpLet = new Regex(@"(\w*[A-Z]+\w*)");
            MatchCollection passL = MaskPassUpLet.Matches(TextBox_Password.Text);
            if (passL.Count > 0)
            {
                CheckBukva1.Foreground = Brushes.Green;
                CheckBukva1.Text = "Выполнено";
            }
            else
            {
                CheckBukva1.Foreground = Brushes.Red;
                CheckBukva1.Text = "Не выполнено";
            }
            if (TextBox_Password.Text.Length > 7)
            {
                CheckSymbol8.Foreground = Brushes.Green;
                CheckSymbol8.Text = "Выполнено";
            }
            else
            {
                CheckSymbol8.Foreground = Brushes.Red;
                CheckSymbol8.Text = "Не выполнено";
            }
            if (TextBox_Password.Text.Length < 17)
            {
                CheckSymbol16.Foreground = Brushes.Green;
                CheckSymbol16.Text = "Выполнено";
            }
            else
            {
                CheckSymbol16.Foreground = Brushes.Red;
                CheckSymbol16.Text = "Не выполнено";
            }
        }

        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            MatchCollection passN1 = regex.Matches(TextBox_Email.Text);
            if (passN1.Count <= 0)
            {
                checkEmail.Foreground = Brushes.Red;
                checkEmail.Text = "Не выполнено";
            }
            else
            {
                checkEmail.Foreground = Brushes.Green;
                checkEmail.Text = "Выполнено";
            }
        }

        private void TextBox_NumberPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phoneNumber = TextBox_NumberPhone.Text.Trim();
            string Check = @"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$";
            if (Regex.IsMatch(phoneNumber, Check))
            {
                checkNumberPhone.Foreground = Brushes.Green;
                checkNumberPhone.Text = "Выполнено";
            }
            else
            {
                checkNumberPhone.Foreground = Brushes.Red;
                checkNumberPhone.Text = "Не выполнено";
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
            if(this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
    }
}

