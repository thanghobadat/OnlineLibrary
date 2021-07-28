using Data.Enum;
using System;

namespace Data.Entities
{
    public class DocumentUser
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool RequestExtension { get; set; }
        public TheVote? Votes { get; set; }
    }
}
