using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ServicioAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ServicioAPI.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<tb_DetalleBoleta> tb_DetalleBoleta { get; set; }


    }
}
