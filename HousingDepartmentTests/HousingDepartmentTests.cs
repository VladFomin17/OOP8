using Laba1.src.exception;
using Laba1.Src.Subject;

[TestClass]
public sealed class HousingDepartmentTests
{
    private HousingDepartment housingDepartment;
    
    [TestMethod]
    public void ZeroParamConstructorTest()
    {
        housingDepartment = HousingDepartment.Instance;
        Assert.AreEqual(0, housingDepartment.EmployeeCount);
    }
    
    [TestMethod]
    public void OneParamConstructorTest()
    {
        string district = "Пензенский";
        housingDepartment = HousingDepartment.Instance;
        housingDepartment.District = district;
        Assert.AreEqual(district, housingDepartment.District);
    }
    
    [TestMethod]
    public void TwoParamsConstructorTest()
    {
        string district = "Пензенский";
        int housingDepartmentNumber = 34;
        housingDepartment = HousingDepartment.Instance;
        housingDepartment.District = district;
        housingDepartment.HousingDepartmentNumber = housingDepartmentNumber;
        Assert.AreEqual(housingDepartmentNumber, housingDepartment.HousingDepartmentNumber);
    }

    [TestMethod]
    public void AllParamsConstructorTest()
    {
        string district = "Пензенский";
        int housingDepartmentNumber = 34;
        Resident[] residents = new Resident[2];
        residents[0] = new Resident(1, "A");
        residents[0] = new Resident(2, "B");
        int paidResidentsCount = 1;
        double tariff = 12.5;
        decimal balance = 341;
        int employeeCount = 12;
        housingDepartment = HousingDepartment.Instance;
        housingDepartment.District = district;
        housingDepartment.HousingDepartmentNumber = housingDepartmentNumber;
        housingDepartment.Residents = residents;
        housingDepartment.PaidResidentsCount = paidResidentsCount;
        housingDepartment.Tariff = tariff;
        housingDepartment.Balance = balance;
        housingDepartment.EmployeeCount = employeeCount;
        
        Assert.AreEqual(residents, housingDepartment.Residents);
        Assert.AreEqual(paidResidentsCount, housingDepartment.PaidResidentsCount);
        Assert.AreEqual(tariff, housingDepartment.Tariff);
        Assert.AreEqual(balance, housingDepartment.Balance);
        Assert.AreEqual(employeeCount, housingDepartment.EmployeeCount);
    }
    
    [TestMethod]
    public void ObjectCountTest()
    {
        var housingDepartment1 = HousingDepartment.Instance;
        var housingDepartment2 = HousingDepartment.Instance;
        Assert.AreEqual(1, HousingDepartment.ObjectCount);
    }

    [TestMethod]
    public void GetHousingDepartmentNumberHexTest()
    {
        string district = "Пензенский";
        int housingDepartmentNumber = 34;
        housingDepartment = HousingDepartment.Instance;
        housingDepartment.District = district;
        housingDepartment.HousingDepartmentNumber = housingDepartmentNumber;
        Assert.AreEqual(housingDepartmentNumber.ToString("X"), housingDepartment.GetHousingDepartmentNumberHex());
    }

    [TestMethod]
    [ExpectedException(typeof(HousingDepartmentIndexOutOfRangeException))]
    public void GetResidentInfoByIndexExceptionTest()
    {
        string district = "Пензенский";
        int housingDepartmentNumber = 34;
        Resident[] residents = new Resident[2];
        residents[0] = new Resident(1, "A");
        residents[0] = new Resident(2, "B");
        int paidResidentsCount = 1;
        double tariff = 12.5;
        decimal balance = 341;
        int employeeCount = 12;
        
        housingDepartment = HousingDepartment.Instance;
        housingDepartment.District = district;
        housingDepartment.HousingDepartmentNumber = housingDepartmentNumber;
        housingDepartment.Residents = residents;
        housingDepartment.PaidResidentsCount = paidResidentsCount;
        housingDepartment.Tariff = tariff;
        housingDepartment.Balance = balance;
        housingDepartment.EmployeeCount = employeeCount;

        housingDepartment.GetResidentInfoByIndex(12);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidPaymentCountException))]
    public void InvalidPaymentCountExceptionTest()
    {
        string district = "Пензенский";
        int housingDepartmentNumber = 34;
        Resident[] residents = new Resident[2];
        residents[0] = new Resident(1, "A");
        residents[0] = new Resident(2, "B");
        int paidResidentsCount = 34;
        double tariff = 12.5;
        decimal balance = 341;
        int employeeCount = 12;


        housingDepartment = HousingDepartment.Instance;
        housingDepartment.District = district;
        housingDepartment.HousingDepartmentNumber = housingDepartmentNumber;
        housingDepartment.Residents = residents;
        housingDepartment.PaidResidentsCount = paidResidentsCount;
        housingDepartment.Tariff = tariff;
        housingDepartment.Balance = balance;
        housingDepartment.EmployeeCount = employeeCount;
    }
}