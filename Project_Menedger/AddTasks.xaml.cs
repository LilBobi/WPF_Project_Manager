using Project_Menedger.Models;
using Project_Menedger.Properties;
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
    /// Логика взаимодействия для AddTasks.xaml
    /// </summary>
    public partial class AddTasks : Window
    {
        public AddTasks()
        {
            InitializeComponent();
            UpdateComboBox();
        }
        void UpdateComboBox()
        {
            ComboBox_Priority.ItemsSource = App.db.Priorityy.ToList();
            ComboBox_Status.ItemsSource = App.db.Statuss.ToList();
            List<Production_Project> Proj = App.db.Production_Project.ToList();
            var filerProject = Proj.Where(proj => proj.id_Status != 3).ToList();
            ComboBox_Project.ItemsSource = filerProject;
            List<Userr> userrs = App.db.Userr.ToList();
            var filerUser = userrs.Where(userr => userr.id_Role == 1).ToList();
            ComboBox_Employee.ItemsSource = filerUser;
        }

        string formatedDateOne;
        string formatedDateTwo;

        private void Button_AddTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Check())
                {
                    return;
                }
                Tasks tasks = new Tasks();
                tasks.id_Project = (int)ComboBox_Project.SelectedValue;
                tasks.Namee = TextBox_TasksName.Text;
                tasks.Descriptionn = TextBox_Description.Text;
                tasks.id_Priority = (int)ComboBox_Priority.SelectedValue;
                tasks.Start_Datee = DateTime.Parse(formatedDateOne.ToString());
                    tasks.End_Datee = DateTime.Parse(formatedDateTwo.ToString());
                tasks.id_Status = (int)ComboBox_Status.SelectedValue;
                if(TextBox_TimeProject.Text == "")
                {
                    tasks.Time_Spent = null;
                }
                else if(TextBox_TimeProject.Text != null)
                {
                    tasks.Time_Spent = TextBox_TimeProject.Text;
                }
                tasks.id_User = (int)ComboBox_Employee.SelectedValue;
                App.db.Tasks.Add(tasks);
                App.db.SaveChanges();
                MessageBox.Show("Задача была успешно добавлена!", "Успешно", MessageBoxButton.OK,
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
            if(TextBox_TasksName.Text == "")
            {
                MessageBox.Show("Вы не написали название задачи!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(ComboBox_Priority.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали приоритет!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (datePicker1.Text == "")
            {
                MessageBox.Show("Вы не выбрали дату начала задачи!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker1.Text == null)
            {
                MessageBox.Show("Вы не выбрали дату начала задачи!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker1.Text == "Выбор даты")
            {
                MessageBox.Show("Вы не выбрали дату начала задачи!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (!DateTime.TryParse(datePicker1.Text, out DateTime a))
            {
                MessageBox.Show("Вы неправильно выбрали или написали дату начала задачи! Пример: 12.12.2024", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            DateTime mindate = new DateTime(2024, 1, 1);
            DateTime maxdate = DateTime.Now.AddYears(2);
            if (datePicker1.SelectedDate < mindate)
            {
                MessageBox.Show($"Дата начала задачи не может быть выставлена раньше {mindate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
                if (datePicker2.Text == "")
                {
                    MessageBox.Show("Вы не выбрали дату окончания задачи!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if (datePicker2.Text == null)
                {
                    MessageBox.Show("Вы не выбрали дату окончания задачи!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if (datePicker2.Text == "Выбор даты")
                {
                    MessageBox.Show("Вы не выбрали дату окончания задачи!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if (!DateTime.TryParse(datePicker1.Text, out DateTime aa))
                {
                    MessageBox.Show("Вы неправильно выбрали или написали дату окончания задачи! Пример: 12.12.2024", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if (datePicker1.SelectedDate > datePicker2.SelectedDate)
                {
                    MessageBox.Show("Дата начала задачи не может быть больше даты окончания задачи!", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker1.SelectedDate = null;
                    datePicker2.SelectedDate = null;
                    return false;
                }
            if (datePicker2.SelectedDate > maxdate)
            {
                MessageBox.Show($"Дата окончания задачи не может быть выставлена позже {maxdate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker2.SelectedDate = null;
                return false;
            }
            if (ComboBox_Status.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали статус!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            var time = TextBox_TimeProject.Text;
            string timeFilter = @"^(?:\d{1,3}):[0-5]\d:[0-5]\d$";
            if(time != string.Empty)
            {
                if (!Regex.IsMatch(time, timeFilter))
                {
                    MessageBox.Show("Вы неправильно указали затраченное время! Пример: 05:30:00 или 120:20:00", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if(ComboBox_Employee.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали сотрудника!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker1.Text == "Выбор даты")
            {
                datePicker1.SelectedDate = null;
                return;
            }
            if (datePicker1.Text == null)
            {
                datePicker1.SelectedDate = null;
                return;
            }
            if (datePicker1.Text == "")
            {
                datePicker1.SelectedDate = null;
                return;
            }
            DateTime selectedDate = datePicker1.SelectedDate.Value;
            formatedDateOne = selectedDate.ToString("yyyy.MM.dd");
        }

        private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker2.Text == "Выбор даты")
            {
                datePicker2.SelectedDate = null;
                return;
            }
            if (datePicker2.Text == null)
            {
                datePicker2.SelectedDate = null;
                return;
            }
            if (datePicker2.Text == "")
            {
                datePicker2.SelectedDate = null;
                return;
            }
            DateTime selectedDate = datePicker2.SelectedDate.Value;
            formatedDateTwo = selectedDate.ToString("yyyy.MM.dd");
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
