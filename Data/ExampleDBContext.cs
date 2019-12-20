using BE.Example.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.Example.ViewModels;

namespace BE.Example.Data
{
    public class ExampleDBContext : DbContext
    {
        public ExampleDBContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Module>().HasData(
                new Module
                {
                    ModuleId = 1, //Startup.GuidId,
                    Name = "Login & Registration"
                }
            );

            builder.Entity<Country>().HasData(
                new Country
                {
                    CountryId = 1,
                    Code = "AR",
                    Name = "Argentina"
                },
                new Country
                {
                    CountryId = 2,
                    Code = "BR",
                    Name = "Brazil"
                },
                new Country
                {
                    CountryId = 3,
                    Code = "MX",
                    Name = "Mexico"
                }
            );

            builder.Entity<Language>().HasData(
                new Language
                {
                    LanguageId = 1,
                    Code = "es",
                    Name = "Spanish; Castilian"
                }
            );

            builder.Entity<Literal>().HasData(
                new Literal
                {
                    LiteralId = 1,
                    ModuleId = 1, //Startup.GuidId,
                    Code = "login_button",
                    Description = "The label of the login button",
                    ExampleURL = "/login#login_button"
                }
            );

            builder.Entity<Variable>().HasData(
                new Variable
                {
                    VariableId = 1,
                    Name = "%quantity",
                    LiteralId = 1
                }
            );

            builder.Entity<LiteralTranslation>().HasData(
                new LiteralTranslation
                {
                    LiteralTranslationId = 1,
                    LiteralId = 1,
                    CountryId = null,
                    LanguageId = 1,
                    ValueZero = "No se encontraron resultados.", //0,
                    ValueOne = "Se encontró un resultado", //1,
                    ValueMany = "Se encontraron %quantity resultados." //2 
                }
            );
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Literal> Literals { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<LiteralTranslation> LiteralTranslations { get; set; }
    }
}
