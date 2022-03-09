namespace DataAccess.Dto
{
    using System.ComponentModel.DataAnnotations;
    public class Editorial
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int MaxBooks { get; set; }
    }
}
