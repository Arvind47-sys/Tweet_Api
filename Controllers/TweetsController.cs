using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;
using Tweet_Api.Extensions;
using Tweet_Api.Interfaces;
using Tweet_Api.Repository.IRepository;

namespace Tweet_Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1.0/[controller]")]
    public class TweetsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITweetRepository _tweetRepository;
        private readonly IMapper _mappper;
        private readonly ITokenService _tokenService;
        private readonly ITweetAppService _tweetAppService;

        public TweetsController(IUserRepository userRepository, ITweetRepository tweetRepository,
            IMapper mappper, ITokenService tokenService,
            ITweetAppService tweetAppService)
        {
            _userRepository = userRepository;
            _tweetRepository = tweetRepository;
            _mappper = mappper;
            _tokenService = tokenService;
            _tweetAppService = tweetAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userRepository.EmailExists(registerDto.Email)) return BadRequest("Email is already used");
            if (await _userRepository.UserExists(registerDto.Username)) return BadRequest("Username is taken");
            var newUser = new AppUser();
            if (await _userRepository.AddUser(_mappper.Map(registerDto, newUser)))
            {
                return new UserDto
                {
                    Username = newUser.Username,
                    Token = _tokenService.CreateToken(newUser),
                    Name = newUser.FirstName + " " + newUser.LastName
                };
            }
            return BadRequest("Problem in adding user");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.AuthenticateUser(loginDto);
            if (user == null) return Unauthorized("Invalid Username and or Password");

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                Name = user.FirstName + " " + user.LastName
            };
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("forgot/{username}")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto, string username)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null) return BadRequest("Invalid Username");

            user.Password = forgotPasswordDto.NewPassword;

            if (await _userRepository.Update(user))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost]
        [Route("reset/{email}")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto, string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return BadRequest("Invalid Email");
            if (user.Username != User.GetUserName()) return Unauthorized();
            if (user.Password != resetPasswordDto.OldPassword) return BadRequest("Old Password is not correct");

            user.Password = resetPasswordDto.NewPassword;

            if (await _userRepository.Update(user))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<TweetDto>>> GetAllTweets()
        {
            var allTweets = await _tweetAppService.GetAllTweets();
            return Ok(allTweets);
        }

        [HttpGet]
        [Route("users/all")]
        public async Task<ActionResult<IEnumerable<UserDetailDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var userDetails = _mappper.Map<List<UserDetailDto>>(users);
            return Ok(userDetails);
        }

        [HttpGet]
        [Route("user/search/{username}")]
        public async Task<ActionResult<AppUser>> SearchByUsername(string username)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null)
                return BadRequest("Username not found");
            return Ok(user);
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<IEnumerable<TweetDto>>> GetAllTweetsOfUser(string username)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var tweetsByUser = await _tweetAppService.GetTweetsByUser(user);
            return Ok(tweetsByUser);
        }

        [HttpPost]
        [Route("add/{username}")]
        public async Task<ActionResult<bool>> PostNewTweet([FromBody] TweetDto tweet, string username)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _tweetAppService.PostNewTweet(tweet, user);
            return Ok(result);
        }

        [HttpPost]
        [Route("update/{username}/{id}")]
        public async Task<ActionResult<bool>> UpdateTweet([FromBody] TweetDto tweet, string username)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null)
            {
                return Unauthorized();
            }
            if (tweet.UserId != user.LoginId)
            {
                return Unauthorized();
            }
            return await _tweetAppService.UpdateTweet(tweet, user);
        }

        [HttpDelete]
        [Route("delete/{username}/{id}")]
        public async Task<ActionResult<bool>> DeleteTweet(string username, int id)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var tweetToBeDeleted = await _tweetRepository.GetTweetById(id);
            if (tweetToBeDeleted == null)
            {
                return NotFound();
            }
            if (tweetToBeDeleted.AppUserId != user.LoginId)
            {
                return Unauthorized();
            }
            return await _tweetAppService.DeleteTweet(id);
        }

        [HttpPost]
        [Route("like/{username}/{id}")]
        public async Task<ActionResult<bool>> LikeTweet(string username, int id, [FromBody] LikeDto like)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null)
            {
                return BadRequest("Invalid Username");
            }
            return await _tweetAppService.LikeTweet(user, id, like);
        }

        [HttpPost]
        [Route("reply/{username}/{id}")]
        public async Task<ActionResult<bool>> ReplyToTweet([FromBody] ReplyDto reply, string username, int id)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user == null)
            {
                return Unauthorized();
            }
            return await _tweetAppService.ReplyToTweet(reply.Reply, user, id);
        }
    }
}