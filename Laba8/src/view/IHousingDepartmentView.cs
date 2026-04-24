namespace Laba1.src.view;

/// <summary>
/// Интерфейс представления для вывода информации о жилищном департаменте.
/// </summary>
public interface IHousingDepartmentView
{
    /// <summary>
    /// Отображает информацию о департаменте.
    /// </summary>
    /// <param name="info">Строковое представление модели.</param>
    void ShowDepartmentInfo(string info);
}
