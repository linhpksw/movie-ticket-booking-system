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
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSignalR(); // Support for real-time features
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAntiforgery();
            builder.Services.AddHttpClient("EmailClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7000");
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<EmailSender>();
            // Add distributed memory cache for session storage
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7000") });

            // Register session service
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "G5MovieSession"; // Tên cookie của Session
                options.IdleTimeout = TimeSpan.FromMinutes(60); // Thời gian tồn tại: 60 phút
                options.Cookie.HttpOnly = true; // Bảo mật: Chỉ cho phép truy cập qua HTTP
                options.Cookie.IsEssential = true; // Đánh dấu cookie là thiết yếu (bắt buộc cho GDPR)
            });

            // Cấu hình xác thực Google
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "G5MovieAuthCookie";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
            })
            .AddGoogle(options =>
            {
                var clientId = builder.Configuration["Authentication:Google:client_id"];
                var clientSecret = builder.Configuration["Authentication:Google:client_secret"];

                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
                options.SaveTokens = true; // Lưu token!
                options.CallbackPath = new PathString("/auth/google-response"); // Khớp với endpoint trong AuthController
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
                options.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
            });

            // Register the DbContext with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .LogTo(Console.WriteLine, LogLevel.Information) // Log SQL để debug
                    .EnableSensitiveDataLogging()); // Hiển thị dữ liệu nhạy cảm trong log

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
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession(); // Gọi một lần duy nhất
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            app.MapBlazorHub();
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("pages/404.html");

            app.Run();
        }
    }
}
