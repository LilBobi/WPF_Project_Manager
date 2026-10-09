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
    /// Логика взаимодействия для InformationUsers.xaml
    /// </summary>
    public partial class InformationUsers : Window
    {
        is1_25_kokorinds_Kursovoy_ProjectEntities1 db = new is1_25_kokorinds_Kursovoy_ProjectEntities1 ();
        public InformationUsers()
        {
            InitializeComponent();
            ComboBox_Post.ItemsSource = App.db.Post.ToList();
            ComboBox_Role.ItemsSource = App.db.Rolee.ToList();
            UpdateDG();
        }

        List<Userr> curUser(List<Userr> userS)
        {
            return userS.Where(x => x.id_User != Information.idUser).ToList();
        }

        void UpdateDG()
        {
            List<Userr> userList = curUser(App.db.Userr.ToList());
            if(ComboBox_Post.SelectedIndex != -1)
            {
                string categoryPost = ComboBox_Post.SelectedValue.ToString();
                List<Userr> sortList1 = new List<Userr>();
                sortList1.Clear();
                foreach(Userr userr in curUser(userList))
                {
                    if (categoryPost.Equals(userr.Post.Namee))
                        sortList1.Add(userr);
                }
                userList = sortList1;
            }
            if(ComboBox_Role.SelectedIndex != -1)
            {
                string categoryRole = ComboBox_Role.SelectedValue.ToString();
                List<Userr> sortList2 = new List<Userr>();
                sortList2.Clear();
                foreach(Userr userr in curUser(userList))
                {
                    if(categoryRole.Equals(userr.Rolee.Namee))
                        sortList2.Add(userr);
                }
                userList = sortList2;
            }
            if (TextBox_Search.Text.Length > 0)
            {
                List<Userr> sortList3 = new List<Userr>();
                sortList3.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                //var filteredProducts = db.Payments.Where(p => p.id_User == Information.idUser && (p.Payment_Name.ToLower().Contains(searchText) ||
                //p.Price.ToString().ToLower().Contains(searchText) || p.Datee.ToString().ToLower().Contains(searchText))).ToList();
                foreach (Userr userr in curUser(userList))
                {
                    if (userr.id_User == userr.id_User)
                    {
                        sortList3 = userList.Where(p => p.Surname.ToLower().Contains(searchText) ||
                        p.Namee.ToString().ToLower().Contains(searchText) || p.Patronymic.ToString().ToLower().Contains(searchText)
                        || p.Email.ToString().ToLower().Contains(searchText) || p.Number_Phone.ToString().ToLower().Contains(searchText)).ToList();
                    }
                }
                userList = sortList3;
            }
            Table.ItemsSource = userList;
        }

        private void Button_RedactRoleAndDoljnost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    ComboBox_Post.ItemsSource = null;
                    ComboBox_Role.ItemsSource = null;
                    TextBox_Search.Text = "";
                    ComboBox_Post.ItemsSource = App.db.Post.ToList();
                    ComboBox_Role.ItemsSource = App.db.Rolee.ToList();

                    Userr userR = (Userr)Table.SelectedItem;
                    RedactRoleAndDoljnost redact = new RedactRoleAndDoljnost(userR);
                    redact.ShowDialog();
                    UpdateDG();
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

        private void Button_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Table.SelectedItem != null)
                {
                    if (Table.SelectedIndex == -1)
                    {
                        return;
                    }
                    if (MessageBox.Show("Вы уверены что хотите удалить пользователя? Пользователь будет удален из команды проекта, также все задачи назначенные ему также будут удалены!",
                        "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        ComboBox_Post.ItemsSource = null;
                        ComboBox_Role.ItemsSource = null;
                        TextBox_Search.Text = "";
                        ComboBox_Post.ItemsSource = App.db.Post.ToList();
                        ComboBox_Role.ItemsSource = App.db.Rolee.ToList();

                        var selectedUser = Table.SelectedItem as Userr;
                        if (selectedUser != null)
                        {
                            var useRs = App.db.Userr.FirstOrDefault(x => x.id_User == selectedUser.id_User);

                            var projectsToDelete = App.db.Project_Team.Where(x => x.id_User == useRs.id_User).ToList();

                            if (projectsToDelete.Any())
                            {
                                // Находим все связанные задачи
                                var taskIds = projectsToDelete.Select(p => p.id_User).ToList();
                                var tasksToDelete = App.db.Project_Team.Where(x => taskIds.Contains(x.id_User)).ToList();

                                // Удаляем задачи
                                if (tasksToDelete.Any())
                                {
                                    App.db.Project_Team.RemoveRange(tasksToDelete);
                                }
                            }

                            var TasksDelete = App.db.Tasks.Where(x => x.id_User == useRs.id_User).ToList();

                            // Удаляем проектные команды
                            if (TasksDelete.Any())
                            {
                                // Находим все связанные задачи
                                var taskIds = TasksDelete.Select(p => p.id_User).ToList();
                                var tasksToDelete = App.db.Tasks.Where(x => taskIds.Contains(x.id_User)).ToList();

                                if (tasksToDelete.Any())
                                    App.db.Tasks.RemoveRange(tasksToDelete);
                            }
                        }

                        var select = Table.SelectedItem;
                        Userr user = (Userr)select;
                        if (user != null)
                        {
                            App.db.Userr.Remove(user);
                        }
                        App.db.SaveChanges();
                        UpdateDG();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox_Post.ItemsSource = null;
                TextBox_Search.Text = "";
                ComboBox_Role.ItemsSource = null;
                ComboBox_Role.ItemsSource = App.db.Rolee.ToList();
                ComboBox_Post.ItemsSource = App.db.Post.ToList();
                UpdateDG();
                MessageBox.Show("Фильтрация успешно сброшена!", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            try
            {
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

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_Search.Text.Length > 1)
            {
                UpdateDG();
            }
            else
            {
                UpdateDG();
            }
        }

        private void ComboBox_Post_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDG();
        }

        private void ComboBox_Role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDG();
        }

        private void Button_AddUsers_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
            UpdateDG();
            return;
        }
    }
}
