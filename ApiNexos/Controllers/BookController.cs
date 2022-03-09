namespace ApiNexos.Controllers
{
    using BusinessRules.BusinessRules;
    using DataAccess.DB;
    using EntityLayer.Entities.InputData;
    using EntityLayer.Entities.OutputData;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class BookController : BookBl
    {
        public BookController(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        [HttpGet("GetTotalBooks")]
        public Response GetTotalBooks()
        {
            return GetListBooks();
        }

        [HttpGet("GetBookByAuthor")]
        public Response GetBookByAuthor(int autorId)
        {
            return GetListBooksFiltered(b => b.Author.ID == autorId);
        }

        [HttpGet("GetBookByTitle")]
        public Response GetBookByTitle(string title)
        {
            return GetListBooksFiltered(b => b.Title == title);
        }
        [HttpGet("GetBookByYear")]
        public Response GetBookByYear(string year)
        {
            return GetListBooksFiltered(b => b.Year == year);
        }

        [HttpPost("CreateBook")]
        public Response CreateNewBook([FromBody] BookEntity book)
        {
            return CreateBook(book);
        }
    }
}
