using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Domain.Models.Request;
using SocialNetwork.Domain.Models.View;

namespace SocialNetwork.Domain.Abstractions
{
    public interface IUserService
    {
        Task Create(UserRequestModel model);

        IQueryable<UserViewModel> GetAll();
    }
}
