using Project_Menedger.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
    /// Логика взаимодействия для InformationDocuments.xaml
    /// </summary>
    public partial class InformationDocuments : Window
    {
        public InformationDocuments()
        {
            InitializeComponent();
            ComboBox_Status.ItemsSource = App.db.Statuss.ToList();
            RefreshListviewDocument();
        }

        void RefreshListviewDocument()
        {
            List<Models.Documentation> documentt = App.db.Documentation.ToList();
            if (TextBox_Search.Text.Length > 0)
            {
                List<Models.Documentation> sortTextBoxSearch = new List<Models.Documentation>();
                sortTextBoxSearch.Clear();
                var searchText = TextBox_Search.Text.ToLower();
                foreach (Models.Documentation Docc in documentt)
                {
                    sortTextBoxSearch = documentt.Where(p => p.Production_Project.Namee.ToString().ToLower().Contains(searchText) ||
                    p.Progress.ToString().ToLower().Contains(searchText) || p.Cost.ToString().ToLower().Contains(searchText) ||
                    p.Profit.ToString().ToLower().Contains(searchText)).ToList();
                }
                documentt = sortTextBoxSearch;
            }
            if (ComboBox_Status.SelectedIndex != -1)
            {
                string category = ComboBox_Status.SelectedValue.ToString();
                List<Models.Documentation> sortComboBox = new List<Models.Documentation>();
                sortComboBox.Clear();
                foreach (Models.Documentation Docc in documentt)
                {
                    if (category.Equals(Docc.Statuss.Namee))
                        sortComboBox.Add(Docc);
                }
                documentt = sortComboBox;
            }
            ListView_Documents.ItemsSource = documentt;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListviewDocument();
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListviewDocument();
        }

        private void Button_AddDocuments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InformationAddRedact.AddRedactDocuments = 0;
                AddRedactDocuments document = new AddRedactDocuments();
                document.ShowDialog();
                RefreshListviewDocument();
                ListView_Documents.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Button_RedactDocuments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListView_Documents.SelectedItem != null)
                {
                    if (ListView_Documents.SelectedIndex == -1)
                    {
                        return;
                    }
                    Models.Documentation RedactDocument = (Models.Documentation)ListView_Documents.SelectedItem;
                    InformationAddRedact.AddRedactDocuments = 1;
                    AddRedactDocuments redact = new AddRedactDocuments(RedactDocument);
                    redact.ShowDialog();
                    RefreshListviewDocument();
                    ListView_Documents.SelectedIndex = -1;
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

        private void Button_DeleteDocuments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListView_Documents.SelectedItem != null)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
                        == MessageBoxResult.Yes)
                    {
                        Models.Documentation deleteDocuments = (Models.Documentation)ListView_Documents.SelectedItem;
                        App.db.Documentation.Remove(deleteDocuments);
                        App.db.SaveChanges();
                        RefreshListviewDocument();
                        ListView_Documents.SelectedIndex = -1;
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
            ComboBox_Status.ItemsSource = null;
            TextBox_Search.Text = "";
            ComboBox_Status.ItemsSource = App.db.Statuss.ToList();
            RefreshListviewDocument();
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
    }
}
