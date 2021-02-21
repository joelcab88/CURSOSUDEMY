using ManupulacionDatos_M5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManupulacionDatos_M5.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>()
                .HasOne(p => p.Autor)
                .WithMany(b => b.Books)
                .HasForeignKey(p => p.AutorId); //iIdAutor es la FK
            //var autores = new List<Autor>()
            //{
            //    new Autor(){Id = 1, Nombre = "Felipe Gavilán" },
            //    new Autor(){Id = 2, Nombre = "Claudia Rodríguez" }
            //};

            //modelBuilder.Entity<Autor>().HasData(autores);

            //var libros = new List<Libro>()
            //{
            //    new Libro(){Id = 1, Titulo = "Entity Framework Core 2.1 - De verdad", AutorId = 1},
            //     new Libro(){Id = 2, Titulo = "Entity Framework Core 3.1 - De verdad", AutorId = 1},
            //    new Libro(){Id = 3, Titulo = "Construyendo Web APIs con ASP.NET Core 2.2", AutorId = 2},
            //    new Libro(){Id = 4, Titulo = "Libro de prueba", AutorId = 2}
            //};

            //modelBuilder.Entity<Libro>().HasData(libros);

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }


        /// <summary>
        /// Método para especificar cual es la columna FK en model Libros.
        /// </summary>
        /// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Libro>()
        //        .HasOne(p => p.Autor)
        //        .WithMany(b => b.Books)
        //        .HasForeignKey(p => p.AutorId); //iIdAutor es la FK
        //}
    }
}
