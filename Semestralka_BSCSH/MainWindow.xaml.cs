using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DB_Editor;
using DataModels;


namespace Semestralka_BSCSH;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<FanficModel> allFanfics = new List<FanficModel>();
    public MainWindow()
    {
        InitializeComponent();
        CategoryList.ItemsSource = DataHelper.GetCategories();
        GenreList.ItemsSource = DataHelper.GetGenre();
        TegList.ItemsSource = DataHelper.GetTag();
        allFanfics = DataHelper.GetFanfics();
        UpdateFanficDisplay(allFanfics);

       
    }

 

    private void AuthorsButton_Click(object sender, RoutedEventArgs e)
    {
        AutorsPage authorsPage = new AutorsPage();
        authorsPage.Show();
    }

    private void UpdateFanficDisplay(List<FanficModel> fanfics)
    {
        FanficPanel.Children.Clear();

        foreach (var fanfic in fanfics)
        {
            Border border = new Border
            {
                Width = 250,
                Height = 400,
                Background = new SolidColorBrush(Color.FromRgb(40, 40, 40)),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1)
            };

            StackPanel mainPanel = new StackPanel();

            TextBlock titleBlock = new TextBlock
            {
                Text = fanfic.Title,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap
            };

            TextBlock authorBlock = new TextBlock
            {
                Text = $"Author: {DataHelper.GetAuthorName(fanfic.AuthorId)}",
                Foreground = Brushes.LightGray,
                FontSize = 14,
                Margin = new Thickness(0, 5, 0, 5)
            };

            // Категория
            WrapPanel categoryPanel = new WrapPanel
            {
                Margin = new Thickness(0, 5, 0, 5)
            };

            string category = DataHelper.GetFanficCategoryFromFilters(fanfic.Id);

            Border categoryBorder = new Border
            {
                Background = Brushes.DarkOrange,
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(5),
                Margin = new Thickness(3)
            };

            TextBlock categoryText = new TextBlock
            {
                Text = category,
                Foreground = Brushes.White,
                FontSize = 12
            };

            categoryBorder.Child = categoryText; 
            categoryPanel.Children.Add(categoryBorder);


            // Пейринги
            WrapPanel pairingsPanel = new WrapPanel
            {
                Margin = new Thickness(0, 5, 0, 5)
            };
            string pairings = DataHelper.GetFanficPairings(fanfic.Id);

            Border pairingsBorder = new Border
            {
                Background = Brushes.BlueViolet, 
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(5),
                Margin = new Thickness(3)
            };

            TextBlock pairingsText = new TextBlock
            {
                Text = pairings,
                Foreground = Brushes.White,
                FontSize = 12
            };

            pairingsBorder.Child = pairingsText; 
            pairingsPanel.Children.Add(pairingsBorder); 


            // Жанры (теги)
            WrapPanel tagsPanel = new WrapPanel
            {
                Margin = new Thickness(0, 5, 0, 5)
            };

            var tags = DataHelper.GetFanficTagsFromFilters(fanfic.Id);
            foreach (var tag in tags)
            {
                Border tagBorder = new Border
                {
                    Background = Brushes.DarkSlateGray,
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(5),
                    Margin = new Thickness(3)
                };

                TextBlock tagText = new TextBlock
                {
                    Text = tag,
                    Foreground = Brushes.White,
                    FontSize = 12
                };

                tagBorder.Child = tagText;
                tagsPanel.Children.Add(tagBorder);
            }

            Button readButton = new Button
            {
                Content = "Read",
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Background = Brushes.DarkRed,
                Foreground = Brushes.White,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(8, 5, 8, 5),
                Margin = new Thickness(0, 10, 0, 0),
                Cursor = Cursors.Hand,
                Tag = fanfic
            };

            Button deleteButton = new Button
            {
                Content = "Delete",
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Background = Brushes.DarkSlateGray,
                Foreground = Brushes.White,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(8, 5, 8, 5),
                Margin = new Thickness(5),
                Cursor = Cursors.Hand,
                Tag = fanfic
            };



            Button addChapterButton = new Button
            {
                Content = "Add Chapter",
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Background = Brushes.DarkGreen,
                Foreground = Brushes.White,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(8, 5, 8, 5),
                Margin = new Thickness(5),
                Cursor = Cursors.Hand,
                Tag = fanfic
            };
            addChapterButton.Click += OpenAddChapterWindow;

            deleteButton.Click += DeleteFanfic;






            readButton.Click += OpenFanfic;

            readButton.MouseEnter += (s, e) => readButton.Background = Brushes.Red;
            readButton.MouseLeave += (s, e) => readButton.Background = Brushes.DarkRed;

            mainPanel.Children.Add(titleBlock);
            mainPanel.Children.Add(authorBlock);
            mainPanel.Children.Add(categoryPanel); 
            mainPanel.Children.Add(pairingsPanel); 
            mainPanel.Children.Add(tagsPanel);     
            mainPanel.Children.Add(readButton);
            mainPanel.Children.Add(deleteButton);
            mainPanel.Children.Add(addChapterButton);



            border.Child = mainPanel;
            FanficPanel.Children.Add(border);
        }
    }




    private void OpenAddFanficWindow_Click(object sender, RoutedEventArgs e)
    {
        AddFanficWindow addFanficWindow = new AddFanficWindow();
        addFanficWindow.ShowDialog();
        allFanfics = DataHelper.GetFanfics();
        UpdateFanficDisplay(allFanfics);
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = SearchBox.Text.ToLower();

        var filteredFanfics = allFanfics
            .Where(f => f.Title.ToLower().Contains(searchText))
            .ToList();

        UpdateFanficDisplay(filteredFanfics);
    }

    private void FilterFanfics(object sender, SelectionChangedEventArgs e)
    {
        FanficPanel.Children.Clear();
        int? selectedCategory = CategoryList.SelectedItem is string categoryName
            ? DataHelper.GetCategoryIdByName(categoryName) : (int?)null;

        int? selectedGenre = GenreList.SelectedItem is string genreName
            ? DataHelper.GetGenreIdByName(genreName) : (int?)null;

        int? selectedTag = TegList.SelectedItem is string tagName
            ? DataHelper.GetTagIdByName(tagName) : (int?)null;

        var filteredFanfics = DataHelper.GetFilteredFanfics(selectedCategory, selectedGenre, selectedTag);
        UpdateFanficDisplay(filteredFanfics);
    }


    private void ResetFilters_Click(object sender, RoutedEventArgs e)
    {
        CategoryList.SelectedIndex = -1; 
        GenreList.SelectedIndex = -1;
        TegList.SelectedIndex = -1;
    }

    public void LoadFanficsByAuthor(int authorId)
    {
        var fanfics = DataHelper.GetFanficsByAuthor(authorId);

        UpdateFanficDisplay(fanfics);
    }

    private void FanficBtn_Click(object sender, RoutedEventArgs e)
    {
        UpdateFanficDisplay(allFanfics); 
    }

    private void OpenFanfic(object sender, RoutedEventArgs e)
    {
        var selectedFanfic = (FanficModel)((Button)sender).Tag;

        FanficReader readerWindow = new FanficReader(
            selectedFanfic.Id,
            selectedFanfic.Title,
            selectedFanfic.Content
        );

        readerWindow.Show();
    }

    private void DeleteFanfic(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedFanfic = button?.Tag as FanficModel;

        if (selectedFanfic == null)
            return;

        var result = MessageBox.Show($"Are you sure you want to delete the fanfic \"{selectedFanfic.Title}\"?",
                                     "Delete Confirmation",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                FanficReaderDatabase.DeleteFanfic(selectedFanfic.Id);
                allFanfics = DataHelper.GetFanfics();
                UpdateFanficDisplay(allFanfics);
                MessageBox.Show("Fanfic deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting fanfic: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void OpenAddChapterWindow(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var fanfic = button?.Tag as FanficModel;

        if (fanfic != null)
        {
            var chapterWindow = new AddChapterWindow(fanfic.Id);
            chapterWindow.ShowDialog();
        }
    }





}