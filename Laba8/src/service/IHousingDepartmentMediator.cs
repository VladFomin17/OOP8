namespace Laba1.service;

public interface IHousingDepartmentMediator
{
    public void UpdateDistrict(string district);
    public void UpdateHousingDepartmentNumber(int housingDepartmentNumber);
    public void UpdateResidents(string names, string houseNumbers);
    public void UpdatePaidResidentsCount(int paidResidentsCount);
    public void UpdateTariff(double tariff);
    public void UpdateBalance(decimal balance);
    public void UpdateEmployeeCount(int employeeCount);
    public string GetDepartmentInfo();
}
