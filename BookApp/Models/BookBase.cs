using System;

/* This class contains the base elements of Book and BookExtended. */

namespace Models
{
    public class BookBase
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public Guid UserId { get; set; }
    }
}