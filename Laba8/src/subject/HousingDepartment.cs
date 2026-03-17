using Laba1.src.exception;

namespace Laba1.Src.Subject;

public class HousingDepartment
{
    private static HousingDepartment _instance;

    public static HousingDepartment Instance => _instance ??= new HousingDepartment();
    
    public static int ObjectCount { get; private set; }
    
    public string District { get; set; }
    public int HousingDepartmentNumber { get; set; }
    private Resident[] _residents;

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

    private int _paidResidentsCount;
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

    public double Tariff { get; set; }
    public decimal Balance { get; set; }
    public int EmployeeCount { get; set; }

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

    public string GetInfoAboutResidents()
    {
        string info = "";
        foreach (Resident resident in Residents)
        {
            info += resident.ToString();
        }

        return info;
    }

    public string GetHousingDepartmentNumberHex()
    {
        return HousingDepartmentNumber.ToString("X");
    }

    public string GetResidentInfoByIndex(int index)
    {
        if (index < 0 || index >= _residents.Length)
        {
            throw new HousingDepartmentIndexOutOfRangeException(index, _residents.Length);
        }
        return _residents[index].ToString();
    }
}