using G5_MovieTicketBookingSystem.Components;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Services;
using G5_MovieTicketBookingSystem.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
            builder.Services.AddHttpClient();
            builder.Services.AddSession();
            // Add distributed memory cache for session storage
            builder.Services.AddDistributedMemoryCache();

            // Đăng ký dịch vụ Session
            //builder.Services.AddSession(cfg =>
            //{
            //    cfg.Cookie.Name = "G5"; // Tên cookie của Session
            //    cfg.IdleTimeout = new TimeSpan(0, 60, 0); // Thời gian tồn tại của Session: 60 phút
            //    cfg.Cookie.HttpOnly = true; // Bảo mật: Chỉ cho phép truy cập cookie qua HTTP
            //    cfg.Cookie.IsEssential = true; // Đánh dấu cookie là thiết yếu (bắt buộc cho GDPR)
            //});

            //builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories and services
            builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IUserServices, UserServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting(); // Required for session and endpoint mapping
            app.UseAntiforgery();
            app.UseSession(); // Enable session middleware
            app.UseSession();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}