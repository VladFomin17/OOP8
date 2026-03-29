namespace Laba1.Src.Subject;

/// <summary>
/// Класс, представляющий жильца.
/// </summary>
public class Resident
{
    /// <summary>
    /// Номер дома.
    /// </summary>
    private int NumberHouse { get; set; }

    /// <summary>
    /// Имя жильца.
    /// </summary>
    private string Name { get; set; }

    /// <summary>
    /// Инициализирует нового жильца.
    /// </summary>
    /// <param name="numberHouse">Номер дома.</param>
    /// <param name="name">Имя жильца.</param>
    public Resident(int numberHouse, string name)
    {
        NumberHouse = numberHouse;
        Name = name;
    }

    /// <summary>
    /// Возвращает строковое представление жильца.
    /// </summary>
    /// <returns>Информация о жильце.</returns>
    public override string ToString()
    {
        return $"Имя: {Name} | Номер дома: {NumberHouse}\n";
    }
}