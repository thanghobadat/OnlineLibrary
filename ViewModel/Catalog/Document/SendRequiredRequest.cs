using System;

namespace ViewModel.Catalog.Document
{
    public class SendRequiredRequest
    {
        public int DocumentId { get; set; }
        public Guid UserId { get; set; }
    }
}
