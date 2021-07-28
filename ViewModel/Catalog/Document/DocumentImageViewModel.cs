using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Catalog.Document
{
    public class DocumentImageViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        [Display(Name = "Size")]
        public long FizeSize { get; set; }
        [Display(Name = "Choose Image")]
        public IFormFile ThumnailImage { get; set; }
    }
}
