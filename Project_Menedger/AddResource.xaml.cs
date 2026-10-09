using Project_Menedger.Models;
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
    /// Логика взаимодействия для AddResource.xaml
    /// </summary>
    public partial class AddResource : Window
    {
        public AddResource()
        {
            InitializeComponent();
            ComboBox_TypeResource.ItemsSource = App.db.Resource_Type.ToList();
        }

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

        private void Button_AddResource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Check())
                {
                    return;
                }
                Resourcee resourcee = new Resourcee();
                resourcee.id_Resource_Type = (int)ComboBox_TypeResource.SelectedValue;
                resourcee.Namee = TextBox_ResourceName.Text;
                resourcee.Descriptionn = TextBox_Description.Text;
                resourcee.Availabilityy = TextBox_Availability.Text;
                App.db.Resourcee.Add(resourcee);
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
            if (ComboBox_TypeResource.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали тип ресурса!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_ResourceName.Text == "")
            {
                MessageBox.Show("Вы не написали название ресурса", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TextBox_Availability.Text == "")
            {
                MessageBox.Show("Вы не написали доступность ресурса! Пример: Доступен или в использовании.", "Внимание", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
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
