using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ChatEcoSystem.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatEcoSystem.AuthService.Logic
{
    internal class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
    {
        #region CTOR
        /// <inheritdoc cref="UserAuthDbContext"/>
        private readonly UserAuthDbContext _context;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger<Repository<TKey, TEntity>> _logger;

        public Repository(UserAuthDbContext context, ILogger<Repository<TKey, TEntity>> logger)
        {
            _logger = logger;
            _context = context;
        }
        #endregion

        [return: MaybeNull]
        public async Task<TKey> Create(TEntity entity)
        {
            try
            {

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public async Task Delete(TEntity entity)
        {
            try
            {
                var entities = _context.Set<TEntity>();
                entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Can not delete entity", ex);
            }
        }

        [return: MaybeNull]
        public async Task<TEntity> Get(BaseFilter filter)
        {
            try
            {
                var entities = _context.Set<TEntity>();
                var entity = await entities.FirstOrDefaultAsync(filter.Filters);

                if (entity == null)
                {
                    _logger.LogWarning($"Cannot find entity with params {filter.Criteria}");
                    return null;
                }

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot filter entity", ex);
                return null;
            }
        }

        public Task<IReadOnlyCollection<TEntity>> GetAll(BaseFilter filter, int maxPageSize = 10, int startPosition = 1, int endPosition = 10)
        {
            throw new NotImplementedException();
        }

        [return: MaybeNull]
        public async Task<TEntity> GetById(TKey id)
        {
            try
            {
                var entities = _context.Set<TEntity>();
                var entity = await entities.Where(x => x.Id == id).FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    _logger.LogWarning($"Cannot find entity with ID {id}");
                    return null;
                }

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot filter entity", ex);
                return null;
            }
        }

        [return: MaybeNull]
        public async Task<TKey> Update(TEntity entity)
        {
            try
            {
                var entities = _context.Set<TEntity>();

                entities.Update(entity);

                await _context.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Can not update entity", ex);
                return default;
            }
        }

        private int ApplyFilter(BaseFilter filter)
        {
            var dbSet = _context.Set<TEntity>();
            var query = dbSet.AsQueryable();
            // Применяем фильтры
            if (filter.Filters != null && filter.Filters.Any())
            {
                var entityType = typeof(TEntity);

                foreach (var kvp in filter.Filters)
                {
                    if (kvp.Value != null)
                    {
                        var propertyName = kvp.Key;
                        var propertyValue = kvp.Value;

                        // Проверяем существование свойства
                        var property = entityType.GetProperty(propertyName);
                        if (property != null)
                        {
                            // Применяем фильтр в зависимости от типа свойства
                            if (property.PropertyType == typeof(string))
                            {
                                // Для строковых полей - поиск по подстроке (содержит)
                                var stringValue = propertyValue.ToString();
                                if (!string.IsNullOrEmpty(stringValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<string>(entity, propertyName).Contains(stringValue));
                                }
                            }
                            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                            {
                                // Для целых чисел - точное совпадение
                                if (int.TryParse(propertyValue.ToString(), out int intValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<int>(entity, propertyName) == intValue);
                                }
                            }
                            else if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Guid?))
                            {
                                // Для GUID - точное совпадение
                                if (Guid.TryParse(propertyValue.ToString(), out Guid guidValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<Guid>(entity, propertyName) == guidValue);
                                }
                            }
                            else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                            {
                                // Для булевых значений - точное совпадение
                                if (bool.TryParse(propertyValue.ToString(), out bool boolValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<bool>(entity, propertyName) == boolValue);
                                }
                            }
                            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                            {
                                // Для дат - точное совпадение
                                if (DateTime.TryParse(propertyValue.ToString(), out DateTime dateValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<DateTime>(entity, propertyName) == dateValue);
                                }
                            }
                            else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
                            {
                                // Для double - точное совпадение
                                if (double.TryParse(propertyValue.ToString(), out double doubleValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<double>(entity, propertyName) == doubleValue);
                                }
                            }
                            else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                            {
                                // Для decimal - точное совпадение
                                if (decimal.TryParse(propertyValue.ToString(), out decimal decimalValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<decimal>(entity, propertyName) == decimalValue);
                                }
                            }
                            else if (property.PropertyType.IsEnum)
                            {
                                // Для enum - точное совпадение
                                if (Enum.TryParse(property.PropertyType, propertyValue.ToString(), out var enumValue))
                                {
                                    query = query.Where(entity =>
                                        EF.Property<object>(entity, propertyName).Equals(enumValue));
                                }
                            }
                            else
                            {
                                // Для остальных типов - попытка точного совпадения
                                try
                                {
                                    var convertedValue = Convert.ChangeType(propertyValue, property.PropertyType);
                                    query = query.Where(entity =>
                                        EF.Property<object>(entity, propertyName).Equals(convertedValue));
                                }
                                catch
                                {
                                    // Игнорируем фильтр если не удалось привести к нужному типу
                                    _logger.LogWarning($"Cannot apply filter for property {propertyName} with value {propertyValue}");
                                }
                            }
                        }
                    }
                }
            }

            // Применяем сортировку
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                var sortOrder = filter.SortOrder?.ToLower() == "desc" ? "descending" : "ascending";

                // Проверяем существование свойства для сортировки
                var sortProperty = typeof(TEntity).GetProperty(filter.SortBy);
                if (sortProperty != null)
                {
                    try
                    {
                        query = query.OrderBy($"{filter.SortBy} {sortOrder}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"Cannot apply sorting for property {filter.SortBy}: {ex.Message}");
                    }
                }
                else
                {
                    _logger.LogWarning($"Sort property {filter.SortBy} not found");
                }
            }

            var elements = query
                .Skip(startPosition - 1)
                .Take(count);

            return (elements, totalCount);
        });
        }
    }
}
