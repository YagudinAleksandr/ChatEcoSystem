namespace ChatEcoSystem.SharedLib.Abstractions
{
    /// <summary>
    /// Базовый фильтр
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// Номер страницы (для пагинации)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Размер страницы (количество элементов на странице)
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Направление сортировки (asc/desc)
        /// </summary>
        public string SortDirection { get; set; } = "asc";

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string SearchTerm { get; set; }
    }
}
