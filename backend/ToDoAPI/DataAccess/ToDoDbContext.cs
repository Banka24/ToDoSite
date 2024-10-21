using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using ToDoAPI.Models.Entities;

namespace ToDoAPI.DataAccess
{
    public class ToDoDbContext(IConfiguration configuration, ILogger<ToDoDbContext> logger) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<ToDoDbContext> _logger = logger;

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<User> Users { get; set; }

        public async Task<bool> TrySaveChangesAsync(CancellationToken token)
        {
            try
            {
                await SaveChangesAsync(token);
            }
            catch (DBConcurrencyException ex)
            {
                _logger.LogError($"Произошла ошибка! {ex.Message}");
                return false;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Произошла ошибка! {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Произошла ошибка! {ex.Message}");
                return false;
            }
            return true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }
    }
}