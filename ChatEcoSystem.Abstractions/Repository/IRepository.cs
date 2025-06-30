using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ChatEcoSystem.Abstractions
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public interface IRepository<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>ИД</returns>
        [return: MaybeNull]
        Task<TKey> Create(TEntity entity);


        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>ИД</returns>
        [return: MaybeNull]
        Task<TKey> Update(TEntity entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task Delete(TEntity entity);

        /// <summary>
        /// Получение сущности по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Сущность</returns>
        [return: MaybeNull]
        Task<TEntity> Get(TKey id);

        /// <summary>
        /// Получение списка сущностей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="maxPageSize"></param>
        /// <param name="startPosition">Стартовая позиция</param>
        /// <param name="endPosition">Конечная позиция</param>
        /// <returns>Коллекция элементов</returns>
        Task<IReadOnlyCollection<TEntity>> GetAll(BaseFilter filter, int maxPageSize = 10, int startPosition = 1, int endPosition = 10);
    }
}
