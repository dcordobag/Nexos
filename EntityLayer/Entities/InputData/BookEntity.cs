namespace EntityLayer.Entities.InputData
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public int PagesNumber { get; set; }
        public int EditorialID { get; set; }
        public int AuthorID { get; set; }
    }
}
