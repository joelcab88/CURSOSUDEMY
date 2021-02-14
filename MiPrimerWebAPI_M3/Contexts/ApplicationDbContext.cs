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
        public DbSet<Libro> Libros { get; set; }

        /// <summary>
        /// Método para especificar cual es la columna FK en model Libros.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>()
                .HasOne(p => p.Autor)
                .WithMany(b => b.lstLibros)
                .HasForeignKey(p => p.iIdAutor); //iIdAutor es la FK
        }
    }
}
