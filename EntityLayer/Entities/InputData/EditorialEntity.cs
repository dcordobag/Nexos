namespace EntityLayer.Entities.InputData
{
    public class EditorialEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int MaxBooks { get; set; } = -1;
    }
}
