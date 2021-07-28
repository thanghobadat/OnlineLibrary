using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public int View { get; set; }
        public bool IsShow { get; set; }

        public List<DocumentCategory> DocumentCategories { get; set; }
        public List<Chapter> Chapters { get; set; }
        public List<DocumentUser> DocumentUsers { get; set; }
        public DocumentImage DocumentImage { get; set; }
    }
}
