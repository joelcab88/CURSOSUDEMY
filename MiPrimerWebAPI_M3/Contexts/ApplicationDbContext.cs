using Microsoft.EntityFrameworkCore;
using MiPrimerWebAPI_M3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebAPI_M3.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
    }
}
