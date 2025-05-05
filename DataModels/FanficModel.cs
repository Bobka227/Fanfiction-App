namespace DB_Editor
{
    public class FanficModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int GenreId { get; set; }
        public string DateAdded { get; set; }

        public List<int> TagIds { get; set; } = new List<int>(); // ✅ Добавляем список жанров

    }
}
