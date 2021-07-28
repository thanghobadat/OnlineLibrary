using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Catalog.Document
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public int View { get; set; }
        public bool IsShow { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
    }
}
