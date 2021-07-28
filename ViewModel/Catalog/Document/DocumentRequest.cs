using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Catalog.Document
{
    public class DocumentRequest
    {
        public int Id { get; set; }
        [Display(Name = "Document Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        [Display(Name = "Choose Image")]
        public IFormFile ThumbnailImage { get; set; }
    }
}
