using ToDoAPI.Contracts;
using ToDoAPI.DataAccess;
using ToDoAPI.Services;

namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddScoped<ToDoDbContext>();
            builder.Services.AddScoped<IToDoService, ToDoService>();

            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithOrigins("http://localhost:3000");
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateAsyncScope())
            {
                using var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseCors();
            app.MapControllers();
            app.Run();
        }
    }
}