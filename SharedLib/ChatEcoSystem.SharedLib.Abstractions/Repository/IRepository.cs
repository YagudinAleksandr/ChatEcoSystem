using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ChatEcoSystem.SharedLib.Abstractions
{
    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    /// <typeparam name="TKey">Тип ИД</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Созданная сущность</returns>
        Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Обновленная сущность</returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Получение сущности по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Сущность</returns>
        [return: MaybeNull]
        Task<TEntity> GetById(TKey id);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task Delete(TEntity entity);

        /// <summary>
        /// Получение списка сущностей
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAll(BaseFilter filter = null);
    }
}
