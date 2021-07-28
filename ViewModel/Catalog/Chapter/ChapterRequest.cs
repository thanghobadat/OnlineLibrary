namespace ViewModel.Catalog.Chapter
{
    public class ChapterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int SortOrder { get; set; }
        public int DocumentId { get; set; }
    }
}
