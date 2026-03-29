using Laba1.Src.Subject;

namespace Laba1.service;

/// <summary>
/// Контроллер для управления объектом <see cref="HousingDepartment"/>.
/// Инкапсулирует логику изменения состояния и обеспечивает централизованный доступ.
/// </summary>
public class HousingDepartmentPresenter : IHousingDepartmentPresenter
{
    /// <summary>
    /// Экземпляр жилищного департамента (Singleton).
    /// </summary>
    private readonly HousingDepartment _department;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера.
    /// </summary>
    public HousingDepartmentPresenter()
    {
        _department = HousingDepartment.Instance;
    }

    /// <summary>
    /// Обновляет район обслуживания.
    /// </summary>
    /// <param name="district">Название района.</param>
    public void UpdateDistrict(string district)
    {
        if (!string.IsNullOrWhiteSpace(district))
            _department.District = district;
    }
    
    /// <summary>
    /// Обновляет номер жилищного департамента.
    /// </summary>
    /// <param name="housingDepartmentNumber">Номер департамента.</param>
    public void UpdateHousingDepartmentNumber(int housingDepartmentNumber)
    {
        _department.HousingDepartmentNumber = housingDepartmentNumber;
    }

    /// <summary>
    /// Обновляет список жильцов.
    /// </summary>
    /// <param name="names">Строка с именами жильцов, разделенными ';'.</param>
    /// <param name="houseNumbers">Строка с номерами домов, разделенными ';'.</param>
    public void UpdateResidents(string names, string houseNumbers)
    {
        if (!string.IsNullOrWhiteSpace(names) && !string.IsNullOrWhiteSpace(houseNumbers))
            _department.Residents = ParseResidents(names, houseNumbers);
    }

    /// <summary>
    /// Обновляет количество оплативших жильцов.
    /// </summary>
    /// <param name="paidResidentsCount">Количество оплативших.</param>
    public void UpdatePaidResidentsCount(int paidResidentsCount)
    {
        _department.PaidResidentsCount = paidResidentsCount;
    }

    /// <summary>
    /// Обновляет тариф.
    /// </summary>
    /// <param name="tariff">Значение тарифа.</param>
    public void UpdateTariff(double tariff)
    {
        _department.Tariff = tariff;
    }

    /// <summary>
    /// Обновляет баланс департамента.
    /// </summary>
    /// <param name="balance">Баланс.</param>
    public void UpdateBalance(decimal balance)
    {
        _department.Balance = balance;
    }

    /// <summary>
    /// Обновляет количество сотрудников.
    /// </summary>
    /// <param name="employeeCount">Количество сотрудников.</param>
    public void UpdateEmployeeCount(int employeeCount)
    {
        _department.EmployeeCount = employeeCount;
    }

    /// <summary>
    /// Возвращает информацию о департаменте.
    /// </summary>
    /// <returns>Строковое представление объекта <see cref="HousingDepartment"/>.</returns>
    public string GetDepartmentInfo()
    {
        return _department.ToString();
    }

    /// <summary>
    /// Преобразует строки с данными жильцов в массив объектов <see cref="Resident"/>.
    /// </summary>
    /// <param name="residentNames">Имена жильцов, разделенные ';'.</param>
    /// <param name="residentNumberHouse">Номера домов, разделенные ';'.</param>
    /// <returns>Массив жильцов.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Выбрасывается, если количество имен не совпадает с количеством номеров домов.
    /// </exception>
    /// <exception cref="FormatException">
    /// Выбрасывается, если номер дома не удалось преобразовать в число.
    /// </exception>
    private Resident[] ParseResidents(string residentNames, string residentNumberHouse)
    {
        string[] splitNames = residentNames.Split(';');
        string[] splitNumber = residentNumberHouse.Split(';');

        if (splitNames.Length != splitNumber.Length)
            throw new IndexOutOfRangeException("Количество имен не совпадает с количеством номеров домов");

        Resident[] residents = new Resident[splitNames.Length];

        for (int i = 0; i < splitNames.Length; i++)
        {
            int num = int.Parse(splitNumber[i]);
            residents[i] = new Resident(num, splitNames[i]);
        }

        return residents;
    }
}
