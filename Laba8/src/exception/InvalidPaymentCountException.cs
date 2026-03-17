namespace Laba1.src.exception;

public class InvalidPaymentCountException : Exception
{
    private int Paid { get; }
    private int Total { get; }

    public InvalidPaymentCountException(int paid, int total)
        : base("Число оплативших превышает общее число жильцов")
    {
        Paid = paid;
        Total = total;
    }
}