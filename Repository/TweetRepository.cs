using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweet_Api.Data;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;
using Tweet_Api.Repository.IRepository;

namespace Tweet_Api.Repository
{
    public class TweetRepository : ITweetRepository
    {
        public DataContext _context { get; }

        public TweetRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tweet>> GetTweetsByUser(int userId)
        {
            return await _context.Tweets.Where(x => x.AppUserId == userId).OrderByDescending(x => x.PostedDate).ToListAsync();
        }

        public async Task<bool> PostNewTweet(Tweet tweet)
        {
            _context.Tweets.Add(tweet);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Tweet>> GetAllTweets()
        {
            return await _context.Tweets.OrderByDescending(x => x.PostedDate).ToListAsync();
        }

        public async Task<bool> UpdateTweet(Tweet tweet)
        {
            _context.Entry(tweet).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTweet(int tweetId)
        {
            var tweetToDelete = await _context.Tweets.FindAsync(tweetId);
            _context.Tweets.Remove(tweetToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> LikeTweet(string username, int tweetId, LikeDto likeDto)
        {
            if (likeDto.Id == 0)
            {
                var like = new Likes()
                {
                    LikedBy = username,
                    TweetId = tweetId,
                    LikedDate = DateTime.Now
                };
                _context.Likes.Add(like);
            }
            else
            {
                var likes = await GetLikes(tweetId);
                var like = likes.Where(x => x.Id == likeDto.Id).SingleOrDefault();
                _context.Likes.Remove(like);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReplyToTweet(string repliedTweet, string repliedBy, int tweetId)
        {
            var reply = new Replies()
            {
                Reply = repliedTweet,
                TweetId = tweetId,
                RepliedBy = repliedBy,
                RepliedDate = DateTime.Now
            };
            _context.Replies.Add(reply);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Likes>> GetLikes(int tweetId)
        {
            var likes = await _context.Likes.Where(x => x.TweetId == tweetId).ToListAsync();
            return likes;
        }

        public async Task<List<Replies>> GetReplies(int tweetId)
        {
            var replies = await _context.Replies.Where(x => x.TweetId == tweetId).ToListAsync();
            return replies;
        }

        public async Task<Tweet> GetTweetById(int id)
        {
            return await _context.Tweets.Where(x => x.TweetId == id).SingleOrDefaultAsync();
        }
    }
}