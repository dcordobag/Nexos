
namespace EntityLayer.Entities.InputData
{
    using System;

    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string OriginCity { get; set; }
        public string Email { get; set; }
    }
}
