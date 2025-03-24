using G5_MovieTicketBookingSystem.Components;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Services;
using G5_MovieTicketBookingSystem.Services.Impl;
using G5_MovieTicketBookingSystem.Util;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSignalR(); // Real-time
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAntiforgery();
            builder.Services.AddHttpClient("EmailClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7000");
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<EmailSender>();

            // Cache for session
            builder.Services.AddDistributedMemoryCache();

            // Named HttpClient
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7000") });

            // Auth & Authz
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "auth-token";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/access-denied";
                });
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();

            // Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                       .LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableSensitiveDataLogging());

            // Register services
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ISeatLockService, SeatLockService>();
            builder.Services.AddScoped<IVnPayService, VnPayService>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IScreenSeatService, ScreenSeatService>();
            builder.Services.AddScoped<ITransactionLogService, TransactionLogService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            // Register repositories
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
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            var app = builder.Build();

            // Middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

           
            app.MapControllers();
            app.MapFallbackToFile("pages/404.html");

            app.Run();
        }
    }
}
