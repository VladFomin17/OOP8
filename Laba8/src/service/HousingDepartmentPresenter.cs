using Laba1.src.view;
using Laba1.Src.Subject;

namespace Laba1.service;

/// <summary>
/// Презентер для управления объектом <see cref="HousingDepartment"/>.
/// В MVP презентер координирует модель и представления.
/// </summary>
public class HousingDepartmentPresenter : IHousingDepartmentPresenter
{
    /// <summary>
    /// Экземпляр жилищного департамента (Singleton).
    /// </summary>
    private readonly HousingDepartment _department;

    /// <summary>
    /// Зарегистрированные представления.
    /// </summary>
    private readonly List<IHousingDepartmentView> _views = new();

    public HousingDepartmentPresenter()
    {
        _department = HousingDepartment.Instance;
    }

    /// <inheritdoc />
    public void AttachView(IHousingDepartmentView view)
    {
        if (!_views.Contains(view))
        {
            _views.Add(view);
        }
    }

    /// <inheritdoc />
    public void RefreshViews()
    {
        string info = GetDepartmentInfo();

        foreach (IHousingDepartmentView view in _views)
        {
            view.ShowDepartmentInfo(info);
        }
    }

    /// <inheritdoc />
    public void UpdateDistrict(string district)
    {
        if (!string.IsNullOrWhiteSpace(district))
        {
            _department.District = district;
            RefreshViews();
        }
    }

    /// <inheritdoc />
    public void UpdateHousingDepartmentNumber(int housingDepartmentNumber)
    {
        _department.HousingDepartmentNumber = housingDepartmentNumber;
        RefreshViews();
    }

    /// <inheritdoc />
    public void UpdateResidents(string names, string houseNumbers)
    {
        if (!string.IsNullOrWhiteSpace(names) && !string.IsNullOrWhiteSpace(houseNumbers))
        {
            _department.Residents = ParseResidents(names, houseNumbers);
            RefreshViews();
        }
    }

    /// <inheritdoc />
    public void UpdatePaidResidentsCount(int paidResidentsCount)
    {
        _department.PaidResidentsCount = paidResidentsCount;
        RefreshViews();
    }

    /// <inheritdoc />
    public void UpdateTariff(double tariff)
    {
        _department.Tariff = tariff;
        RefreshViews();
    }

    /// <inheritdoc />
    public void UpdateBalance(decimal balance)
    {
        _department.Balance = balance;
        RefreshViews();
    }

    /// <inheritdoc />
    public void UpdateEmployeeCount(int employeeCount)
    {
        _department.EmployeeCount = employeeCount;
        RefreshViews();
    }

    /// <inheritdoc />
    public string GetDepartmentInfo()
    {
        return _department.ToString();
    }

    /// <summary>
    /// Преобразует строки с данными жильцов в массив объектов <see cref="Resident"/>.
    /// </summary>
    private Resident[] ParseResidents(string residentNames, string residentNumberHouse)
    {
        string[] splitNames = residentNames.Split(';');
        string[] splitNumber = residentNumberHouse.Split(';');

        if (splitNames.Length != splitNumber.Length)
        {
            throw new IndexOutOfRangeException("Количество имен не совпадает с количеством номеров домов");
        }

        Resident[] residents = new Resident[splitNames.Length];

        for (int i = 0; i < splitNames.Length; i++)
        {
            int num = int.Parse(splitNumber[i]);
            residents[i] = new Resident(num, splitNames[i]);
        }

        return residents;
    }
}
