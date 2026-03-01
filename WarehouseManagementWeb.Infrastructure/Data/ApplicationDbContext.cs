using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Infrastructure.Identity;

namespace WarehouseManagementWeb.Infrastructure.Data
{
    /// <summary>
    /// Класс контекста Ef Core.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Параметры.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
