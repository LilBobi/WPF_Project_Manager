using Project_Menedger.Models;
using System;
using System.Collections;
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
    /// Логика взаимодействия для InformationProject.xaml
    /// </summary>
    public partial class InformationProject : Window
    {
        is1_25_kokorinds_Kursovoy_ProjectEntities1 db = new is1_25_kokorinds_Kursovoy_ProjectEntities1();
        public InformationProject()
        {
            InitializeComponent();
            if(Information.idRole == 1)
            {
                Button_Back.Content = "Выход";
                Table.Visibility = Visibility.Hidden;
                TableUsers.Visibility = Visibility.Visible;
                InformationEmployee.Visibility = Visibility.Visible;
                InformationAdministrator.Visibility = Visibility.Hidden;
                InformationManager.Visibility = Visibility.Hidden;
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                UpdateDGUsers();
                ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
            }
            else if(Information.idRole == 2)
            {
                TableUsers.Visibility = Visibility.Hidden;
                Table.Visibility = Visibility.Visible;
                InformationManager.Visibility= Visibility.Visible;
                InformationEmployee.Visibility= Visibility.Hidden;
                InformationAdministrator.Visibility= Visibility.Hidden;
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                UpdateDG();
                ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
            }
            else if(Information.idRole == 3)
            {
                TableUsers.Visibility = Visibility.Hidden;
                Table.Visibility = Visibility.Visible;
                InformationAdministrator.Visibility= Visibility.Visible;
                InformationEmployee.Visibility= Visibility.Hidden;
                InformationManager.Visibility= Visibility.Hidden;
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                UpdateDG();
                ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
            }
        }

        List<Production_Project> curProject(List<Production_Project> project)
        {
            return project.Where(x => x.id_Project == x.id_Project).ToList();
        }

        // Метод сортировки и поиска
        void UpdateDG()
        {
            List<Production_Project> curProjectt = curProject(App.db.Production_Project.ToList());
            if (datePicker1.SelectedDate > datePicker2.SelectedDate)
            {
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
            }
            else if (datePicker1.SelectedDate <= datePicker2.SelectedDate)
            {
                DateTime dateTimeStart = datePicker1.SelectedDate.Value;
                string formatedDate = dateTimeStart.ToString("yyyy.MM.dd");
                DateTime dateTimeEnd = datePicker2.SelectedDate.Value;
                string formatedDate1 = dateTimeEnd.ToString("yyyy.MM.dd");
                List<Production_Project> sortDate = new List<Production_Project>();
                sortDate.Clear();
                foreach (Production_Project prodProject in curProject(curProjectt))
                {
                    if (DateTime.Parse(formatedDate) <= prodProject.Start_Datee && DateTime.Parse(formatedDate1) >= prodProject.Start_Datee)
                        sortDate.Add(prodProject);
                }
                curProjectt = sortDate;
            }
            if (ComboBox_Category.SelectedIndex != -1)
            {
                string category = ComboBox_Category.SelectedValue.ToString();
                List<Production_Project> sortComboBox = new List<Production_Project>();
                sortComboBox.Clear();
                foreach(Production_Project prodProject in curProject(curProjectt))
                {
                    if (category.Equals(prodProject.Statuss.Namee))
                        sortComboBox.Add(prodProject);
                }
                curProjectt = sortComboBox;
            }
            if (TextBox_Search.Text.Length > 0)
            {
                List<Production_Project> sortTextBoxSearch = new List<Production_Project>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                var sortTextBox = db.Production_Project.Where(p => p.id_Project == p.id_Project && 
                (p.Namee.ToString().ToLower().Contains(searchText) || p.Descriptionn.ToString().ToLower().Contains(searchText) 
                || p.Budget.ToString().ToLower().Contains(searchText) || p.Resourcee.Namee.ToLower().Contains(searchText))).ToList();
                foreach (Production_Project prodProject in curProject(curProjectt))
                {
                    if (prodProject.id_Project == prodProject.id_Project)
                    {
                        sortTextBoxSearch = curProjectt.Where(p => p.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Descriptionn.ToString().ToLower().Contains(searchText) || p.Budget.ToString().ToLower().Contains(searchText) ||
                        p.Resourcee.Namee.ToLower().Contains(searchText)).ToList();
                    }
                }
                curProjectt = sortTextBoxSearch;
            }
            if(Information.idRole == 1)
            {
                TableUsers.ItemsSource = curProjectt;
            }
            if (Information.idUser >= 2)
            {
                Table.ItemsSource = curProjectt;
            }
        }

        List<Production_Project> FilterUserProjects(List<Production_Project> projects)
        {
            // Получаем ID проектов, связанных с пользователем
            var userProjectIds = App.db.Project_Team
                .Where(pt => pt.id_User == Information.idUser)
                .Select(pt => (int)pt.id_Project)
                .Union(
                    App.db.Tasks
                        .Where(t => t.id_User == Information.idUser)
                        .Select(t => (int)t.id_Project)
                )
                .Distinct()
                .ToList();

            // Фильтруем переданный список
            return projects.Where(p => userProjectIds.Contains(p.id_Project)).ToList();
        }

        // Метод сортировки и поиска
        void UpdateDGUsers()
        {
            List<Production_Project> curProjectt = FilterUserProjects(App.db.Production_Project.ToList());
            if (datePicker1.SelectedDate > datePicker2.SelectedDate)
            {
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
            }
            else if (datePicker1.SelectedDate <= datePicker2.SelectedDate)
            {
                DateTime dateTimeStart = datePicker1.SelectedDate.Value;
                string formatedDate = dateTimeStart.ToString("yyyy.MM.dd");
                DateTime dateTimeEnd = datePicker2.SelectedDate.Value;
                string formatedDate1 = dateTimeEnd.ToString("yyyy.MM.dd");
                List<Production_Project> sortDate = new List<Production_Project>();
                sortDate.Clear();
                foreach (Production_Project prodProject in FilterUserProjects(curProjectt))
                {
                    if (DateTime.Parse(formatedDate) <= prodProject.Start_Datee && DateTime.Parse(formatedDate1) >= prodProject.Start_Datee)
                        sortDate.Add(prodProject);
                }
                curProjectt = sortDate;
            }
            if (ComboBox_Category.SelectedIndex != -1)
            {
                string category = ComboBox_Category.SelectedValue.ToString();
                List<Production_Project> sortComboBox = new List<Production_Project>();
                sortComboBox.Clear();
                foreach (Production_Project prodProject in FilterUserProjects(curProjectt))
                {
                    if (category.Equals(prodProject.Statuss.Namee))
                        sortComboBox.Add(prodProject);
                }
                curProjectt = sortComboBox;
            }
            if (TextBox_Search.Text.Length > 0)
            {
                List<Production_Project> sortTextBoxSearch = new List<Production_Project>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                var sortTextBox = db.Production_Project.Where(p => p.id_Project == p.id_Project &&
                (p.Namee.ToString().ToLower().Contains(searchText) || p.Descriptionn.ToString().ToLower().Contains(searchText)
                || p.Budget.ToString().ToLower().Contains(searchText) || p.Resourcee.Namee.ToLower().Contains(searchText))).ToList();
                foreach (Production_Project prodProject in FilterUserProjects(curProjectt))
                {
                    if (prodProject.id_Project == prodProject.id_Project)
                    {
                        sortTextBoxSearch = curProjectt.Where(p => p.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Descriptionn.ToString().ToLower().Contains(searchText) || p.Budget.ToString().ToLower().Contains(searchText) ||
                        p.Resourcee.Namee.ToLower().Contains(searchText)).ToList();
                    }
                }
                curProjectt = sortTextBoxSearch;
            }
                TableUsers.ItemsSource = curProjectt;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Information.idRole == 1)
                {
                    if (MessageBox.Show("Вы действительно хотите выйти из системы?", "Внимание",
                        MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        Information.idRole = null;
                        Information.idUser = null;
                        Information.Name = null;
                        Information.Surname = null;
                        Information.Patronymic = null;
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                MainAdministrator mainAdministrator = new MainAdministrator();
                mainAdministrator.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_TeamProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InformationProjectTeam teamProject = new InformationProjectTeam();
                teamProject.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_AddProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;

                AddProject addProject = new AddProject();
                addProject.ShowDialog();
                UpdateDG();
                Table.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_RedactProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    ComboBox_Category.ItemsSource = null;
                    TextBox_Search.Text = "";
                    ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
                    datePicker1.SelectedDate = DateTime.MinValue;
                    datePicker2.SelectedDate = DateTime.MaxValue;

                    Production_Project production_Project = (Production_Project)Table.SelectedItem;
                    RedactProject redact = new RedactProject(production_Project);
                    redact.ShowDialog();
                    UpdateDG();
                    Table.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Вы не выбрали строку для редактирования!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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

        // Метод удаления проекта
        private void Button_RemoveProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    if (MessageBox.Show("Вы уверены что хотите удалить проект? Все задачи, команды и документация выбранного проекта удалятся!", "Внимание", MessageBoxButton.OKCancel,
                        MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        ComboBox_Category.ItemsSource = null;
                        TextBox_Search.Text = "";
                        ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
                        datePicker1.SelectedDate = DateTime.MinValue;
                        datePicker2.SelectedDate = DateTime.MaxValue;

                        var selectedProject = Table.SelectedItem as Production_Project;
                        if (selectedProject != null)
                        {
                            var project = App.db.Production_Project.FirstOrDefault(x => x.id_Project == selectedProject.id_Project);

                            var DeleteTasks = App.db.Tasks.Where(x => x.id_Project == project.id_Project).ToList();
                            if (DeleteTasks.Any())
                            {
                                App.db.Tasks.RemoveRange(DeleteTasks);
                            }

                            var DeleteTeamProject = App.db.Project_Team.Where(x => x.id_Project == project.id_Project).ToList();
                            if (DeleteTeamProject.Any())
                            {
                                App.db.Project_Team.RemoveRange(DeleteTeamProject);
                            }

                            var DeleteDocuments = App.db.Documentation.Where(x => x.id_Project == project.id_Project).ToList();
                            if(DeleteDocuments.Any())
                            {
                                App.db.Documentation.RemoveRange(DeleteDocuments);
                            }
                        }

                        var select = Table.SelectedItem;
                        Production_Project production_Project = (Production_Project)select;
                        if (production_Project != null)
                        {
                            App.db.Production_Project.Remove(production_Project);
                        }
                        App.db.SaveChanges();
                        UpdateDG();
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
                    MessageBox.Show("Вы не выбрали строку для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void ComboBox_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Information.idRole == 1)
            {
                UpdateDGUsers();
            }
            else if (Information.idRole >= 2)
            {
                UpdateDG();
            }
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Information.idRole == 1)
            {
                UpdateDGUsers();
            }
            else if (Information.idRole >= 2)
            {
                UpdateDG();
            }
        }

        private void Button_ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            if (Information.idRole == 1)
            {
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                UpdateDGUsers();
                MessageBox.Show("Фильтрация успешно сброшена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (Information.idRole >= 2)
            {
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                UpdateDG();
                MessageBox.Show("Фильтрация успешно сброшена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void Button_Tasks_Click(object sender, RoutedEventArgs e)
        {
            InformationTasks informationTasks = new InformationTasks();
            informationTasks.Show();
            this.Close();
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

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Information.idRole == 1)
            {
                if (datePicker1.Text == "Выбор даты")
                {
                    datePicker1.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker1.Text == null)
                {
                    datePicker1.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker1.Text == "")
                {
                    datePicker1.SelectedDate = DateTime.MaxValue;
                    return;
                }
                UpdateDGUsers();
            }
            else if (Information.idRole >= 2)
            {
                if (datePicker1.Text == "Выбор даты")
                {
                    datePicker1.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker1.Text == null)
                {
                    datePicker1.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker1.Text == "")
                {
                    datePicker1.SelectedDate = DateTime.MaxValue;
                    return;
                }
                UpdateDG();
            }
        }

        private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Information.idRole == 1)
            {
                if (datePicker2.Text == "Выбор даты")
                {
                    datePicker2.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker2.Text == null)
                {
                    datePicker2.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker2.Text == "")
                {
                    datePicker2.SelectedDate = DateTime.MaxValue;
                    return;
                }
                UpdateDGUsers();
            }
            else if (Information.idRole >= 2)
            {
                if (datePicker2.Text == "Выбор даты")
                {
                    datePicker2.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker2.Text == null)
                {
                    datePicker2.SelectedDate = DateTime.MaxValue;
                    return;
                }
                if (datePicker2.Text == "")
                {
                    datePicker2.SelectedDate = DateTime.MaxValue;
                    return;
                }
                UpdateDG();
            }
        }

        private void NewsAndEvent_Click(object sender, RoutedEventArgs e)
        {
            EventsAndNews eventsAndNews = new EventsAndNews();
            eventsAndNews.Show();
            this.Close();
        }
    }
}
