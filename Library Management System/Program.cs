using Library_Management_System.Data;
using Library_Management_System.Models;
using Library_Management_System.Repositories;
using Library_Management_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<LibraryContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));

            builder.Services.AddScoped<IRepository<Book>, BookRepository>();
            builder.Services.AddScoped<IRepository<Member>, MemberRepository>();
            builder.Services.AddScoped<IRepository<Checkout>, CheckoutRepository>();

            builder.Services.AddScoped<IBookService, BookService>();

            // Add other services and configure MVC


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admins}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
