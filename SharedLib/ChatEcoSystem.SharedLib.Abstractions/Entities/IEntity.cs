namespace ChatEcoSystem.SharedLib.Abstractions
{
    /// <summary>
    /// Базовый интерфейс сущностей
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
