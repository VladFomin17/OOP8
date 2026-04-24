namespace Laba1.src.view;

/// <summary>
/// Простейшее консольное представление для демонстрации MVP.
/// </summary>
public class ConsoleHousingDepartmentView : IHousingDepartmentView
{
    /// <inheritdoc />
    public void ShowDepartmentInfo(string info)
    {
        Console.WriteLine("=== Console View (MVP) ===");
        Console.WriteLine(info);
    }
}
