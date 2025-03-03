using G5_MovieTicketBookingSystem.Components;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Services;
using G5_MovieTicketBookingSystem.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using G5_MovieTicketBookingSystem.Hubs;

namespace G5_MovieTicketBookingSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Cấu hình Razor Components & Blazor Server
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSignalR(); // SignalR hỗ trợ real-time
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAntiforgery();
        builder.Services.AddHttpClient();

        // Cấu hình Database Context (SQL Server)
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                   .LogTo(Console.WriteLine, LogLevel.Information) // Log SQL để debug
                   .EnableSensitiveDataLogging()); // Hiển thị dữ liệu nhạy cảm trong log

        // Đăng ký Repository (Data Access Layer)
        builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
        builder.Services.AddScoped<ISeatLockRepository, SeatLockRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IScreenSeatRepository, ScreenSeatRepository>();
        builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

        // Đăng ký Service (Business Logic Layer)
        builder.Services.AddScoped<ICinemaService, CinemaService>();
        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<ISeatLockService, SeatLockService>();
        builder.Services.AddScoped<IVnPayService, VnPayService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IScreenSeatService, ScreenSeatService>();

        // Đăng ký thêm IOrderItemService và OrderItemService
        builder.Services.AddScoped<IOrderItemService, OrderItemService>();

        var app = builder.Build();

        // Cấu hình Middleware
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseAntiforgery(); // Bảo vệ CSRF

        // Cấu hình Endpoint
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapRazorPages();
        app.MapBlazorHub();
        app.MapHub<CountdownHub>("/countdownHub"); // SignalR Hub

        app.Run();
    }
}
