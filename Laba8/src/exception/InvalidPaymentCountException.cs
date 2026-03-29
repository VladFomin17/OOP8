namespace Laba1.src.exception;

/// <summary>
/// Исключение, возникающее при некорректном количестве оплативших жильцов,
/// когда число оплат превышает общее количество жильцов.
/// </summary>
public class InvalidPaymentCountException : Exception
{
    /// <summary>
    /// Количество оплативших жильцов.
    /// </summary>
    private int Paid { get; }

    /// <summary>
    /// Общее количество жильцов.
    /// </summary>
    private int Total { get; }

    /// <summary>
    /// Инициализирует новый экземпляр исключения
    /// <see cref="InvalidPaymentCountException"/>.
    /// </summary>
    /// <param name="paid">Количество оплативших жильцов.</param>
    /// <param name="total">Общее количество жильцов.</param>
    public InvalidPaymentCountException(int paid, int total)
        : base("Число оплативших превышает общее число жильцов")
    {
        Paid = paid;
        Total = total;
    }
}