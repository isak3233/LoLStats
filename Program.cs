namespace LoLApi
{
    using Db;
    using LoLApi.LoL;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    class Program
    {
        
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });


            builder.Services.AddScoped<LoLService>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            app.UseCors("AllowAll");
            app.MapControllers();
            

            app.Run();

        }
        
    }

}
