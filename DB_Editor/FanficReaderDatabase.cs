using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using DataModels;
using System.Data;

namespace DB_Editor
{
    public class FanficReaderDatabase
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FunficDataBase.db");
        private static string ConnectionString => $"Data Source={dbPath};Version=3;";

        public static List<ChapterModel> GetChapters(int fanficId)
        {
            List<ChapterModel> chapters = new List<ChapterModel>();

            string query = "SELECT Id, ChapterNumber, Title, FilePath FROM Chapters WHERE FanficId = @FanficId ORDER BY ChapterNumber";

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FanficId", fanficId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChapterModel chapter = new ChapterModel
                                {
                                    Id = reader.GetInt32(0),
                                    ChapterNumber = reader.GetInt32(1),
                                    Title = reader.GetString(2),
                                    FilePath = reader.GetString(3)
                                };
                                chapters.Add(chapter);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while getting chapters: {ex.Message}");
            }

            return chapters;
        }

        public static string GetChapterContent(string filePath)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(appDirectory, filePath);


            if (File.Exists(fullPath))
            {
                return File.ReadAllText(fullPath);
            }
            else
            {
                return $"Error: Chapter file not found! Path: {fullPath}";
            }
        }

        public static int AddFanfic(string title, int authorId, int categoryId, int genreId, List<int> tagIds)
        {
            int fanficId = -1;

            string query = @"
            INSERT INTO Fanfics (Title, Content, AuthorId, CategoryId, GenreId, DateAdded)
            VALUES (@Title, '', @AuthorId, @CategoryId, @GenreId, @DateAdded);
            SELECT last_insert_rowid();";

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@AuthorId", authorId);
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        command.Parameters.AddWithValue("@GenreId", genreId);
                        command.Parameters.AddWithValue("@DateAdded", DateTime.Now.ToString("yyyy-MM-dd"));

                        Console.WriteLine($"📌 SQL-запрос: {query}");
                        fanficId = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"✅ Фанфик добавлен с ID: {fanficId}");
                    }

                    foreach (var tagId in tagIds)
                    {
                        string tagQuery = "INSERT INTO FanficTags (FanficId, TagId) VALUES (@FanficId, @TagId)";
                        using (var tagCommand = new SQLiteCommand(tagQuery, connection))
                        {
                            tagCommand.Parameters.AddWithValue("@FanficId", fanficId);
                            tagCommand.Parameters.AddWithValue("@TagId", tagId);
                            tagCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка в AddFanfic(): {ex.Message}");
            }

            return fanficId;
        }



        public static void AddChapter(int fanficId, int chapterNumber, string title, string filePath)
        {
            string query = @"
            INSERT INTO Chapters (FanficId, ChapterNumber, Title, FilePath, DateAdded)
            VALUES (@FanficId, @ChapterNumber, @Title, @FilePath, @DateAdded)";

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FanficId", fanficId);
                    command.Parameters.AddWithValue("@ChapterNumber", chapterNumber);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@FilePath", filePath);
                    command.Parameters.AddWithValue("@DateAdded", DateTime.Now.ToString("yyyy-MM-dd"));

                    command.ExecuteNonQuery();
                }
            }
        }


        public static List<TagModel> GetTags()
        {
            List<TagModel> tags = new List<TagModel>();
            string query = "SELECT Id, Name FROM Tags"; // ONo rabotaet dngksngjsngksmgmsd

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tag = new TagModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            tags.Add(tag);
                        }
                    }
                }
            }

            Console.WriteLine("Из базы получено жанров: " + tags.Count);
            return tags;
        }


        public static int GetGenreIdByName(string name)
        {
            int genreId = -1;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id FROM Genres WHERE Name = @Name";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        genreId = Convert.ToInt32(result);
                    }
                }
            }
            return genreId;
        }

        public static List<CategoryModel> GetCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name FROM Categories;"; 
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = new CategoryModel
                        {
                            Id = reader.GetInt32(0),   
                            Name = reader.GetString(1) 
                        };
                        categories.Add(category);
                        Console.WriteLine($"Loaded category: {category.Id} - {category.Name}");
                    }
                }
            }
            return categories;
        }
        public static List<GenreModel> GetGenre()
        {
            List<GenreModel> genres = new List<GenreModel>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database opened successfully for genres!");

                    string query = "SELECT Id, Name FROM Genres;"; // Теперь получаем и ID, и Name
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var genre = new GenreModel
                            {
                                Id = reader.GetInt32(0),   // ID
                                Name = reader.GetString(1) // Name
                            };
                            genres.Add(genre);
                            Console.WriteLine($"Loaded genre: {genre.Id} - {genre.Name}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetGenre(): {ex.Message}");
            }

            return genres;
        }

        public static List<int> GetFanficTags(int fanficId)
        {
            List<int> tagIds = new List<int>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT TagId FROM FanficTags WHERE FanficId = @FanficId;";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FanficId", fanficId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tagIds.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return tagIds;
        }

        public static void DeleteFanfic(int fanficId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand("DELETE FROM FanficTags WHERE FanficId = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", fanficId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand("DELETE FROM Chapters WHERE FanficId = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", fanficId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand("DELETE FROM Fanfics WHERE Id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", fanficId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteChapter(int chapterId)
        {
            string query = "DELETE FROM Chapters WHERE Id = @Id";

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", chapterId);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
