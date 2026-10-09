using Project_Menedger.Models;
using Project_Menedger.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    /// Логика взаимодействия для InformationResource.xaml
    /// </summary>
    public partial class InformationResource : Window
    {
        is1_25_kokorinds_Kursovoy_ProjectEntities1 db = new is1_25_kokorinds_Kursovoy_ProjectEntities1();
        public InformationResource()
        {
            InitializeComponent();
            if(InfoTypeResource.Visibility == Visibility.Hidden & InfoTypeResourcee.Visibility 
                == Visibility.Hidden & InfoMenuTypeResource.Visibility == Visibility.Hidden
                & TableTypeResource.Visibility == Visibility.Hidden)
            {
                ComboBox_Category.ItemsSource = App.db.Resource_Type.ToList();
                UpdateResourceManagerAndAdministrator();
            }
            else if (InfoResource.Visibility == Visibility.Hidden & InfoResourcee.Visibility
                == Visibility.Hidden & InfoMenuResource.Visibility == Visibility.Hidden
                & Table.Visibility == Visibility.Hidden)
            {
                UpdateTypeResourceManagerAndAdministrator();
            }
        }
        List<Resourcee> curResource(List<Resourcee> resourCe)
        {
            return resourCe.Where(x => x.id_Resource == x.id_Resource).ToList();
        }

        void UpdateResourceManagerAndAdministrator()
        {
            List<Resourcee> curResourcee = curResource(App.db.Resourcee.ToList());
            if (ComboBox_Category.SelectedIndex != -1)
            {
                string category = ComboBox_Category.SelectedValue.ToString();
                List<Resourcee> sortComboBox = new List<Resourcee>();
                sortComboBox.Clear();
                foreach (Resourcee resource in curResource(curResourcee))
                {
                    if (category.Equals(resource.Resource_Type.Namee))
                        sortComboBox.Add(resource);
                }
                curResourcee = sortComboBox;
            }
            if (TextBox_Search.Text.Length > 0)
            {
                List<Resourcee> sortTextBoxSearch = new List<Resourcee>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                foreach (Resourcee resource in curResource(curResourcee))
                {
                    if (resource.id_Resource == resource.id_Resource)
                    {
                        sortTextBoxSearch = curResourcee.Where(p => p.Resource_Type.Namee.ToString().ToLower().Contains(searchText) ||
                        p.Namee.ToString().ToLower().Contains(searchText) || p.Descriptionn.ToString().ToLower().Contains(searchText) ||
                        p.Availabilityy.ToString().Contains(searchText)).ToList();
                    }
                }
                curResourcee = sortTextBoxSearch;
            }
            Table.ItemsSource = curResourcee;
        }
        List<Resource_Type> curTypeResource(List<Resource_Type> typeResource)
        {
            return typeResource.Where(x => x.id_Resource_Type == x.id_Resource_Type).ToList();
        }

        void UpdateTypeResourceManagerAndAdministrator()
        {
            List<Resource_Type> curTypeResourcee = curTypeResource(App.db.Resource_Type.ToList());
            if (TextBox_SearchTypeResource.Text.Length > 0)
            {
                List<Resource_Type> sortTextBoxSearchType = new List<Resource_Type>();
                sortTextBoxSearchType.Clear();
                var searchText = TextBox_SearchTypeResource.Text.ToLower();
                foreach (Resource_Type resourceType in curTypeResource(curTypeResourcee))
                {
                    if (resourceType.id_Resource_Type == resourceType.id_Resource_Type)
                    {
                        sortTextBoxSearchType = db.Resource_Type.Where(p => p.Namee.ToString().ToLower().Contains(searchText)).ToList();
                    }
                }
                curTypeResourcee = sortTextBoxSearchType;
            }
            TableTypeResource.ItemsSource = curTypeResourcee;
        }

        // Методы использования окна информации о ресурсах
        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            MainAdministrator mainAdministrator = new MainAdministrator();
            mainAdministrator.Show();
            this.Close();
        }

        private void Button_TypeResource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Resource_Type.ToList();
                label_Text.Content = "Типы ресурсов";
                InfoResource.Visibility = Visibility.Hidden;
                InfoResourcee.Visibility = Visibility.Hidden;
                InfoTypeResource.Visibility = Visibility.Visible;
                InfoTypeResourcee.Visibility = Visibility.Visible;
                InfoMenuResource.Visibility = Visibility.Hidden;
                InfoMenuTypeResource.Visibility = Visibility.Visible;
                Table.Visibility = Visibility.Hidden;
                TableTypeResource.Visibility = Visibility.Visible;
                UpdateTypeResourceManagerAndAdministrator();
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
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Resource_Type.ToList();

                AddResource addResource = new AddResource();
                addResource.ShowDialog();
                UpdateResourceManagerAndAdministrator();
                Table.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_RedactResource_Click(object sender, RoutedEventArgs e)
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
                    ComboBox_Category.ItemsSource = App.db.Resource_Type.ToList();

                    Resourcee resourcee = (Resourcee)Table.SelectedItem;
                    RedactResource redact = new RedactResource(resourcee);
                    redact.ShowDialog();
                    UpdateResourceManagerAndAdministrator();
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

        private void Button_DeleteResource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    if (MessageBox.Show("Вы уверены что хотите удалить ресурс? Все проекты к которым привязан данный ресурс будут удалены а с ними их задачи и команды!", "Внимание", MessageBoxButton.OKCancel,
                        MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        ComboBox_Category.ItemsSource = null;
                        TextBox_Search.Text = "";
                        ComboBox_Category.ItemsSource = App.db.Resource_Type.ToList();

                        var selectedResource = Table.SelectedItem as Resourcee;
                        if (selectedResource != null)
                        {
                            var resources = App.db.Resourcee.FirstOrDefault(x => x.id_Resource == selectedResource.id_Resource);

                            var projectsToDelete = App.db.Production_Project.Where(x => x.id_Resource == resources.id_Resource).ToList();

                            if (projectsToDelete.Any())
                            {
                                // Находим все связанные задачи
                                var taskIds = projectsToDelete.Select(p => p.id_Project).ToList();
                                var tasksToDelete = App.db.Tasks.Where(x => taskIds.Contains(x.id_Project)).ToList();

                                // Удаляем задачи
                                if (tasksToDelete.Any())
                                {
                                    App.db.Tasks.RemoveRange(tasksToDelete);
                                }

                                // Находим все связанные проектные команды
                                var projectTeamsToDelete = App.db.Project_Team.Where(x => taskIds.Contains((int)x.id_Project)).ToList();

                                // Удаляем проектные команды
                                if (projectTeamsToDelete.Any())
                                {
                                    App.db.Project_Team.RemoveRange(projectTeamsToDelete);
                                }

                                // Удаляем проекты
                                App.db.Production_Project.RemoveRange(projectsToDelete);

                            }
                        }

                        var select = Table.SelectedItem;
                        Resourcee resourcee = (Resourcee)select;
                        if (resourcee != null)
                        {
                            App.db.Resourcee.Remove(resourcee);
                        }
                        App.db.SaveChanges();
                        UpdateResourceManagerAndAdministrator();
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
                    MessageBox.Show("Вы не выбрали строчку для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateResourceManagerAndAdministrator();
        }

        private void Button_ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox_Category.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Category.ItemsSource = App.db.Resource_Type.ToList();
                UpdateResourceManagerAndAdministrator();
                MessageBox.Show("Фильтрация успешно сброшена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            UpdateResourceManagerAndAdministrator();
        }

        // Методы использования окна информации о типе ресурса
        private void TextBox_SearchTypeResource_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTypeResourceManagerAndAdministrator();
        }

        private void Button_Resource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox_SearchTypeResource.Text = "";
                label_Text.Content = "Ресурсы";
                InfoResource.Visibility = Visibility.Visible;
                InfoResourcee.Visibility = Visibility.Visible;
                InfoTypeResource.Visibility = Visibility.Hidden;
                InfoTypeResourcee.Visibility = Visibility.Hidden;
                InfoMenuResource.Visibility = Visibility.Visible;
                InfoMenuTypeResource.Visibility = Visibility.Hidden;
                Table.Visibility = Visibility.Visible;
                TableTypeResource.Visibility = Visibility.Hidden;
                ComboBox_Category.ItemsSource = App.db.Resource_Type.ToList();
                UpdateResourceManagerAndAdministrator();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_AddTypeResource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox_SearchTypeResource.Text = "";

                AddTypeResource addTypeResource = new AddTypeResource();
                addTypeResource.ShowDialog();
                UpdateTypeResourceManagerAndAdministrator();
                Table.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_RedactTypeResource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TableTypeResource.SelectedItem != null)
                {
                    if (TableTypeResource.SelectedIndex == -1)
                    {
                        return;
                    }
                    TextBox_SearchTypeResource.Text = "";

                    Resource_Type resource_Type = (Resource_Type)TableTypeResource.SelectedItem;
                    RedactTypeResource redact = new RedactTypeResource(resource_Type);
                    redact.ShowDialog();
                    UpdateTypeResourceManagerAndAdministrator();
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

        private void Button_DeleteTypeResource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TableTypeResource.SelectedItem != null)
                {
                    if (TableTypeResource.SelectedIndex == -1)
                    {
                        return;
                    }
                    if (MessageBox.Show("Вы уверены что хотите удалить тип ресурса? Ресурсы связанные с данным типом будут удалены, также проекты, задачи и команды привязанные к ресурсу также будут удалены!",
                        "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        TextBox_SearchTypeResource.Text = "";

                        var selectedResource = TableTypeResource.SelectedItem as Resource_Type;
                        if (selectedResource != null)
                        {
                            var res = App.db.Resource_Type.FirstOrDefault(x => x.id_Resource_Type == selectedResource.id_Resource_Type);

                            var del = App.db.Resourcee.Where(x => x.id_Resource_Type == res.id_Resource_Type);

                            if(del.Any())
                            {
                                var taskIds = del.Select(p => p.id_Resource).ToList();
                                var tasksToDelete = App.db.Resourcee.Where(x => taskIds.Contains(x.id_Resource)).ToList();

                                // Удаляем задачи
                                if (tasksToDelete.Any())
                                {
                                    App.db.Resourcee.RemoveRange(tasksToDelete);
                                }
                            }

                            var resources = App.db.Resourcee.FirstOrDefault(x => x.id_Resource == selectedResource.id_Resource_Type);

                            var projectsToDelete = App.db.Production_Project.Where(x => x.id_Resource == resources.id_Resource).ToList();

                            if (projectsToDelete.Any())
                            {
                                // Находим все связанные задачи
                                var taskIds = projectsToDelete.Select(p => p.id_Project).ToList();
                                var tasksToDelete = App.db.Tasks.Where(x => taskIds.Contains(x.id_Project)).ToList();

                                // Удаляем задачи
                                if (tasksToDelete.Any())
                                {
                                    App.db.Tasks.RemoveRange(tasksToDelete);
                                }

                                // Находим все связанные проектные команды
                                var projectTeamsToDelete = App.db.Project_Team.Where(x => taskIds.Contains((int)x.id_Project)).ToList();

                                // Удаляем проектные команды
                                if (projectTeamsToDelete.Any())
                                {
                                    App.db.Project_Team.RemoveRange(projectTeamsToDelete);
                                }

                                // Удаляем проекты
                                App.db.Production_Project.RemoveRange(projectsToDelete);

                            }
                        }

                        var select = TableTypeResource.SelectedItem;
                        Resource_Type resource_Type = (Resource_Type)select;
                        if (resource_Type != null)
                        {
                            App.db.Resource_Type.Remove(resource_Type);
                        }
                        App.db.SaveChanges();
                        UpdateTypeResourceManagerAndAdministrator();
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
                    MessageBox.Show("Вы не выбрали строчку для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
