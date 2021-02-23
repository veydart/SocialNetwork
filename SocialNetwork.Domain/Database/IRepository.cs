using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Database
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity id);
    }
}