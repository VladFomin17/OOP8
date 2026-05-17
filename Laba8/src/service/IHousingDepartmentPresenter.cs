using Laba1.src.view;

namespace Laba1.service;

/// <summary>
/// Интерфейс презентера, управляющего сценарием работы представлений.
/// </summary>
public interface IHousingDepartmentPresenter
{
    /// <summary>
    /// Инициализирует представление начальными данными и состоянием.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Подключает дополнительное представление к презентеру.
    /// </summary>
    void AttachView(IHousingDepartmentView view);

    /// <summary>
    /// Возвращает строку с текущими данными департамента.
    /// </summary>
    string GetDepartmentInfo();

    void UpdateDistrict(string district);
    void UpdateHousingDepartmentNumber(int housingDepartmentNumber);
    void UpdateResidents(string names, string houseNumbers);
    void UpdatePaidResidentsCount(int paidResidentsCount);
    void UpdateTariff(double tariff);
    void UpdateBalance(decimal balance);
    void UpdateEmployeeCount(int employeeCount);
}
