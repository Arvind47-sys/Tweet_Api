using AutoMapper;
using KafkaNet;
using KafkaNet.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;
using Tweet_Api.Interfaces;
using Tweet_Api.Repository.IRepository;

namespace Tweet_Api.Services
{
    public class TweetAppService : ITweetAppService
    {
        private readonly IMapper _mapper;
        private readonly ITweetRepository _tweetRepository;
        private readonly IConfiguration _config;

        public TweetAppService(IUserRepository userRepository, IMapper mapper,
            ITweetRepository tweetRepository, IConfiguration config)
        {
            _mapper = mapper;
            _tweetRepository = tweetRepository;
            _config = config;
        }

        public async Task<bool> DeleteTweet(int tweetId)
        {
            return await _tweetRepository.DeleteTweet(tweetId);
        }

        public async Task<IEnumerable<TweetDto>> GetAllTweets()
        {
            var tweets = _mapper.Map<List<TweetDto>>(await _tweetRepository.GetAllTweets());
            var allTweets = new List<TweetDto>();
            tweets.ForEach(t =>
            {
                t.Likes = new List<LikeDto>();
                var likes = _tweetRepository.GetLikes(t.TweetId).Result;
                var replies = _tweetRepository.GetReplies(t.TweetId).Result;
                likes.ForEach(l =>
                {
                    t.Likes.Add(_mapper.Map<LikeDto>(l));
                });
                replies.ForEach(r =>
                {
                    t.Replies.Add(_mapper.Map<ReplyDto>(r));
                });
                allTweets.Add(t);
            });

            return allTweets;
        }

        public async Task<IEnumerable<TweetDto>> GetTweetsByUser(AppUser user)
        {
            var tweets = _mapper.Map<List<TweetDto>>(await _tweetRepository.GetTweetsByUser(user.LoginId));
            var allTweetsByUser = new List<TweetDto>();
            tweets.ForEach(t =>
            {
                t.Likes = new List<LikeDto>();
                var likes = _tweetRepository.GetLikes(t.TweetId).Result;
                var replies = _tweetRepository.GetReplies(t.TweetId).Result;
                likes.ForEach(l =>
                {
                    t.Likes.Add(_mapper.Map<LikeDto>(l));
                });
                replies.ForEach(r =>
                {
                    t.Replies.Add(_mapper.Map<ReplyDto>(r));
                });
                allTweetsByUser.Add(t);
            });

            return allTweetsByUser;
        }

        public async Task<bool> LikeTweet(AppUser user, int tweetId, LikeDto like)
        {
            return await _tweetRepository.LikeTweet(user.Username, tweetId, like);
        }

        public async Task<bool> PostNewTweet(TweetDto tweetDto, AppUser user)
        {
            var result = false;
            var tweet = _mapper.Map<Tweet>(tweetDto);
            tweet.Username = user.Username;
            if (user != null)
            {
                tweet.AppUserId = user.LoginId;
                tweet.PostedDate = DateTime.Now;
                result = await _tweetRepository.PostNewTweet(tweet);
                if (result)
                {
                    var payLoad = $"Tag: {tweet.Tag}\tBody: {tweet.Body}";
                    sendMessageToKafkaTopic(payLoad);
                }
            }
            return result;
        }

        // Sending posted tweet to Apache Kafka
        private void sendMessageToKafkaTopic(string payload)
        {
            Uri uri = new Uri(_config["KafkaEndPoint"]);
            string topic = _config["KafkaTopicName"];
            var sendMessage = new Thread(() =>
            {
                KafkaNet.Protocol.Message msg = new KafkaNet.Protocol.Message(payload);
                var options = new KafkaOptions(uri);
                var router = new BrokerRouter(options);
                var client = new Producer(router);
                client.SendMessageAsync(topic, new List<KafkaNet.Protocol.Message> { msg }).Wait();
            });
            sendMessage.Start();
        }

        public async Task<bool> ReplyToTweet(string tweet, AppUser user, int id)
        {
            return await _tweetRepository.ReplyToTweet(tweet, user.Username, id);
        }

        public async Task<bool> UpdateTweet(TweetDto tweetDto, AppUser user)
        {
            var tweet = _mapper.Map<Tweet>(tweetDto);
            tweet.AppUserId = user.LoginId;
            tweet.PostedDate = DateTime.Now;
            return await _tweetRepository.UpdateTweet(tweet);
        }
    }
}