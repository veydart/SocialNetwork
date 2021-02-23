using System.ComponentModel.DataAnnotations;
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
    public class UserSubscribeService : IUserSubscribeService
    {
        private readonly IRepository<User> _userRepository;

        private readonly IRepository<UserSubscriber> _userSubscriberRepository;

        public UserSubscribeService(IRepository<User> userRepository, IRepository<UserSubscriber> userSubscriberRepository)
        {
            _userRepository = userRepository;
            _userSubscriberRepository = userSubscriberRepository;
        }

        public async Task SubscribeOn(UserSubscribeRequestModel model)
        {
            await ValidateOnSubscribe(model);

            var userSubscriber = new UserSubscriber(model);

            await _userSubscriberRepository.Create(userSubscriber);
        }

        public IQueryable<PopularUserViewModel> GetPopular()
        {
            var users = _userRepository.Get().AsNoTracking();
            var userSubscribers = _userSubscriberRepository.Get().AsNoTracking();

            var result = from u in users
                       join us in userSubscribers on u.Id equals us.UserId into j1
                       from j2 in j1.DefaultIfEmpty()
                       group j2 by new { u.Id, u.Name }
                       into grouped
                       select new PopularUserViewModel
                       {
                           Id = grouped.Key.Id,
                           Name = grouped.Key.Name,
                           SubscriberCount = grouped.Count(x => x.UserId != null)
                       };

            return result.OrderByDescending(x => x.SubscriberCount);
        }

        private async Task ValidateOnSubscribe(UserSubscribeRequestModel model)
        {
            if (model.UserId == model.SubscriberId)
            {
                throw new ValidationException("Same id`s");
            }

            var subscriber = await _userRepository.Get().AsNoTracking().SingleOrDefaultAsync(x => x.Id == model.SubscriberId);
            if (subscriber == null)
            {
                throw new ValidationException("Subscriber not found");
            }

            var user = await _userRepository.Get().AsNoTracking().SingleOrDefaultAsync(x => x.Id == model.UserId);
            if (user == null)
            {
                throw new ValidationException("User not found");
            }

            var subscribeUser = await _userSubscriberRepository
                                      .Get()
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(x => x.UserId == model.UserId && x.SubscriberId == model.SubscriberId);
            if (subscribeUser != null)
            {
                throw new ValidationException("This user has already subscribed");
            }
        }
    }
}