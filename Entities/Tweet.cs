using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tweet_Api.Entities
{
    public class Tweet
    {
        [Key]
        public int TweetId { get; set; }

        public string Username { get; set; }

        [MaxLength(50)]
        public string Tag { get; set; }

        [MaxLength(144)]
        public string Body { get; set; }

        public ICollection<Likes> LikedBy { get; set; }

        public ICollection<Replies> Replies { get; set; }

        public DateTime PostedDate { get; set; }

        /// <summary>
        /// Foreign key references AppUser
        /// </summary>
        public AppUser AppUser { get; set; }

        public int AppUserId { get; set; }
    }
}