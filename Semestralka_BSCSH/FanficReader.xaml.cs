using DataModels;
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
  
    public partial class FanficReader : Window
    {
        private int fanficId;
        private ChapterModel? previouslySelectedChapter = null;


        public FanficReader(int fanficId, string title, string description)
        {
            InitializeComponent();
            this.fanficId = fanficId;
            FanficTitle.Text = title;
            FanficDescription.Text = description;

            LoadChapters();
        }

        private void LoadChapters()
        {
            var chapters = FanficReaderDatabase.GetChapters(fanficId);
            ChaptersList.ItemsSource = chapters;
            ChaptersList.DisplayMemberPath = "Title"; 
        }


        private void ChaptersList_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var item = ChaptersList.SelectedItem as ChapterModel;
            if (item == null)
                return;

            if (previouslySelectedChapter != null && previouslySelectedChapter.Id == item.Id)
            {
                ChaptersList.SelectedItem = null;
                ChapterContent.Text = "";
                previouslySelectedChapter = null;
            }
            else
            {
                ChapterContent.Text = FanficReaderDatabase.GetChapterContent(item.FilePath);
                previouslySelectedChapter = item;
            }
        }



        private void ChaptersListBox_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (ChaptersList.SelectedItem is ChapterModel selectedChapter)
            {
                var result = MessageBox.Show($"Do you want to delete chapter \"{selectedChapter.Title}\"?",
                                             "Delete Chapter",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        FanficReaderDatabase.DeleteChapter(selectedChapter.Id);
                        LoadChapters(); 
                        ChapterContent.Text = ""; 
                        MessageBox.Show("Chapter deleted successfully.", "Success");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting chapter: {ex.Message}", "Error");
                    }
                }
            }
        }

    }
}

