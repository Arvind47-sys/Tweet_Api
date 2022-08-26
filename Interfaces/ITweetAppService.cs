using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;

namespace Tweet_Api.Interfaces
{
    public interface ITweetAppService
    {
        Task<bool> PostNewTweet(TweetDto tweetDto, AppUser user);

        Task<IEnumerable<TweetDto>> GetTweetsByUser(AppUser user);

        Task<IEnumerable<TweetDto>> GetAllTweets();

        Task<bool> UpdateTweet(TweetDto tweetDto, AppUser user);

        Task<bool> DeleteTweet(int tweetId);

        Task<bool> LikeTweet(AppUser user, int tweetId, LikeDto like);

        Task<bool> ReplyToTweet(string tweet, AppUser user, int id);
    }
}