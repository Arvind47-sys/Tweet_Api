using System;

namespace Tweet_Api.Entities
{
    public class Replies
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public string RepliedBy { get; set; }
        public DateTime RepliedDate { get; set; }

        /// <summary>
        /// Foreign key references Tweet Entity
        /// </summary>
        public Tweet Tweet { get; set; }

        public int TweetId { get; set; }
    }
}