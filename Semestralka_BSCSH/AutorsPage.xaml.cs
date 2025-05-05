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
using DataModels;
using DB_Editor;
namespace Semestralka_BSCSH
{
    /// <summary>
    /// Логика взаимодействия для AutorsPage.xaml
    /// </summary>
    public partial class AutorsPage : Window
    {
        private List<AutorsModel> allAuthors = new List<AutorsModel>(); 

        public AutorsPage()
        {
            InitializeComponent();
            allAuthors = DataHelper.GetAuthors();
            AuthorsList.ItemsSource = allAuthors;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();

            var filteredAuthors = allAuthors
                .Where(a => a.Name.ToLower().Contains(searchText)) 
                .ToList();

            AuthorsList.ItemsSource = filteredAuthors;
        }

        private void AuthorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorsList.SelectedItem is AutorsModel selectedAuthor)
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.LoadFanficsByAuthor(selectedAuthor.Id);
                }

                this.Close();
            }
        }

        private void OpenAddAuthorWindow(object sender, RoutedEventArgs e)
        {
            var window = new AddAuthorWindow();
            window.ShowDialog();
            RefreshAuthors(); 
        }

        private void RefreshAuthors()
        {
            allAuthors = DataHelper.GetAuthors();
            AuthorsList.ItemsSource = allAuthors;
        }


        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is AutorsModel author)
            {
                var result = MessageBox.Show($"Are you sure you want to delete author \"{author.Name}\"?",
                                             "Confirm Deletion",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataHelper.DeleteAuthor(author.Id);
                        RefreshAuthors();
                        MessageBox.Show("Author deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting author: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


    }

}

