using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tweet_Api.DTOs
{
    public class TweetDto
    {
        public int UserId { get; set; }
        public int TweetId { get; set; }

        public string Username { get; set; }

        [MaxLength(50)]
        public string Tag { get; set; }

        [MaxLength(144)]
        public string Body { get; set; }

        public List<LikeDto> Likes { get; set; }

        public List<ReplyDto> Replies { get; set; }

        public DateTime PostedDate { get; set; }
    }

    public class ReplyDto
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public string RepliedBy { get; set; }

        public DateTime RepliedDate { get; set; }
    }

    public class LikeDto
    {
        public int Id { get; set; }
        public string LikedBy { get; set; }
        public DateTime LikedDate { get; set; }
    }
}