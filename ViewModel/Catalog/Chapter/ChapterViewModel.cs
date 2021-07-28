using System.ComponentModel.DataAnnotations;

namespace ViewModel.Catalog.Chapter
{
    public class ChapterViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Chapter Name")]
        public string Name { get; set; }
        public string Content { get; set; }
        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }
        public int DocumentId { get; set; }
    }
}
