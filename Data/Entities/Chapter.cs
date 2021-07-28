namespace Data.Entities
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int SortOrder { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
