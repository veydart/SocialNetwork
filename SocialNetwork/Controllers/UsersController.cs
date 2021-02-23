using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Abstractions;
using SocialNetwork.Domain.Models.Request;
using SocialNetwork.Domain.Models.View;
using SocialNetwork.Extensions.Collections;

namespace SocialNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSubscribeService _subscribeService;

        public UsersController(IUserService userService, IUserSubscribeService subscribeService)
        {
            _userService = userService;
            _subscribeService = subscribeService;
        }

        [HttpGet("All")]
        public async Task<CollectionResult<UserViewModel>> GetAll(int offset, int count)
        {
            var users = _userService.GetAll();

            return await users.AsCollection(offset, count);
        }

        [HttpPost]
        public async Task Create(UserRequestModel request)
        {
            await _userService.Create(request);
        }
        
        [HttpPost("Subscribe")] //Знаю что не желательно называть методы апи при помощи глаголов, но ничего другого в голову не пришло
        public async Task Subscribe(UserSubscribeRequestModel request)
        {
            await _subscribeService.SubscribeOn(request);
        }

        [HttpGet("Popular")]
        public async Task<CollectionResult<PopularUserViewModel>> GetPopular(int offset, int count)
        {
            var users = _subscribeService.GetPopular();

            return await users.AsCollection(offset, count);
        }
    }   
}