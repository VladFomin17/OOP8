namespace Laba1.src.exception;

public class HousingDepartmentIndexOutOfRangeException : Exception
{
    private int InvalidIndex { get; }
    private int MaxIndex { get; }

    public HousingDepartmentIndexOutOfRangeException(int invalidIndex, int maxIndex)
        : base("Попытка обращения к элементу за пределами массива")
    {
        InvalidIndex = invalidIndex;
        MaxIndex = maxIndex;
    }

    public override string ToString()
    {
        return $"{Message}\nНедопустимый индекс: {InvalidIndex}\nМаксимальный индекс: {MaxIndex}";
    }
}