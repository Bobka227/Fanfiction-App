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
    /// Логика взаимодействия для AddChapterWindow.xaml
    /// </summary>
    public partial class AddChapterWindow : Window
    {
        private int fanficId;

        public AddChapterWindow(int fanficId)
        {
            InitializeComponent();
            this.fanficId = fanficId;
        }

        private void AddChapter_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ChapterNumberTextBox.Text, out int chapterNumber))
            {
                MessageBox.Show("Please enter a valid chapter number.");
                return;
            }

            string title = TitleTextBox.Text.Trim();
            string filePath = FilePathTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Please fill in both title and file path.");
                return;
            }

            try
            {
                FanficReaderDatabase.AddChapter(fanficId, chapterNumber, title, filePath);
                MessageBox.Show("Chapter added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while adding chapter: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
