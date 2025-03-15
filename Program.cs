using G5_MovieTicketBookingSystem.Components;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Services;
using G5_MovieTicketBookingSystem.Services.Impl;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddSignalR(); // SignalR hỗ trợ real-time
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAntiforgery();
        builder.Services.AddHttpClient();

        // Register the DbContext with SQL Server
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                   .LogTo(Console.WriteLine, LogLevel.Information) // Log SQL để debug
                   .EnableSensitiveDataLogging()); // Hiển thị dữ liệu nhạy cảm trong log

        builder.Services.AddScoped<ICinemaService, CinemaService>();
        builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<ISeatLockService, SeatLockService>();
        builder.Services.AddScoped<IVnPayService, VnPayService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IScreenSeatService, ScreenSeatService>();
        builder.Services.AddScoped<ITransactionLogService, TransactionLogService>();
        builder.Services.AddScoped<IOrderItemService, OrderItemService>();
        builder.Services.AddScoped<ITicketService, TicketService>();

        builder.Services.AddScoped<ISeatLockRepository, SeatLockRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        builder.Services.AddScoped<ITransactionLogRepository, TransactionLogRepository>();
        builder.Services.AddScoped<ITicketRepository, TicketRepository>();
        builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
        builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IScreenSeatRepository, ScreenSeatRepository>();

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapFallbackToFile("pages/404.html");

        app.Run();
    }
}
