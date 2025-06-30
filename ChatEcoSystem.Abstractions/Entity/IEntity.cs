namespace ChatEcoSystem.Abstractions
{
    /// <summary>
    /// Базовая модель сущности
    /// </summary>
    /// <typeparam name="TKeyType">Тип ИД</typeparam>
    public interface IEntity<TKeyType>
    {
        /// <summary>
        /// ИД
        /// </summary>
        TKeyType Id { get; set; }
    }
}
