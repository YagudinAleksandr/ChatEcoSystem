using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ChatEcoSystem.Abstractions;

namespace ChatEcoSystem.AuthService.Logic
{
    internal class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        [return: MaybeNull]
        public Task<TKey> Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        [return: MaybeNull]
        public Task<TEntity> Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetAll(BaseFilter filter, int maxPageSize = 10, int startPosition = 1, int endPosition = 10)
        {
            throw new NotImplementedException();
        }

        [return: MaybeNull]
        public Task<TKey> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
