using Project_Menedger.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// Логика взаимодействия для EventsAndNews.xaml
    /// </summary>
    public partial class EventsAndNews : Window
    {
        public EventsAndNews()
        {
            InitializeComponent();
            RefreshListView();
            if(Information.idRole == 1)
            {
                InfoMenuNews.Visibility = Visibility.Hidden;
                InfoMenuEvents.Visibility = Visibility.Hidden;
                InfoMenuEventsEmployee.Visibility = Visibility.Visible;
                InfoNews.Visibility = Visibility.Hidden;
                InfoEvent.Visibility = Visibility.Visible;
                InfoNewss.Visibility = Visibility.Hidden;
                InfoEvents.Visibility = Visibility.Visible;
                NewsVisibility.Visibility = Visibility.Hidden;
                EventsVisibility.Visibility = Visibility.Visible;
            }
            if(Information.idRole >= 2)
            {
                InfoMenuNews.Visibility = Visibility.Hidden;
                InfoMenuEventsEmployee.Visibility = Visibility.Hidden;
                InfoMenuEvents.Visibility = Visibility.Visible;
                InfoNews.Visibility = Visibility.Hidden;
                InfoEvent.Visibility = Visibility.Visible;
                InfoNewss.Visibility = Visibility.Hidden;
                InfoEvents.Visibility = Visibility.Visible;
                NewsVisibility.Visibility = Visibility.Hidden;
                EventsVisibility.Visibility = Visibility.Visible;
            }
        }

        void RefreshListView()
        {
            DateTime monthAgo = DateTime.Now.Date;
            List<Eventt> eventts = App.db.Eventt.Where(x=> x.End_Date >= monthAgo).ToList();
            if (TextBox_Search.Text.Length > 0)
            {
                List<Eventt> sortTextBoxSearch = new List<Eventt>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                foreach (Eventt Event in eventts)
                {
                    sortTextBoxSearch = eventts.Where(p => p.Namee.ToString().ToLower().Contains(searchText) ||
                    p.Descriptionn.ToString().ToLower().Contains(searchText) || p.Place.ToString().ToLower().Contains(searchText) ||
                    p.Userr.Surname.ToLower().Contains(searchText) || p.Userr.Namee.ToLower().Contains(searchText) ||
                    p.Userr.Patronymic.ToLower().Contains(searchText)).ToList();
                }
                eventts = sortTextBoxSearch;
            }
            if(DatePicker_Time.SelectedDate != null)
            {
                List<Eventt> sortDate = new List<Eventt>();
                sortDate.Clear();
                foreach (Eventt Time in eventts)
                {
                    if (DateTime.Parse(DatePicker_Time.ToString()) == Time.Start_Datee)
                        sortDate.Add(Time);
                }
                eventts = sortDate;
            }
            ListView_Events.ItemsSource = eventts;
        }

        void RefreshListviewNews()
        {
            DateTime monthAgo = DateTime.Now.AddMonths(-1).Date;
            List<News> news = App.db.News.Where(x => x.Publication_Date >= monthAgo).ToList();
            if (TextBox_SearchNews.Text.Length > 0)
            {
                List<News> sortTextBoxSearch = new List<News>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_SearchNews.Text.ToLower();
                foreach (News Newss in news)
                {
                    sortTextBoxSearch = news.Where(p => p.Heading.ToString().ToLower().Contains(searchText) ||
                    p.Descriptionn.ToString().ToLower().Contains(searchText) || p.Userr.Surname.ToLower().Contains(searchText)
                    || p.Userr.Namee.ToLower().Contains(searchText) || p.Userr.Patronymic.ToLower().Contains(searchText)).ToList();
                }
                news = sortTextBoxSearch;
            }
            if (DatePicker_TimesNews.SelectedDate != null)
            {
                List<News> sortDate = new List<News>();
                sortDate.Clear();
                foreach (News Newss in news)
                {
                    if (DateTime.Parse(DatePicker_TimesNews.ToString()) == Newss.Publication_Date)
                        sortDate.Add(Newss);
                }
                news = sortDate;
            }
            ListView_News.ItemsSource = news;
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_Search.Text.Length > 0)
            {
                RefreshListView();
            }
            else
            {
                RefreshListView();
            }
        }

        private void DatePicker_Time_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DatePicker_Time.Text == "Выбор даты")
                {
                    DatePicker_Time.SelectedDate = null;
                    return;
                }
                if (DatePicker_Time.Text == null)
                {
                    DatePicker_Time.SelectedDate = null;
                    return;
                }
                if (DatePicker_Time.Text == "")
                {
                    DatePicker_Time.SelectedDate = null;
                    return;
                }
                RefreshListView();
                return;
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

        private void Button_RollUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_AddEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InformationAddRedact.AddRedactEvents = 0;
                AddRedactEvent eventtt = new AddRedactEvent();
                eventtt.ShowDialog();
                RefreshListView();
                ListView_Events.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_RedactEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListView_Events.SelectedItem != null)
                {
                    if (ListView_Events.SelectedIndex == -1)
                    {
                        return;
                    }
                    Eventt RedactEvent = (Eventt)ListView_Events.SelectedItem;
                    InformationAddRedact.AddRedactEvents = 1;
                    AddRedactEvent redact = new AddRedactEvent(RedactEvent);
                    redact.ShowDialog();
                    RefreshListView();
                    ListView_Events.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись для редактирования!", "Внимание",
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

        private void Button_DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ListView_Events.SelectedItem != null)
                {
                    if(MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
                        == MessageBoxResult.Yes)
                    {
                        Eventt RedactEvent = (Eventt)ListView_Events.SelectedItem;
                        App.db.Eventt.Remove(RedactEvent);
                        App.db.SaveChanges();
                        RefreshListView();
                        ListView_Events.SelectedIndex = -1;
                        MessageBox.Show("Запись успешно удалена!", "Успешно",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись для удаления!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка при удалении! Запись уже используется.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void FiltersCleart_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Search.Text = string.Empty;
            DatePicker_Time.Text = string.Empty;
            RefreshListView();
            MessageBox.Show("Фильтрация успешно сброшена!", "Успешно",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
            return;
        }

        private void Button_NewsSwaping_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Search.Text = string.Empty;
            DatePicker_Time.Text = string.Empty;
            RefreshListviewNews();
            label_Text.Content = "Новости";
            InfoMenuEvents.Visibility = Visibility.Hidden;
            InfoMenuNews.Visibility = Visibility.Visible;
            InfoEvent.Visibility = Visibility.Hidden;
            InfoNews.Visibility = Visibility.Visible;
            InfoEvents.Visibility = Visibility.Hidden;
            InfoNewss.Visibility = Visibility.Visible;
            EventsVisibility.Visibility = Visibility.Hidden;
            NewsVisibility.Visibility = Visibility.Visible;
        }

        private void TextBox_SearchNews_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TextBox_SearchNews.Text.Length > 0)
            {
                RefreshListviewNews();
            }
            else
            {
                RefreshListviewNews();
            }
        }

        private void DatePicker_TimesNews_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DatePicker_TimesNews.Text == "Выбор даты")
                {
                    DatePicker_TimesNews.SelectedDate = null;
                }
                if (DatePicker_TimesNews.Text == null)
                {
                    DatePicker_TimesNews.SelectedDate = null;
                }
                if (DatePicker_TimesNews.Text == "")
                {
                    DatePicker_TimesNews.SelectedDate = null;
                }
                RefreshListviewNews();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_EventSwaping_Click(object sender, RoutedEventArgs e)
        {
            TextBox_SearchNews.Text = string.Empty;
            DatePicker_TimesNews.Text = string.Empty;
            RefreshListView();
            label_Text.Content = "Мероприятия";
            InfoMenuNews.Visibility = Visibility.Hidden;
            InfoMenuEvents.Visibility = Visibility.Visible;
            InfoNews.Visibility = Visibility.Hidden;
            InfoEvent.Visibility = Visibility.Visible;
            InfoNewss.Visibility = Visibility.Hidden;
            InfoEvents.Visibility = Visibility.Visible;
            NewsVisibility.Visibility = Visibility.Hidden;
            EventsVisibility.Visibility = Visibility.Visible;
        }

        private void Button_AddNews_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InformationAddRedact.AddRedactNews = 0;
                AddRedactNews newws = new AddRedactNews();
                newws.ShowDialog();
                RefreshListviewNews();
                ListView_News.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_RedactNews_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListView_News.SelectedItem != null)
                {
                    if (ListView_News.SelectedIndex == -1)
                    {
                        return;
                    }
                    News RedactNews = (News)ListView_News.SelectedItem;
                    InformationAddRedact.AddRedactNews = 1;
                    AddRedactNews redact = new AddRedactNews(RedactNews);
                    redact.ShowDialog();
                    RefreshListviewNews();
                    ListView_News.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись для редактирования!", "Внимание",
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

        private void Button_DeleteNews_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListView_News.SelectedItem != null)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
                        == MessageBoxResult.Yes)
                    {
                        News RedactNews = (News)ListView_News.SelectedItem;
                        App.db.News.Remove(RedactNews);
                        App.db.SaveChanges();
                        RefreshListviewNews();
                        ListView_News.SelectedIndex = -1;
                        MessageBox.Show("Запись успешно удалена!", "Успешно",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись для удаления!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка при удалении! Запись уже используется.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            TextBox_SearchNews.Text = string.Empty;
            DatePicker_TimesNews.Text = string.Empty;
            RefreshListviewNews();
            MessageBox.Show("Фильтрация успешно сброшена!", "Успешно",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
            return;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Information.idRole == 1)
                {
                    InformationProject informationProject = new InformationProject();
                    informationProject.Show();
                    this.Close();
                    return;
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

        private void Button_NewsSwapingEmployee_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Search.Text = string.Empty;
            DatePicker_Time.Text = string.Empty;
            RefreshListviewNews();
            label_Text.Content = "Новости";
            InfoMenuEventsEmployee.Visibility = Visibility.Hidden;
            InfoMenuNewsEmployee.Visibility = Visibility.Visible;
            InfoEvent.Visibility = Visibility.Hidden;
            InfoNews.Visibility = Visibility.Visible;
            InfoEvents.Visibility = Visibility.Hidden;
            InfoNewss.Visibility = Visibility.Visible;
            EventsVisibility.Visibility = Visibility.Hidden;
            NewsVisibility.Visibility = Visibility.Visible;
        }

        private void Button_EventSwapingEmployee_Click(object sender, RoutedEventArgs e)
        {
            TextBox_SearchNews.Text = string.Empty;
            DatePicker_TimesNews.Text = string.Empty;
            RefreshListView();
            label_Text.Content = "Мероприятия";
            InfoMenuNewsEmployee.Visibility = Visibility.Hidden;
            InfoMenuEventsEmployee.Visibility = Visibility.Visible;
            InfoNews.Visibility = Visibility.Hidden;
            InfoEvent.Visibility = Visibility.Visible;
            InfoNewss.Visibility = Visibility.Hidden;
            InfoEvents.Visibility = Visibility.Visible;
            NewsVisibility.Visibility = Visibility.Hidden;
            EventsVisibility.Visibility = Visibility.Visible;
        }
    }
}
