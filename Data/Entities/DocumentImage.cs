using System;

namespace Data.Entities
{
    public class DocumentImage
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImagePath { get; set; }
        public long FizeSize { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
