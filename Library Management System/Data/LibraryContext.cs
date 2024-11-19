using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Library_Management_System.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkout>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Checkouts);

            modelBuilder.Entity<Checkout>()
                .HasOne(c => c.Member)
                .WithMany(m => m.Checkouts);

            modelBuilder.Entity<Penalty>()
                .HasOne(p => p.Checkout)
                .WithMany(c => c.Penalties);
        }
    }

}
