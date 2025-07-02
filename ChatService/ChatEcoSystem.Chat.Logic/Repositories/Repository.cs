using System.Linq;
using System.Threading.Tasks;
using ChatEcoSystem.Chat.Logic.Data;
using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.Chat.Logic.Repositories
{
    /// <summary>
    /// Репозиторий сервиса чатов
    /// </summary>
    /// <inheritdoc cref="IRepository{TEntity, TKey}"/>
    internal class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        #region CTOR
        /// <inheritdoc cref="UserAuthContext"/>
        private readonly ChatContext _context;

        /// <inheritdoc cref="DbSet{TEntity}"/>
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        #endregion

        public async Task<TEntity> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<TEntity>> GetAll(BaseFilter filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null && !string.IsNullOrWhiteSpace(filter.SortBy))
            {
                // Для простоты сортируем только по Id
                if (filter.SortBy.ToLower() == "id")
                {
                    query = filter.SortDirection.ToLower() == "desc"
                        ? query.OrderByDescending(e => e.Id)
                        : query.OrderBy(e => e.Id);
                }
            }
            else
            {
                query = query.OrderBy(e => e.Id);
            }

            if (filter != null)
            {
                query = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }

            return await Task.FromResult(query);
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
