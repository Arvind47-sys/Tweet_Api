using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;

namespace Tweet_Api.Repository.IRepository
{
    public interface ITweetRepository
    {
        Task<IEnumerable<Tweet>> GetTweetsByUser(int userId);

        Task<bool> PostNewTweet(Tweet tweetDto);

        Task<IEnumerable<Tweet>> GetAllTweets();

        Task<bool> UpdateTweet(Tweet tweet);

        Task<bool> DeleteTweet(int tweetId);

        Task<bool> LikeTweet(string username, int tweetId, LikeDto likeDto);

        Task<bool> ReplyToTweet(string repliedTweet, string repliedBy, int tweetId);

        Task<List<Likes>> GetLikes(int tweetId);

        Task<List<Replies>> GetReplies(int tweetId);

        Task<Tweet> GetTweetById(int id);
    }
}