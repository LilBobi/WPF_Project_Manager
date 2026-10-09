using Project_Menedger.Models;
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
    /// Логика взаимодействия для RedactRoleAndDoljnost.xaml
    /// </summary>
    public partial class RedactRoleAndDoljnost : Window
    {
        Models.Userr useRR;
        public RedactRoleAndDoljnost(Userr userr)
        {
            InitializeComponent();
            ComboBox_Post.ItemsSource = App.db.Post.ToList();
            useRR = userr;
            TextBox_Surname.Text = userr.Surname;
            TextBox_Name.Text = userr.Namee;
            TextBox_Patronomyc.Text = userr.Patronymic;
            ComboBox_Post.Text = userr.Post.Namee;
            TextBlock_Role.Text = userr.Rolee.Namee;
        }

        int PickRole = 0;
        private void ComboBox_Post_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBox_Post.SelectedIndex >= 0 & ComboBox_Post.SelectedIndex <= 2)
            {
                PickRole = 1;
                TextBlock_Role.Text = "Сотрудник";
                return;
            }
            else if (ComboBox_Post.SelectedIndex == 3)
            {
                PickRole = 2;
                TextBlock_Role.Text = "Менеджер";
                return;
            }
            else if (ComboBox_Post.SelectedIndex == 4)
            {
                PickRole = 3;
                TextBlock_Role.Text = "Администратор";
                return;
            }
        }
        bool CheckMultipleAdmins()
        {
            if (PickRole == 3)
            {
                var adminCount = App.db.Userr.Count(x => x.id_Role == 3);
                if (adminCount >= 1)
                {
                    MessageBox.Show("Ошибка выдачи роли! В системе уже есть администратор.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        bool CheckMultipleManager()
        {
            if (PickRole == 2)
            {
                int managerCount = App.db.Userr.Count(x => x.id_Role == 2);
                if (managerCount >= 3)
                {
                    MessageBox.Show("Ошибка выдачи роли! В системе уже есть 3 менеджера.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }

        private void Button_RedactRoleAndDoljnost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Check())
                {
                    return;
                }
                if(!CheckMultipleAdmins())
                {
                    return;
                }
                if (!CheckMultipleManager())
                {
                    return;
                }
                Userr user = new Userr();
                user = useRR;
                user.Namee = user.Namee;
                user.Surname = user.Surname;
                user.Patronymic = user.Patronymic;
                user.id_Post = (int)ComboBox_Post.SelectedValue;
                user.Email = user.Email;
                user.Number_Phone = user.Number_Phone;
                user.Passwordd = user.Passwordd;
                user.id_Role = PickRole;
                PickRole = 0;
                App.db.SaveChanges();
                MessageBox.Show("Права были успешно изменены!", "Успешно", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        bool Check()
        {
            if (ComboBox_Post.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали должность!!", "Внимание", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
