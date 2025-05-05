using DB_Editor;
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

namespace Semestralka_BSCSH
{
    /// <summary>
    /// Логика взаимодействия для AddAuthorWindow.xaml
    /// </summary>
    public partial class AddAuthorWindow : Window
    {
        public AddAuthorWindow()
        {
            InitializeComponent();
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string bio = BioTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(bio))
            {
                MessageBox.Show("Please fill in both fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                DataHelper.AddAuthor(name, bio);
                MessageBox.Show("Author added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
