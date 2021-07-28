using Data.Enum;
using System;

namespace ViewModel.Catalog.Document
{
    public class UserVoteRequest
    {
        public int DocumentId { get; set; }
        public Guid UserId { get; set; }
        public TheVote? Vote { get; set; }
    }
}
