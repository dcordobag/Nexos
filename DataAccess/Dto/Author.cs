
namespace DataAccess.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class Author
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string OriginCity { get; set; }
        public string Email { get; set; }
    }
}
