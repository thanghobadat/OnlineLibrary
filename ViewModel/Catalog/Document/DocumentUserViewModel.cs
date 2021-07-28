using Data.Enum;
using System;

namespace ViewModel.Catalog.Document
{
    public class DocumentUserViewModel
    {
        public int DocumentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool RequestExtension { get; set; }
        public TheVote? Votes { get; set; }

    }
}
