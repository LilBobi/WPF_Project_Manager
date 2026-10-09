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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Menedger
{
    /// <summary>
    /// Логика взаимодействия для MainAdministrator.xaml
    /// </summary>
    public partial class MainAdministrator : Window
    {
        public MainAdministrator()
        {
            InitializeComponent();
            Label_NameUser.Content = $"{Information.Surname} {Information.Name} {Information.Patronymic}";
            if (Information.idRole == 2)
            {
                Button_User.Visibility = Visibility.Hidden;
                VisibleUsers.Height = new GridLength(0);
                this.Width = 800;
                this.Height = 630;
            }
            if (Information.idRole == 3)
            {
                this.Width = 800;
                this.Height = 630;
            }
        }

        private void Button_Project_Click(object sender, RoutedEventArgs e)
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

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите выйти из системы?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                    Information.idRole = null;
                    Information.idUser = null;
                    Information.Name = null;
                    Information.Surname = null;
                    Information.Patronymic = null;
                }
                else
                {
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

        private void Button_InfoResource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InformationResource informationResource = new InformationResource();
                informationResource.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_User_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InformationUsers informationUsers = new InformationUsers();
                informationUsers.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            InformationDocuments informationDocuments = new InformationDocuments();
            informationDocuments.Show();
            this.Close();
        }

        private void Button_NewsAndEvents_Click(object sender, RoutedEventArgs e)
        {
            EventsAndNews eventsAndNews = new EventsAndNews();
            eventsAndNews.Show();
            this.Close();
        }
    }
}
