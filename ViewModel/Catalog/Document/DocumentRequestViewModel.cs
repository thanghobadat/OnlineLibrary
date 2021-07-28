using System;

namespace ViewModel.Catalog.Document
{
    public class DocumentRequestViewModel
    {
        public int DocumentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DocumentName { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
