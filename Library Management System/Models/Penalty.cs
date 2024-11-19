namespace Library_Management_System.Models
{
    public class Penalty
    {
        public int PenaltyId { get; set; }
        public int CheckoutId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PenaltyDate { get; set; }

        public Checkout Checkout { get; set; }
    }

}
