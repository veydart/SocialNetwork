using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Domain.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SocialNetworkDbContext _dbContext;

        public Repository(SocialNetworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Get()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}