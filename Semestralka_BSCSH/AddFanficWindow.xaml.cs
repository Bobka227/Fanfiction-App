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
using Microsoft.Win32;

namespace Semestralka_BSCSH
{
   
    public partial class AddFanficWindow : Window
    {
        private List<string> chapterFiles = new List<string>();

        public AddFanficWindow()
        {
            InitializeComponent();
            LoadAuthors();
            LoadCategories();
            LoadGenres();
            LoadTags(); 

        }
        private void LoadAuthors()
        {
            var authors = DataHelper.GetAuthors();
            AuthorComboBox.ItemsSource = authors;
            AuthorComboBox.DisplayMemberPath = "Name";
            AuthorComboBox.SelectedValuePath = "Id"; 
        }

        private void LoadCategories()
        {
            var categories = FanficReaderDatabase.GetCategories();
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "Id";
        }

        private void LoadGenres()
        {
            var genres = FanficReaderDatabase.GetGenre();
            GenreComboBox.ItemsSource = genres;
            GenreComboBox.DisplayMemberPath = "Name";
            GenreComboBox.SelectedValuePath = "Id";
        }
        private void LoadTags()
        {
            var tags = FanficReaderDatabase.GetTags();
            TagsListBox.ItemsSource = tags;
            TagsListBox.DisplayMemberPath = "Name";
            TagsListBox.SelectedValuePath = "Id";

        }


        private void SaveFanfic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                    AuthorComboBox.SelectedItem == null ||
                    CategoryComboBox.SelectedItem == null ||
                    GenreComboBox.SelectedItem == null ||
                    TagsListBox.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please fill in all fields!");
                    return;
                }

                string title = TitleTextBox.Text;
                int authorId = ((AutorsModel)AuthorComboBox.SelectedItem).Id;
                int categoryId = (int)CategoryComboBox.SelectedValue;
                int genreId = ((GenreModel)GenreComboBox.SelectedItem).Id;

                List<int> tagIds = new List<int>();
                foreach (var item in TagsListBox.SelectedItems)
                {
                    if (item is TagModel tag)
                    {
                        tagIds.Add(tag.Id);
                    }
                    else
                    {
                        MessageBox.Show("Error: Invalid tag format!");
                        return;
                    }
                }


                int fanficId = FanficReaderDatabase.AddFanfic(title, authorId, categoryId, genreId, tagIds);

                if (fanficId > 0)
                {
                    MessageBox.Show("Fanfic added successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error while adding fanfic!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ An error occurred: {ex.Message}\n{ex.StackTrace}");
            }
        }



    }
}
