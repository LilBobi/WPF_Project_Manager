using Project_Menedger.Models;
using Project_Menedger.Properties;
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
    /// Логика взаимодействия для AddRedactEvent.xaml
    /// </summary>
    public partial class AddRedactEvent : Window
    {
        Eventt events;

        public AddRedactEvent()
        {
            InitializeComponent();
            List<Userr> userrs = App.db.Userr.ToList();
            var filerUser = userrs.Where(userr => userr.id_Role >= 2).ToList();
            ComboBox_User.ItemsSource = filerUser;
        }

        public AddRedactEvent(Eventt eventt)
        {
            try
            {
                InitializeComponent();
                List<Userr> userrs = App.db.Userr.ToList();
                var filerUser = userrs.Where(userr => userr.id_Role >= 2).ToList();
                ComboBox_User.ItemsSource = filerUser;
                events = eventt;
                TextBox_NameEvent.Text = eventt.Namee;
                ComboBox_User.Text = eventt.Userr.Surname;
                TextBox_Description.Text = eventt.Descriptionn;
                datePicker1.SelectedDate = eventt.Start_Datee;
                datePicker2.SelectedDate = eventt.End_Date;
                TextBox_Place.Text = eventt.Place;
                LabelName.Content = "Редактирование мероприятия";
                Button_AddEvent.Content = "Редактировать";
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
                Eventt eventt = new Eventt();
                if (InformationAddRedact.AddRedactEvents == 0)
                {
                    if (!Check())
                    {
                        return;
                    }
                    eventt.id_User = (int)ComboBox_User.SelectedValue;
                    eventt.Namee = TextBox_NameEvent.Text;
                    eventt.Descriptionn = TextBox_Description.Text;
                    eventt.Start_Datee = DateTime.Parse(datePicker1.ToString());
                    eventt.End_Date = DateTime.Parse(datePicker2.ToString());
                    eventt.Place = TextBox_Place.Text;
                    App.db.Eventt.Add(eventt);
                    App.db.SaveChanges();
                    InformationAddRedact.AddRedactEvents = 0;
                    MessageBox.Show("Данные о мероприятии успешно добавлены!", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.Close();
                    return;
                }
                else if (InformationAddRedact.AddRedactEvents == 1)
                {
                    if (!Check())
                    {
                        return;
                    }
                    eventt = events;
                    eventt.id_User = (int)ComboBox_User.SelectedValue;
                    eventt.Namee = TextBox_NameEvent.Text;
                    eventt.Descriptionn = TextBox_Description.Text;
                    eventt.Start_Datee = DateTime.Parse(datePicker1.ToString());
                    eventt.End_Date = DateTime.Parse(datePicker2.ToString());
                    eventt.Place = TextBox_Place.Text;
                    App.db.SaveChanges();
                    InformationAddRedact.AddRedactEvents = 0;
                    MessageBox.Show("Данные о мероприятии успешно редактированы!", "Успешно",
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

        private void Button_Back_Click(object sender, RoutedEventArgs e)
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

        private void Button_Close_Click(object sender, RoutedEventArgs e)
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
            if(TextBox_NameEvent.Text == "")
            {
                MessageBox.Show("Вы не написали название мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(TextBox_NameEvent.Text.Length > 150)
            {
                MessageBox.Show("Название мероприятия не может превышать 150 букв!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(TextBox_Description.Text == "")
            {
                MessageBox.Show("Вы не написали описание мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(TextBox_Description.Text.Length > 150)
            {
                MessageBox.Show("Описание мероприятия не может превышать 150 букв!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (datePicker1.Text == "")
            {
                MessageBox.Show("Вы не выбрали дату начала мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker1.Text == null)
            {
                MessageBox.Show("Вы не выбрали дату начала мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker1.Text == "Выбор даты")
            {
                MessageBox.Show("Вы не выбрали дату начала мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (!DateTime.TryParse(datePicker1.Text, out DateTime a))
            {
                MessageBox.Show("Вы неправильно выбрали или написали дату начала мероприятия! Пример: 12.12.2024", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            DateTime mindate = new DateTime(2024, 1, 1);
            DateTime maxdate = DateTime.Now.AddYears(2);
            if (datePicker1.SelectedDate < mindate)
            {
                MessageBox.Show($"Дата начала мероприятия не может быть выставлена раньше {mindate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker1.SelectedDate = null;
                return false;
            }
            if (datePicker2.Text == "")
            {
                    MessageBox.Show("Вы не выбрали дату окончания мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
            }
            if (datePicker2.Text == null)
            {
                    MessageBox.Show("Вы не выбрали дату окончания мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
            }
            if (datePicker2.Text == "Выбор даты")
            {
                    MessageBox.Show("Вы не выбрали дату окончания мероприятия!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
            }
            if (!DateTime.TryParse(datePicker2.Text, out DateTime aa))
            {
                    MessageBox.Show("Вы неправильно выбрали или написали дату окончания мероприятия! Пример: 12.12.2024", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker2.SelectedDate = null;
                    return false;
            }
            if (datePicker2.SelectedDate > maxdate)
            {
                MessageBox.Show($"Дата окончания мероприятия не может быть выставлена позже {maxdate:dd.MM.yyyy}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                datePicker2.SelectedDate = null;
                return false;
            }
            if (datePicker1.SelectedDate > datePicker2.SelectedDate)
            {
                    MessageBox.Show("Дата начала мероприятия не может быть больше даты окончания мероприятия!", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    datePicker1.SelectedDate = null;
                    datePicker2.SelectedDate = null;
                    return false;
            }
            if(TextBox_Place.Text == "")
            {
                MessageBox.Show("Вы не написали место проведения!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
