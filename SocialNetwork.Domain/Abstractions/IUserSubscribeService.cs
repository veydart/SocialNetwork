using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Domain.Models.Request;
using SocialNetwork.Domain.Models.View;

namespace SocialNetwork.Domain.Abstractions
{
    public interface IUserSubscribeService
    {
        Task SubscribeOn(UserSubscribeRequestModel model);

        IQueryable<PopularUserViewModel> GetPopular();
    }
}
