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
    public class DataHelper
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FunficDataBase.db");
        private static string ConnectionString => $"Data Source={dbPath};Version=3;";


        public static List<string> GetCategories()
        {

            List<string> categories = new List<string>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Name FROM Categories;";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(reader.GetString(0));
                        Console.WriteLine($"Loaded category: {reader.GetString(0)}");
                    }
                }
            }
            return categories;
        }

        public static List<string> GetGenre()
        {
            List<string> genres = new List<string>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database opened successfully for genres!");

                    string query = "SELECT Name FROM Genres;";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genres.Add(reader.GetString(0));
                            Console.WriteLine($"Loaded genre: {reader.GetString(0)}");
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

        public static List<string> GetTag()
        {
            List<string> tags = new List<string>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database opened successfully for genres!");

                    string query = "SELECT Name FROM Tags;";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tags.Add(reader.GetString(0));
                            Console.WriteLine($"Loaded tag: {reader.GetString(0)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTag(): {ex.Message}");
            }

            return tags;
        }

        public static List<AutorsModel> GetAuthors()
        {
            List<AutorsModel> authors = new List<AutorsModel>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Bio FROM Authors";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AutorsModel author = new AutorsModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Bio = reader.GetString(2)
                            };
                            Console.WriteLine($"[DEBUG] Автор загружен: {author.Name}, ID: {author.Id}");
                            authors.Add(author);
                        }
                    }
                }
            }
            return authors;
        }

        public static List<FanficModel> GetFanfics()
        {
            List<FanficModel> fanfics = new List<FanficModel>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Title, Content, AuthorId, CategoryId, GenreId, DateAdded FROM Fanfics;";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fanfics.Add(new FanficModel
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(2),
                            AuthorId = reader.GetInt32(3),
                            CategoryId = reader.GetInt32(4),
                            GenreId = reader.GetInt32(5),
                            DateAdded = reader.GetString(6)
                        });
                    }
                }
            }

            return fanfics;
        }

        public static string GetAuthorName(int authorId)
        {
            string authorName = "Unknown"; 
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT Name FROM Authors WHERE Id = @AuthorId;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AuthorId", authorId);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            authorName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadAuthors(): {ex.Message}");
            }

            return authorName;
        }

        public static List<FanficModel> GetFilteredFanfics(int? categoryId, int? genreId, int? tagId)
        {
            List<FanficModel> fanfics = new List<FanficModel>();

            string query = @"
            SELECT DISTINCT F.Id, F.Title, F.Content, F.AuthorId, F.CategoryId, F.GenreId, F.DateAdded
            FROM Fanfics F
            LEFT JOIN FanficTags FT ON F.Id = FT.FanficId
            WHERE 
                (@CategoryId IS NULL OR F.CategoryId = @CategoryId)
                AND (@GenreId IS NULL OR F.GenreId = @GenreId)
                AND (@TagId IS NULL OR FT.TagId = @TagId)";

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId.HasValue ? categoryId.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@GenreId", genreId.HasValue ? genreId.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TagId", tagId.HasValue ? tagId.Value : (object)DBNull.Value);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fanfics.Add(new FanficModel
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Content = reader.GetString(2),
                                AuthorId = reader.GetInt32(3),
                                CategoryId = reader.GetInt32(4),
                                GenreId = reader.GetInt32(5),
                                DateAdded = reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return fanfics;
        }

        public static int? GetCategoryIdByName(string name)
        {
            string query = "SELECT Id FROM Categories WHERE Name = @Name;";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : (int?)null;
                }
            }
        }

        public static int? GetGenreIdByName(string name)
        {
            string query = "SELECT Id FROM Genres WHERE Name = @Name;";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : (int?)null;
                }
            }
        }

        public static int? GetTagIdByName(string name)
        {
            string query = "SELECT Id FROM Tags WHERE Name = @Name;";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : (int?)null;
                }
            }
        }

        public static List<FanficModel> GetFanficsByAuthor(int authorId)
        {
            List<FanficModel> fanfics = new List<FanficModel>();

         

            string query = @"
            SELECT Id, Title, Content, AuthorId, CategoryId, GenreId, DateAdded
            FROM Fanfics
            WHERE AuthorId = @AuthorId";

            Console.WriteLine($"[DEBUG] SQL Query: {query}");

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@AuthorId", DbType.Int32) { Value = authorId });

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FanficModel fanfic = new FanficModel
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    Content = reader.GetString(2),
                                    AuthorId = reader.GetInt32(3),
                                    CategoryId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    GenreId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                    DateAdded = reader.GetString(6)
                                };

                                fanfics.Add(fanfic);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR]: {ex.Message}");
                }
            }

            return fanfics;
        }

        public static List<string> GetFanficTagsFromFilters(int fanficId)
        {
            List<string> tags = new List<string>();

            string query = @"
        SELECT Tags.Name FROM Tags
        INNER JOIN FanficTags ON Tags.Id = FanficTags.TagId
        WHERE FanficTags.FanficId = @FanficId";

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
                            tags.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return tags;
        }

        public static string GetFanficCategoryFromFilters(int fanficId)
        {
            string category = "Unknown";

            string query = @"
            SELECT Categories.Name FROM Categories
            INNER JOIN Fanfics ON Categories.Id = Fanfics.CategoryId
            WHERE Fanfics.Id = @FanficId";

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FanficId", fanficId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category = reader.GetString(0);
                        }
                    }
                }
            }

            return category;
        }

        public static string GetFanficPairings(int fanficId)
        {
            string pairings = "Unknown";

            string query = @"
            SELECT Genres.Name FROM Genres
            INNER JOIN Fanfics ON Genres.Id = Fanfics.GenreId
            WHERE Fanfics.Id = @FanficId";

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
                            pairings = reader.GetString(0);
                        }
                    }
                }
            }

            return pairings;
        }

        public static void AddAuthor(string name, string bio)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Authors (Name, Bio) VALUES (@name, @bio)", connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@bio", bio);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteAuthor(int authorId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Authors WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", authorId);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
