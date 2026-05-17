using System.Globalization;
using Laba1.service;
using Laba1.Src.util;

namespace Laba1.src.view;

/// <summary>
/// Консольное представление с интерактивным редактированием данных (MVP).
/// </summary>
public class ConsoleHousingDepartmentView : IHousingDepartmentView
{
    private readonly IHousingDepartmentPresenter _presenter;

    public ConsoleHousingDepartmentView(IHousingDepartmentPresenter presenter)
    {
        _presenter = presenter;
        Task.Run(Run);
    }

    /// <inheritdoc />
    public void ShowDepartmentInfo(string info)
    {
        Console.WriteLine("=== Console View (MVP) ===");
        Console.WriteLine(info);
    }

    private void Run()
    {
        ConsoleManager.Show();

        string input = string.Empty;
        while (input != "0")
        {
            ShowDepartmentInfo(_presenter.GetDepartmentInfo());
            Console.WriteLine("\n=== Меню ===");
            Console.WriteLine("1. Изменить район");
            Console.WriteLine("2. Изменить номер ЖЭК");
            Console.WriteLine("3. Изменить жильцов");
            Console.WriteLine("4. Изменить количество оплативших");
            Console.WriteLine("5. Изменить тариф");
            Console.WriteLine("6. Изменить баланс");
            Console.WriteLine("7. Изменить количество сотрудников");
            Console.WriteLine("8. Показать информацию");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            input = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            try
            {
                switch (input)
                {
                    case "1":
                        Console.Write("Введите район: ");
                        _presenter.UpdateDistrict(Console.ReadLine() ?? string.Empty);
                        break;
                    case "2":
                        Console.Write("Введите номер ЖЭК: ");
                        _presenter.UpdateHousingDepartmentNumber(ReadInt());
                        break;
                    case "3":
                        Console.Write("Введите имена жильцов через ';': ");
                        string names = Console.ReadLine() ?? string.Empty;
                        Console.Write("Введите номера домов через ';': ");
                        string houses = Console.ReadLine() ?? string.Empty;
                        _presenter.UpdateResidents(names, houses);
                        break;
                    case "4":
                        Console.Write("Введите количество оплативших: ");
                        _presenter.UpdatePaidResidentsCount(ReadInt());
                        break;
                    case "5":
                        Console.Write("Введите тариф: ");
                        _presenter.UpdateTariff(ReadDouble());
                        break;
                    case "6":
                        Console.Write("Введите баланс: ");
                        _presenter.UpdateBalance(ReadDecimal());
                        break;
                    case "7":
                        Console.Write("Введите количество сотрудников: ");
                        _presenter.UpdateEmployeeCount(ReadInt());
                        break;
                    case "8":
                        break;
                    case "0":
                        ConsoleManager.Close();
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    private static int ReadInt()
    {
        if (int.TryParse(Console.ReadLine(), out int result))
        {
            return result;
        }

        throw new FormatException("Ожидалось целое число.");
    }

    private static double ReadDouble()
    {
        if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.CurrentCulture, out double result))
        {
            return result;
        }

        throw new FormatException("Ожидалось число с плавающей точкой.");
    }

    private static decimal ReadDecimal()
    {
        if (decimal.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.CurrentCulture, out decimal result))
        {
            return result;
        }

        throw new FormatException("Ожидалось десятичное число.");
    }
}


