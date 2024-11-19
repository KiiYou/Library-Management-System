namespace Library_Management_System.Models
{
    public class Checkout
    {
        public int CheckoutId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime CheckoutDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }=false;
        public bool IsPenality { get; set; } = false;

        public ICollection<Penalty> Penalties { get; set; }

        public bool IsOverdue => ReturnDate.HasValue && ReturnDate.Value > DueDate;
    }

}
