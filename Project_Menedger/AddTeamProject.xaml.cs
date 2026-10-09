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
    /// Логика взаимодействия для AddTeamProject.xaml
    /// </summary>
    public partial class AddTeamProject : Window
    {
        public AddTeamProject()
        {
            InitializeComponent();
            ComboBox_Project.ItemsSource = App.db.Production_Project.ToList();
            List<Userr> userrs = App.db.Userr.ToList();
            var filerUser = userrs.Where(userr => userr.id_Role == 1).ToList();
            ComboBox_Employee.ItemsSource = filerUser;
        }

        private void Button_AddTeamProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Check())
                {
                    return;
                }
                if (App.db.Project_Team.Any(s => s.id_Project == (int)ComboBox_Project.SelectedValue && s.id_User == (int)ComboBox_Employee.SelectedValue))
                {
                    MessageBox.Show("Пользователь с таким проектом уже назначен!",
                        "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Project_Team project_Team = new Project_Team();
                project_Team.id_Project = (int)ComboBox_Project.SelectedValue;
                project_Team.id_User = (int)ComboBox_Employee.SelectedValue;
                App.db.Project_Team.Add(project_Team);
                App.db.SaveChanges();
                MessageBox.Show("Сотрудник был успешно добавлен к проекту!", "Успешно", MessageBoxButton.OK,
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
            if (ComboBox_Project.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали проект!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (ComboBox_Employee.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали сотрудника!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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
