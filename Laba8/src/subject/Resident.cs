namespace Laba1.Src.Subject;

public class Resident
{
    private int NumberHouse { get; set; }
    private string Name { get; set; }

    public Resident(int numberHouse, string name)
    {
        NumberHouse = numberHouse;
        Name = name;
    }

    public override string ToString()
    {
        return $"Имя: {Name} | Номер дома: {NumberHouse}\n";
    }
}