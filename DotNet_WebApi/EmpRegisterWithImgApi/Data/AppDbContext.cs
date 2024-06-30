using EmpRegisterWithImgApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EmpRegisterWithImgApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmpModel> empModels { get; set; }
    }
}
