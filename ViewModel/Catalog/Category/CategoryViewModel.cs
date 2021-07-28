using System.ComponentModel.DataAnnotations;

namespace ViewModel.Catalog.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
