using Project_Menedger.Models;
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

namespace Project_Menedger
{
    /// <summary>
    /// Логика взаимодействия для InformationProjectTeam.xaml
    /// </summary>
    public partial class InformationProjectTeam : Window
    {
        is1_25_kokorinds_Kursovoy_ProjectEntities1 db = new is1_25_kokorinds_Kursovoy_ProjectEntities1();
        public InformationProjectTeam()
        {
            InitializeComponent();
            if (Information.idRole == 1)
            {
                InformationEmployee.Visibility = Visibility.Visible;
                InformationAdministrator.Visibility = Visibility.Hidden;
                InformationManager.Visibility = Visibility.Hidden;
            }
            else if (Information.idRole == 2)
            {
                InformationManager.Visibility = Visibility.Visible;
                InformationEmployee.Visibility = Visibility.Hidden;
                InformationAdministrator.Visibility = Visibility.Hidden;
            }
            else if (Information.idRole == 3)
            {
                InformationAdministrator.Visibility = Visibility.Visible;
                InformationEmployee.Visibility = Visibility.Hidden;
                InformationManager.Visibility = Visibility.Hidden;
            }
            if (Information.idRole == 1)
            {
                UpdateEmployee();
            }
            if (Information.idRole >= 2)
            {
                UpdateManagerAndAdministrator();
            }
        }

        // Метод проверки команды пользователя
        List<Project_Team> curUser(List<Project_Team> projectTeams)
        {
            // Находим проект, к которому назначен текущий пользователь
            var currentUserProject = projectTeams
                .Where(pt => pt.id_User == Information.idUser)
                .Select(pt => pt.id_Project)
                .FirstOrDefault();

            // Проверяем, нашелся ли проект
            if (currentUserProject == default)
            {
                return new List<Project_Team>(); // Если проект не найден, возвращаем пустой список
            }

            // Возвращаем всех пользователей, назначенных к этому проекту
            return projectTeams
                .Where(pt => pt.id_Project == currentUserProject)
                .ToList();
        }

        void UpdateEmployee()
        {
            List<Project_Team> curProjectTeam = curUser(App.db.Project_Team.ToList());
            if (TextBox_Search.Text.Length > 0)
            {
                List<Project_Team> sortTextBoxSearch = new List<Project_Team>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                foreach (Project_Team projectTeam in curUser(curProjectTeam))
                {
                    if (projectTeam.id_Project == projectTeam.id_Project)
                    {
                        sortTextBoxSearch = curProjectTeam.Where(p => p.Production_Project.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Userr.Surname.ToString().ToLower().Contains(searchText) || p.Userr.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Userr.Patronymic.ToString().Contains(searchText) || p.Userr.Post.Namee.ToString().Contains(searchText)).ToList();
                    }
                }
                curProjectTeam = sortTextBoxSearch;
            }
            Table.ItemsSource = curProjectTeam;
        }

        void UpdateManagerAndAdministrator()
        {
            List<Project_Team> curProjectTeam = App.db.Project_Team.ToList();
            if (TextBox_Search.Text.Length > 0)
            {
                List<Project_Team> sortTextBoxSearch = new List<Project_Team>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                foreach (var projectTeam in db.Project_Team)
                {
                    if (projectTeam.id_Project == projectTeam.id_Project)
                    {
                        sortTextBoxSearch = db.Project_Team.Where(p => p.Production_Project.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Userr.Surname.ToString().ToLower().Contains(searchText) || p.Userr.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Userr.Patronymic.ToString().Contains(searchText) || p.Userr.Post.Namee.ToString().Contains(searchText)).ToList();
                    }
                }
                curProjectTeam = sortTextBoxSearch;
            }
            Table.ItemsSource = curProjectTeam;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InformationProject project = new InformationProject();
                project.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Information.idRole == 1)
            {
                UpdateEmployee();
            }
            if (Information.idRole >= 2)
            {
                UpdateManagerAndAdministrator();
            }
        }

        private void Button_AddTeamProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox_Search.Text = "";

                AddTeamProject addTeamProject = new AddTeamProject();
                addTeamProject.ShowDialog();
                UpdateManagerAndAdministrator();
                Table.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_RedactTeamProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    TextBox_Search.Text = "";

                    Project_Team project_Team = (Project_Team)Table.SelectedItem;
                    RedactTeamProject redact = new RedactTeamProject(project_Team);
                    redact.ShowDialog();
                    UpdateManagerAndAdministrator();
                    Table.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Вы не выбрали строчку для редактирования!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_DeleteTeamProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    if (MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        TextBox_Search.Text = "";

                        var select = Table.SelectedItem;
                        Project_Team project_Team = (Project_Team)select;
                        if (project_Team != null)
                        {
                            App.db.Project_Team.Remove(project_Team);
                        }
                        App.db.SaveChanges();
                        UpdateManagerAndAdministrator();
                        Table.SelectedIndex = -1;
                    }
                    else
                    {
                        Table.SelectedIndex = -1;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали строчку для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
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
