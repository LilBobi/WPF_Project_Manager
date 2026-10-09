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
    /// Логика взаимодействия для AddRedactNews.xaml
    /// </summary>
    public partial class AddRedactNews : Window
    {
        News newss;
        public AddRedactNews()
        {
            InitializeComponent();
            List<Userr> userrs = App.db.Userr.ToList();
            var filerUser = userrs.Where(userr => userr.id_Role >= 2).ToList();
            ComboBox_User.ItemsSource = filerUser;
        }

        public AddRedactNews(News news)
        {
            try
            {
                InitializeComponent();
                List<Userr> userrs = App.db.Userr.ToList();
                var filerUser = userrs.Where(userr => userr.id_Role >= 2).ToList();
                ComboBox_User.ItemsSource = filerUser;
                newss = news;
                ComboBox_User.Text = news.Userr.Surname;
                TextBox_Heading.Text = news.Heading;
                TextBox_Description.Text = news.Descriptionn;
                datePicker1.SelectedDate = news.Publication_Date;
                LabelName.Content = "Редактирование новости";
                Button_AddNews.Content = "Редактировать";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_AddNews_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                News news = new News();
                if (InformationAddRedact.AddRedactNews == 0)
                {
                    if (!Check())
                    {
                        return;
                    }
                    news.id_User = (int)ComboBox_User.SelectedValue;
                    news.Heading = TextBox_Heading.Text;
                    news.Descriptionn = TextBox_Description.Text;
                    news.Publication_Date = DateTime.Parse(datePicker1.ToString());
                    App.db.News.Add(news);
                    App.db.SaveChanges();
                    InformationAddRedact.AddRedactNews = 0;
                    MessageBox.Show("Новость успешно добавлена!", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.Close();
                    return;
                }
                else if (InformationAddRedact.AddRedactNews == 1)
                {
                    if (!Check())
                    {
                        return;
                    }
                    news = newss;
                    news.id_User = (int)ComboBox_User.SelectedValue;
                    news.Heading = TextBox_Heading.Text;
                    news.Descriptionn = TextBox_Description.Text;
                    news.Publication_Date = DateTime.Parse(datePicker1.ToString());
                    App.db.SaveChanges();
                    InformationAddRedact.AddRedactNews = 0;
                    MessageBox.Show("Новость успешно редактирована!", "Успешно",
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

        private void Button_Unwrap_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        bool Check()
        {
            if (ComboBox_User.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали сотрудника!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Heading.Text == "")
            {
                MessageBox.Show("Вы не написали заголовок новости!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Heading.Text.Length > 200)
            {
                MessageBox.Show("Заголовок новости не может превышать 200 букв!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Description.Text == "")
            {
                MessageBox.Show("Вы не написали описание новости!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Description.Text.Length > 200)
            {
                MessageBox.Show("Описание новости не может превышать 200 букв!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (datePicker1.Text == "")
            {
                MessageBox.Show("Вы не выбрали дату публикации новости!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker1.Text == null)
            {
                MessageBox.Show("Вы не выбрали дату публикации новости!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker1.Text == "Выбор даты")
            {
                MessageBox.Show("Вы не выбрали дату публикации новости!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (!DateTime.TryParse(datePicker1.Text, out DateTime a))
            {
                MessageBox.Show("Вы неправильно выбрали или написали дату публикации новости! Пример: 12.12.2024", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            DateTime mindate = new DateTime(2024, 1, 1);
            DateTime maxdate = DateTime.Now.AddYears(2);
            if (datePicker1.SelectedDate < mindate)
            {
                MessageBox.Show($"Дата публикации не может быть выставлена раньше {mindate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker1.SelectedDate > maxdate)
            {
                MessageBox.Show($"Дата публикации не может быть выставлена позже {maxdate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            return true;
        }
    }
}
