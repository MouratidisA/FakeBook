using System;

namespace FakeBook.Models
{
    public class CustomPost
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool? Bold { get; set; }
        public string RandomColor { get; set; }
        public string FriendId { get; set; }
        public string OwnerId { get; set; }
    }
}