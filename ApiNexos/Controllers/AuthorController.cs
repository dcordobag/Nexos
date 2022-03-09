namespace ApiNexos.Controllers
{
    using BusinessRules.BusinessRules;
    using DataAccess.DB;
    using EntityLayer.Entities.InputData;
    using EntityLayer.Entities.OutputData;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class AuthorController : AuthorBl
    {
        public AuthorController(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        [HttpGet("GetAuthors")]
        public Response GetTotalAuthors()
        {
            return GetListAuthors();
        }

        [HttpPost("CreateAuthor")]
        public Response CreateAuthor([FromBody] AuthorEntity author)
        {
            return Create(author);
        }
    }
}
