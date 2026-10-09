using Project_Menedger.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        public AddProject()
        {
            InitializeComponent();
            UpdateComboBox();
        }

        void UpdateComboBox()
        {
            ComboBox_Category.ItemsSource = App.db.Statuss.ToList();
            ComboBox_Resource.ItemsSource = App.db.Resourcee.ToList();
        }

        string formatedDateOne;
        string formatedDateTwo;

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // Метод добавления проекта
        private void Button_AddProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Check())
                {
                    return;
                }
                Production_Project project = new Production_Project();
                project.Namee = TextBox_NameProject.Text;
                project.Descriptionn = TextBox_Description.Text;
                project.Start_Datee = DateTime.Parse(formatedDateOne.ToString());
                    project.End_Datee = DateTime.Parse(formatedDateTwo.ToString());
                string zat = TextBox_Budget.Text;
                string normalizedInput = zat.Replace('.', ',');
                double value = double.Parse(normalizedInput, CultureInfo.GetCultureInfo("ru-Ru"));
                project.Budget = decimal.Parse(value.ToString());
                project.id_Status = (int)ComboBox_Category.SelectedValue;
                project.id_Resource = (int)ComboBox_Resource.SelectedValue;
                App.db.Production_Project.Add(project);
                App.db.SaveChanges();
                MessageBox.Show("Проект был успешно добавлен!", "Успешно", MessageBoxButton.OK,
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

        bool Check()
        {
            if (TextBox_NameProject.Text == "")
            {
                MessageBox.Show("Вы не написали название проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (datePicker1.Text == "")
            {
                MessageBox.Show("Вы не выбрали дату начала проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if(datePicker1.Text == null)
            {
                MessageBox.Show("Вы не выбрали дату начала проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate= null;
                return false;
            }
            if(datePicker1.Text == "Выбор даты")
            {
                MessageBox.Show("Вы не выбрали дату начала проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate= null;
                return false;
            }    
            if (!DateTime.TryParse(datePicker1.Text, out DateTime a))
            {
                MessageBox.Show("Вы неправильно выбрали или написали дату начала проекта! Пример: 12.12.2024", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            DateTime mindate = new DateTime(2024, 1, 1);
            DateTime maxdate = DateTime.Now.AddYears(2);
            if (datePicker1.SelectedDate < mindate)
            {
                MessageBox.Show($"Дата начала проекта не может быть выставлена раньше {mindate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker2.Text == "")
                {
                    MessageBox.Show("Вы не выбрали дату окончания проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if (datePicker2.Text == null)
                {
                    MessageBox.Show("Вы не выбрали дату окончания проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if (datePicker2.Text == "Выбор даты")
                {
                    MessageBox.Show("Вы не выбрали дату окончания проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if (!DateTime.TryParse(datePicker2.Text, out DateTime aa))
                {
                    MessageBox.Show("Вы неправильно выбрали или написали дату окончания проекта! Пример: 12.12.2024", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate= null;
                    return false;
                }
                if(datePicker1.SelectedDate > datePicker2.SelectedDate)
                {
                    MessageBox.Show("Дата начала проекта не может быть больше даты окончания проекта!", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
                }
                if(datePicker2.SelectedDate > maxdate)
            {
                MessageBox.Show($"Дата окончания проекта не может быть выставлена позже {maxdate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker2.SelectedDate = null;
                return false;
            }
            if (TextBox_Budget.Text == "")
            {
                MessageBox.Show("Вы не указали бюджет проекта!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Regex regex = new Regex(@"^\d+([,.]\d{1,2})?$");
            string txt = TextBox_Budget.Text;
            if (!regex.IsMatch(txt))
            {
                MessageBox.Show("Неверный ввод бюджета! Пример 30000,00 или 30000.50", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string zat = TextBox_Budget.Text;
            string normalizedInput = zat.Replace('.', ',');
            double value = double.Parse(normalizedInput, CultureInfo.GetCultureInfo("ru-Ru"));
            if (value < 0)
            {
                MessageBox.Show("Сумма бюджета не может быть меньше 0!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (ComboBox_Category.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали статус!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (ComboBox_Resource.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали ресурс!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
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
