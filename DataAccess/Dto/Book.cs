namespace DataAccess.Dto
{
    using System.ComponentModel.DataAnnotations;
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public int PagesNumber { get; set; }
        public int EditorialID { get; set; }
        public int AuthorID { get; set; }
        public Editorial Editorial { get; set; }
        public Author Author { get; set; }
    }
}
