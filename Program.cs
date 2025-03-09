using G5_MovieTicketBookingSystem.Components;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Services;
using G5_MovieTicketBookingSystem.Services.Impl;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;


namespace G5_MovieTicketBookingSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSignalR(); // SignalR hỗ trợ real-time
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAntiforgery();
        builder.Services.AddHttpClient();
 builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
   .AddCookie()
   .AddGoogle(options =>
   {
       options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
       options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
       options.SaveTokens = true;  // 🔥 Cần thiết để lưu token!
       options.CallbackPath = new PathString("/api/auth/login-google-info   ");  // Đảm bảo rằng URL này chính xác
   });

            // Add distributed memory cache for session storage
            builder.Services.AddDistributedMemoryCache();
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


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();  //
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting(); // Required for session and endpoint mapping
            app.UseAntiforgery();
            app.UseSession(); // Enable session middleware

            app.UseSession();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            app.MapControllers();
            app.Run();
        }

    }
}