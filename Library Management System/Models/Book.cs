namespace Library_Management_System.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Checkout> Checkouts { get; set; }
    }

}
