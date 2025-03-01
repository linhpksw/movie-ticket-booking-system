using G5_MovieTicketBookingSystem.Components;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Services;
using G5_MovieTicketBookingSystem.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using G5_MovieTicketBookingSystem.Hubs;

namespace G5_MovieTicketBookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSignalR();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAntiforgery();
            builder.Services.AddHttpClient();


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
            builder.Services.AddScoped<ISeatLockRepository, SeatLockRepository>();
            builder.Services.AddScoped<ISeatLockService, SeatLockService>();
            builder.Services.AddScoped<IVnPayService, VnPayService>();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();
        

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapHub<CountdownHub>("/countdownHub");

            app.Run();
        }
    }
}
