
namespace ApiNexos.Controllers
{
    using BusinessRules.BusinessRules;
    using DataAccess.DB;
    using EntityLayer.Entities.InputData;
    using EntityLayer.Entities.OutputData;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class EditorialController : EditorialBl
    {
        public EditorialController(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        [HttpGet("GetEditorial")]
        public Response GetTotalEditorials()
        {
            return GetListEditorial();
        }

        [HttpPost("CreateNewEditorial")]
        public Response CreateNewEditorial([FromBody] EditorialEntity editorial)
        {
            return CreateEditorial(editorial);
        }
    }
}
