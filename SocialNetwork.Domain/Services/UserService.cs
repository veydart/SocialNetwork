using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Abstractions;
using SocialNetwork.Domain.Database;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Models.Request;
using SocialNetwork.Domain.Models.View;

namespace SocialNetwork.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(UserRequestModel model)
        {
            var newUser = new User(model);
            await _userRepository.Create(newUser);
        }

        public IQueryable<UserViewModel> GetAll()
        {
            return _userRepository.Get().AsNoTracking().Select(x => new UserViewModel(x));
        }
    }
}