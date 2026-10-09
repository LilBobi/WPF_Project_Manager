using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для AddRedactDocuments.xaml
    /// </summary>
    public partial class AddRedactDocuments : Window
    {
        Models.Documentation docc;

        public AddRedactDocuments()
        {
            InitializeComponent();
            ComboBox_Project.ItemsSource = App.db.Production_Project.ToList();
            ComboBox_Status.ItemsSource = App.db.Statuss.ToList();
        }

        public AddRedactDocuments(Models.Documentation documentation)
        {
            try
            {
                InitializeComponent();
                ComboBox_Project.ItemsSource = App.db.Production_Project.ToList();
                ComboBox_Status.ItemsSource = App.db.Statuss.ToList();
                docc = documentation;
                ComboBox_Project.Text = documentation.Production_Project.Namee;
                ComboBox_Status.Text = documentation.Statuss.Namee;
                TextBox_Progress.Text = documentation.Progress;
                TextBox_Cost.Text = documentation.Cost.ToString();
                TextBox_Profit.Text = documentation.Profit.ToString();
                LabelName.Content = "Редактирование документа";
                Button_AddDocument.Content = "Редактировать";
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

        private void Button_Unwrap_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_AddDocument_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Documentation documentation = new Models.Documentation();
                if (InformationAddRedact.AddRedactDocuments == 0)
                {
                    if (!Check())
                    {
                        return;
                    }
                    documentation.id_Status = (int)ComboBox_Status.SelectedValue;
                    documentation.id_Project = (int)ComboBox_Project.SelectedValue;
                    documentation.Progress = TextBox_Progress.Text;
                    string zat = TextBox_Cost.Text;
                    string normalizedInput = zat.Replace('.', ',');
                    double value = double.Parse(normalizedInput, CultureInfo.GetCultureInfo("ru-Ru"));
                    documentation.Cost = decimal.Parse(value.ToString());
                    string prib = TextBox_Profit.Text;
                    string normalizedInputTwo = prib.Replace('.', ',');
                    double valueTwo = double.Parse(normalizedInputTwo, CultureInfo.GetCultureInfo("ru-Ru"));
                    documentation.Profit = decimal.Parse(valueTwo.ToString());
                    App.db.Documentation.Add(documentation);
                    App.db.SaveChanges();
                    InformationAddRedact.AddRedactDocuments = 0;
                    MessageBox.Show("Документ успешно добавлен!", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.Close();
                    return;
                }
                if (InformationAddRedact.AddRedactDocuments == 1)
                {
                    if (!Check())
                    {
                        return;
                    }
                    documentation = docc;
                    documentation.id_Status = (int)ComboBox_Status.SelectedValue;
                    documentation.id_Project = (int)ComboBox_Project.SelectedValue;
                    documentation.Progress = TextBox_Progress.Text;
                    string zat = TextBox_Cost.Text;
                    string normalizedInput = zat.Replace('.', ',');
                    double value = double.Parse(normalizedInput, CultureInfo.GetCultureInfo("ru-Ru"));
                    documentation.Cost = decimal.Parse(value.ToString());
                    string prib = TextBox_Profit.Text;
                    string normalizedInputTwo = prib.Replace('.', ',');
                    double valueTwo = double.Parse(normalizedInputTwo, CultureInfo.GetCultureInfo("ru-Ru"));
                    documentation.Profit = decimal.Parse(valueTwo.ToString());
                    App.db.SaveChanges();
                    InformationAddRedact.AddRedactDocuments = 0;
                    MessageBox.Show("Документ успешно редактирован!", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.Close();
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

        bool Check()
        {
            if (ComboBox_Project.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали проект!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (ComboBox_Status.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали статус!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Progress.Text == "")
            {
                MessageBox.Show("Вы не написали прогресс!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Regex regexx = new Regex(@"^-?\d*[.,]?\d*$");
            string txtt = TextBox_Progress.Text;
            if (!regexx.IsMatch(txtt))
            {
                MessageBox.Show("Неверный ввод прогресса! Введите от 0 до 100.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string Prog = TextBox_Progress.Text;
            if(int.TryParse(Prog, out int number))
            {
                if(number < 0 || number > 100)
                {
                    MessageBox.Show("Вы неверно указали прогресс! Укажите от 0 до 100", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (TextBox_Cost.Text == "")
            {
                MessageBox.Show("Вы не написали затраты!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Regex regex = new Regex(@"^\d+([,.]\d{1,2})?$");
            string txt = TextBox_Cost.Text;
            if (!regex.IsMatch(txt))
            {
                MessageBox.Show("Неверный ввод затрат! Пример 2500,00 или 2500.50", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string zat = TextBox_Cost.Text;
            string normalizedInput = zat.Replace('.', ',');
            double value = double.Parse(normalizedInput, CultureInfo.GetCultureInfo("ru-Ru"));
            if(value < 0)
            {
                MessageBox.Show("Сумма затрат не может быть меньше 0!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Profit.Text == "")
            {
                MessageBox.Show("Вы не написали прибыль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Regex regex1 = new Regex(@"^\d+([,.]\d{1,2})?$");
            string txt1 = TextBox_Profit.Text;
            if (!regex1.IsMatch(txt1))
            {
                MessageBox.Show("Неверный ввод прибыли! Пример 5000,00 или 5000.50", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string prib = TextBox_Profit.Text;
            string normalizedInputTwo = prib.Replace('.', ',');
            double valueTwo = double.Parse(normalizedInputTwo, CultureInfo.GetCultureInfo("ru-Ru"));
            if (valueTwo < 0)
            {
                MessageBox.Show("Сумма прибыли не может быть меньше 0!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
