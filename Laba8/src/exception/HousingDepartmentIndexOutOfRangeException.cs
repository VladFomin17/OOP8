namespace Laba1.src.exception;

/// <summary>
/// Исключение, возникающее при попытке обращения к элементу
/// за пределами допустимого диапазона индексов.
/// </summary>
public class HousingDepartmentIndexOutOfRangeException : Exception
{
    /// <summary>
    /// Недопустимый индекс, к которому была попытка обращения.
    /// </summary>
    private int InvalidIndex { get; }

    /// <summary>
    /// Максимально допустимый индекс.
    /// </summary>
    private int MaxIndex { get; }

    /// <summary>
    /// Инициализирует новый экземпляр исключения
    /// <see cref="HousingDepartmentIndexOutOfRangeException"/>.
    /// </summary>
    /// <param name="invalidIndex">Индекс, к которому был выполнен доступ.</param>
    /// <param name="maxIndex">Максимально допустимый индекс.</param>
    public HousingDepartmentIndexOutOfRangeException(int invalidIndex, int maxIndex)
        : base("Попытка обращения к элементу за пределами массива")
    {
        InvalidIndex = invalidIndex;
        MaxIndex = maxIndex;
    }

    /// <summary>
    /// Возвращает строковое представление ошибки с подробной информацией.
    /// </summary>
    /// <returns>Сообщение об ошибке с указанием индексов.</returns>
    public override string ToString()
    {
        return $"{Message}\nНедопустимый индекс: {InvalidIndex}\nМаксимальный индекс: {MaxIndex}";
    }
}