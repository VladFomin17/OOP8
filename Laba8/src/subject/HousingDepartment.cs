using Laba1.src.exception;

namespace Laba1.Src.Subject;

/// <summary>
/// Класс, представляющий жилищный департамент (ЖЭК).
/// Реализует паттерн Singleton.
/// </summary>
public class HousingDepartment
{
    /// <summary>
    /// Единственный экземпляр класса (Singleton).
    /// </summary>
    private static HousingDepartment _instance;

    /// <summary>
    /// Получает экземпляр <see cref="HousingDepartment"/>.
    /// </summary>
    public static HousingDepartment Instance => _instance ??= new HousingDepartment();

    /// <summary>
    /// Количество созданных объектов класса.
    /// </summary>
    public static int ObjectCount { get; private set; }

    /// <summary>
    /// Район обслуживания.
    /// </summary>
    public string District { get; set; }

    /// <summary>
    /// Номер жилищного департамента.
    /// </summary>
    public int HousingDepartmentNumber { get; set; }

    /// <summary>
    /// Массив жильцов.
    /// </summary>
    private Resident[] _residents;

    /// <summary>
    /// Получает или задаёт список жильцов.
    /// </summary>
    /// <exception cref="InvalidPaymentCountException">
    /// Выбрасывается, если количество оплативших превышает количество жильцов.
    /// </exception>
    public Resident[] Residents
    {
        get => _residents;
        set
        {
            if (_paidResidentsCount > value.Length)
                throw new InvalidPaymentCountException(_paidResidentsCount, value.Length);

            _residents = value;
        }
    }

    /// <summary>
    /// Количество оплативших жильцов.
    /// </summary>
    private int _paidResidentsCount;

    /// <summary>
    /// Получает или задаёт количество оплативших жильцов.
    /// </summary>
    /// <exception cref="InvalidPaymentCountException">
    /// Выбрасывается, если значение превышает общее количество жильцов.
    /// </exception>
    public int PaidResidentsCount
    {
        get => _paidResidentsCount;
        set
        {
            if (value > _residents.Length)
                throw new InvalidPaymentCountException(value, _residents.Length);

            _paidResidentsCount = value;
        }
    }

    /// <summary>
    /// Тариф оплаты.
    /// </summary>
    public double Tariff { get; set; }

    /// <summary>
    /// Баланс департамента.
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Количество сотрудников.
    /// </summary>
    public int EmployeeCount { get; set; }

    /// <summary>
    /// Закрытый конструктор (Singleton).
    /// Инициализирует значения по умолчанию.
    /// </summary>
    private HousingDepartment()
    {
        District = "Не указан";
        HousingDepartmentNumber = 0;
        Residents = new Resident[0];
        PaidResidentsCount = 0;
        Tariff = 0;
        Balance = 0;
        EmployeeCount = 0;

        ObjectCount++;
    }

    /// <summary>
    /// Возвращает строковое представление департамента.
    /// </summary>
    /// <returns>Информация о департаменте.</returns>
    public override string ToString()
    {
        return
            $"Район: {District}\n" +
            $"Номер ЖЭК: {HousingDepartmentNumber}\n" +
            $"Номер ЖЭК HEX: {GetHousingDepartmentNumberHex()}\n" +
            $"Количество жильцов: {Residents.Length}\n" +
            $"Количество оплативших жильцов: {PaidResidentsCount}\n" +
            $"Тариф: {Tariff}\n" +
            $"Баланс: {Balance}\n" +
            $"Количество работников: {EmployeeCount}\n" +
            $"Количество созданных объектов: {ObjectCount}\n";
    }

    /// <summary>
    /// Возвращает информацию о всех жильцах.
    /// </summary>
    /// <returns>Строка с информацией о жильцах.</returns>
    public string GetInfoAboutResidents()
    {
        string info = "";

        foreach (Resident resident in Residents)
        {
            info += resident.ToString();
        }

        return info;
    }

    /// <summary>
    /// Возвращает номер департамента в шестнадцатеричном формате.
    /// </summary>
    /// <returns>HEX-представление номера.</returns>
    public string GetHousingDepartmentNumberHex()
    {
        return HousingDepartmentNumber.ToString("X");
    }

    /// <summary>
    /// Возвращает информацию о жильце по индексу.
    /// </summary>
    /// <param name="index">Индекс жильца.</param>
    /// <returns>Информация о жильце.</returns>
    /// <exception cref="HousingDepartmentIndexOutOfRangeException">
    /// Выбрасывается, если индекс вне диапазона.
    /// </exception>
    public string GetResidentInfoByIndex(int index)
    {
        if (index < 0 || index >= _residents.Length)
        {
            throw new HousingDepartmentIndexOutOfRangeException(index, _residents.Length);
        }

        return _residents[index].ToString();
    }
}