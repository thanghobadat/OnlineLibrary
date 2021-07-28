using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Catalog.Document
{
    public class DocumentImageUpdateRequest
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        [Display(Name = "Choose Image")]
        public IFormFile ThumbnailImage { get; set; }
    }
}
