using Laba1.Src.Subject;

namespace Laba1.service;

public class HousingDepartmentMediator : IHousingDepartmentMediator
{
    private readonly HousingDepartment _department;

    public HousingDepartmentMediator()
    {
        _department = HousingDepartment.Instance;
    }

    public void UpdateDistrict(string district)
    {
        if (!string.IsNullOrWhiteSpace(district))
            _department.District = district;
    }
    
    public void UpdateHousingDepartmentNumber(int housingDepartmentNumber)
    {
        _department.HousingDepartmentNumber = housingDepartmentNumber;
    }
    
    public void UpdateResidents(string names, string houseNumbers)
    {
        if (!string.IsNullOrWhiteSpace(names) && !string.IsNullOrWhiteSpace(houseNumbers))
            _department.Residents = ParseResidents(names, houseNumbers);
    }
    
    public void UpdatePaidResidentsCount(int paidResidentsCount)
    {
        _department.PaidResidentsCount = paidResidentsCount;
    }
    
    public void UpdateTariff(double tariff)
    {
        _department.Tariff = tariff;
    }
    
    public void UpdateBalance(decimal balance)
    {
        _department.Balance = balance;
    }
    
    public void UpdateEmployeeCount(int employeeCount)
    {
        _department.EmployeeCount = employeeCount;
    }

    public string GetDepartmentInfo()
    {
        return _department.ToString();
    }
    
    private Resident[] ParseResidents(string residentNames, string residentNumberHouse)
    {
        string[] splitNames = residentNames.Split(';');
        string[] splitNumber = residentNumberHouse.Split(';');
        
        if (splitNames.Length != splitNumber.Length)
            throw new IndexOutOfRangeException();
        
        Resident[] residents = new Resident[splitNames.Length];
        for (int i = 0; i < splitNames.Length; i++)
        {
            int num = int.Parse(splitNumber[i]);
            residents[i] = new Resident(num, splitNames[i]);
        }
        
        return residents;
    }
}
