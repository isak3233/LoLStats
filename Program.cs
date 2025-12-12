namespace LoLApi
{
    using Dapper;
    using Db;
    using LoLApi.LoL;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Core;
    using Microsoft.Data.SqlClient;
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

            app.UseStaticFiles();
            SetUpHtmlMaps(app);

            app.UseCors("AllowAll");
            app.MapControllers();

            app.MapFallback(() => Results.File("index.html", "text/html"));
            

            app.Run();

        }
        static void SetUpHtmlMaps(WebApplication app)
        {
            var env = app.Environment;
            var wwwroot = Path.Combine(env.WebRootPath);

            var htmlFiles = Directory.GetFiles(wwwroot, "*.html", SearchOption.AllDirectories);

            foreach (var file in htmlFiles)
            {

                var relativePath = Path.GetRelativePath(wwwroot, file).Replace("\\", "/");


                var route = "/" + relativePath.Replace(".html", "");

                if (relativePath == "index.html")
                    route = "/";

                app.MapGet(route, () => Results.File(relativePath, "text/html"));

            }

        }
        
    }

}
//string connectionString = "xxxxx";
//using (var connection = new SqlConnection(connectionString))
//{
//    string[] imageFilesDirectory = Directory.GetFiles("C:\\x\\x\\x\\x");

//    foreach (string file in imageFilesDirectory)
//    {
//        byte[] img = File.ReadAllBytes(file);
//        int imageId = int.Parse(file.Split("\\")[5].Split(".")[0]);
//        Console.WriteLine(imageId);
//        string sql = @"
//                    SET IDENTITY_INSERT ProfileIcon ON;
//                    INSERT INTO ProfileIcon (Id, Image) VALUES (@Id, @Image);
//                    SET IDENTITY_INSERT ProfileIcon OFF;";
//        connection.Execute(sql, new { Id = imageId, Image = img });
//    }
//    Console.WriteLine("Saved to database");

//}
