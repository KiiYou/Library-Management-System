namespace Library_Management_System.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime MembershipDate { get; set; } = DateTime.Now;
        public ICollection<Checkout> Checkouts { get; set; }
    }

}
