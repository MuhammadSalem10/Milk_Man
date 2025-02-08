

namespace MilkMan.Domain;

    public class Payment
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }

public enum PaymentMethod
{
    Cash,
    CreditCard
}