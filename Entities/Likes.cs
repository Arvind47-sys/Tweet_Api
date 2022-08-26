using System;

namespace Tweet_Api.Entities
{
    public class Likes
    {
        public int Id { get; set; }
        public string LikedBy { get; set; }
        public DateTime LikedDate { get; set; }

        /// <summary>
        /// Foreign key references Tweet Entity
        /// </summary>
        public Tweet Tweet { get; set; }

        public int TweetId { get; set; }
    }
}