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
    /// Логика взаимодействия для InformationTasks.xaml
    /// </summary>
    public partial class InformationTasks : Window
    {
        is1_25_kokorinds_Kursovoy_ProjectEntities1 db = new is1_25_kokorinds_Kursovoy_ProjectEntities1();
        public InformationTasks()
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
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                ComboBox_Category.ItemsSource = App.db.Priorityy.ToList();
                UpdateEmployee();
            }
            if(Information.idRole >= 2)
            {
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                ComboBox_Category.ItemsSource = App.db.Priorityy.ToList();
                UpdateManagerAndAdministrator();
            }
        }
        List<Tasks> curUser(List<Tasks> user)
        {
            return user.Where(x => x.id_User == Information.idUser).ToList();
        }

        void UpdateEmployee()
        {
            List<Tasks> curTasks = curUser(App.db.Tasks.ToList());
            if(datePicker1.SelectedDate > datePicker2.SelectedDate)
            {
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
            }
            else if(datePicker1.SelectedDate <= datePicker2.SelectedDate)
            {
                DateTime dateTimeStart = datePicker1.SelectedDate.Value;
                string formatedDate = dateTimeStart.ToString("yyyy.MM.dd");
                DateTime dateTimeEnd = datePicker2.SelectedDate.Value;
                string formatedDate1 = dateTimeEnd.ToString("yyyy.MM.dd");
                List<Tasks> sortDate = new List<Tasks>();
                sortDate.Clear();
                foreach (Tasks tasks in curUser(curTasks))
                {
                    if (DateTime.Parse(formatedDate) <= tasks.Start_Datee && DateTime.Parse(formatedDate1) >= tasks.Start_Datee)
                        sortDate.Add(tasks);
                }
                curTasks = sortDate;
            }
            if (ComboBox_Category.SelectedIndex != -1)
            {
                string category = ComboBox_Category.SelectedValue.ToString();
                List<Tasks> sortComboBox = new List<Tasks>();
                sortComboBox.Clear();
                foreach (Tasks tasks in curUser(curTasks))
                {
                    if (category.Equals(tasks.Priorityy.Namee))
                        sortComboBox.Add(tasks);
                }
                curTasks = sortComboBox;
            }
            if (TextBox_Search.Text.Length > 0)
            {
                List<Tasks> sortTextBoxSearch = new List<Tasks>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                var sortTextBox = db.Tasks.Where(p => p.id_Tasks == Information.idUser && (p.Production_Project.Namee.ToString().ToLower().Contains(searchText) ||
                p.Namee.ToString().ToLower().Contains(searchText) || p.Descriptionn.ToString().ToLower().Contains(searchText) ||
                p.Time_Spent.ToString().Contains(searchText))).ToList();
                foreach (Tasks tasks in curUser(curTasks))
                {
                    if (tasks.id_Project == tasks.id_Project)
                    {
                        sortTextBoxSearch = curTasks.Where(p => p.Production_Project.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Namee.ToString().ToLower().Contains(searchText) || p.Descriptionn.ToString().ToLower().Contains(searchText) ||
                        p.Time_Spent.ToString().Contains(searchText)).ToList();
                    }
                }
                curTasks = sortTextBoxSearch;
            }
            Table.ItemsSource = curTasks;
        }

        List<Tasks> curTasks(List<Tasks> tasks)
        {
            return tasks.Where(x => x.id_Tasks == x.id_Tasks).ToList();
        }

        void UpdateManagerAndAdministrator()
        {
            List<Tasks> taskS = curTasks(App.db.Tasks.ToList());
            if (datePicker1.SelectedDate > datePicker2.SelectedDate)
            {
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
            }
            else if(datePicker1.SelectedDate <= datePicker2.SelectedDate)
            {
                DateTime dateTimeStart = datePicker1.SelectedDate.Value;
                string formatedDate = dateTimeStart.ToString("yyyy.MM.dd");
                DateTime dateTimeEnd = datePicker2.SelectedDate.Value;
                string formatedDate1 = dateTimeEnd.ToString("yyyy.MM.dd");
                List<Tasks> sortDate = new List<Tasks>();
                sortDate.Clear();
                foreach (Tasks tasks in curTasks(taskS))
                {
                    if (DateTime.Parse(formatedDate) <= tasks.Start_Datee && DateTime.Parse(formatedDate1) >= tasks.Start_Datee)
                        sortDate.Add(tasks);
                }
                taskS = sortDate;
            }
            if (ComboBox_Category.SelectedIndex != -1)
            {
                string category = ComboBox_Category.SelectedValue.ToString();
                List<Tasks> sortComboBox = new List<Tasks>();
                sortComboBox.Clear();
                foreach (Tasks tasks in curTasks(taskS))
                {
                    if (category.Equals(tasks.Priorityy.Namee))
                        sortComboBox.Add(tasks);
                }
                taskS = sortComboBox;
            }
            if (TextBox_Search.Text.Length > 0)
            {
                List<Tasks> sortTextBoxSearch = new List<Tasks>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                var sortTextBox = db.Tasks.Where(p => p.id_Tasks == p.id_Tasks && (p.Production_Project.Namee.ToString().ToLower().Contains(searchText) ||
                p.Namee.ToString().ToLower().Contains(searchText) || p.Descriptionn.ToString().ToLower().Contains(searchText) ||
                p.Time_Spent.ToString().Contains(searchText))).ToList();
                foreach (Tasks tasks in curTasks(taskS))
                {
                    if (tasks.id_Tasks == tasks.id_Tasks)
                    {
                        sortTextBoxSearch = taskS.Where(p => p.Production_Project.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Namee.ToString().ToLower().Contains(searchText) || p.Descriptionn.ToString().ToLower().Contains(searchText) ||
                        p.Time_Spent.ToString().Contains(searchText)).ToList();
                    }
                }
                taskS = sortTextBoxSearch;
            }
            Table.ItemsSource = taskS;
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

        private void ComboBox_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Information.idRole == 1)
            {
                UpdateEmployee();
            }
            if(Information.idRole >= 2)
            {
                UpdateManagerAndAdministrator();
            }
        }

        private void Button_AddTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;

                AddTasks addTasks = new AddTasks();
                addTasks.ShowDialog();
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

        private void Button_RedactTasks_Click(object sender, RoutedEventArgs e)
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

                    Tasks tasks = (Tasks)Table.SelectedItem;
                    RedactTasks redact = new RedactTasks(tasks);
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

        private void Button_DeleteTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    if (MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", 
                        MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        ComboBox_Category.ItemsSource = null;
                        TextBox_Search.Text = "";
                        ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
                        datePicker1.SelectedDate = DateTime.MinValue;
                        datePicker2.SelectedDate = DateTime.MaxValue;

                        var select = Table.SelectedItem;
                        Tasks tasks = (Tasks)select;
                        if (tasks != null)
                        {
                            App.db.Tasks.Remove(tasks);
                        }
                        App.db.SaveChanges();
                        UpdateManagerAndAdministrator();
                        Table.SelectedIndex -= 1;
                    }
                    else
                    {
                        Table.SelectedIndex = -1;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали строчку для удаления!", "Внимание", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Priorityy.ToList();
                datePicker1.SelectedDate = DateTime.MinValue;
                datePicker2.SelectedDate = DateTime.MaxValue;
                if (Information.idRole == 1)
                {
                    UpdateEmployee();
                    MessageBox.Show("Фильтрация успешно сброшена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                if (Information.idRole >= 2)
                {
                    UpdateManagerAndAdministrator();
                    MessageBox.Show("Фильтрация успешно сброшена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void TextBox_Search_SelectionChanged(object sender, RoutedEventArgs e)
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
            if(Information.idRole == 1)
            {
                if(datePicker1.Text == "Выберите дату")
                {
                    datePicker1.SelectedDate = DateTime.MinValue;
                    return;
                }
                if(datePicker1.Text == null)
                {
                    datePicker1.SelectedDate = DateTime.MinValue;
                    return;
                }
                if(datePicker1.Text == "")
                {
                    datePicker1.SelectedDate = DateTime.MinValue;
                    return;
                }
                UpdateEmployee();
            }
            if (Information.idRole >= 2)
            {
                if (datePicker1.Text == "Выберите дату")
                {
                    datePicker1.SelectedDate = DateTime.MinValue;
                    return;
                }
                if (datePicker1.Text == null)
                {
                    datePicker1.SelectedDate = DateTime.MinValue;
                    return;
                }
                if (datePicker1.Text == "")
                {
                    datePicker1.SelectedDate = DateTime.MinValue;
                    return;
                }
                UpdateManagerAndAdministrator();
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
                UpdateEmployee();
            }
            if (Information.idRole >= 2)
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
                UpdateManagerAndAdministrator();
            }
        }

        private void ReadyStatus_Click(object sender, RoutedEventArgs e)
        {
            if(Table.SelectedItem != null)
            {
                if(MessageBox.Show("Вы действительно хотите сообщить о завершении задачи?", "Вопрос", MessageBoxButton.YesNo,
                    MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    Tasks tasks = (Tasks)Table.SelectedItem;
                    if(tasks.id_Status == 3)
                    {
                        MessageBox.Show("Задача уже имеет статус 'Завершено'!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        Table.SelectedItem = -1;
                        return;
                    }
                    tasks.id_Status = 3;
                    App.db.SaveChanges();
                    UpdateEmployee();
                    MessageBox.Show("Статус успешно изменен!", "Успешно", MessageBoxButton.OK,
                        MessageBoxImage.Asterisk);
                    return;
                }
                else
                {
                    Table.SelectedItem = -1;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали задачу!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        }
    }
}
