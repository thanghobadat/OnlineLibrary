using System.Collections.Generic;

namespace ViewModel.Catalog.Document
{
    public class CategoryAssignResult
    {
        public DocumentAssignViewModel Document { get; set; }
        public List<CategoryAssignViewModel> ExistsCategory { get; set; }
        public List<CategoryAssignViewModel> NonExistCategory { get; set; }
    }
}
