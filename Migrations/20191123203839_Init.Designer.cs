﻿// <auto-generated />
using System;
using BE.Example.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BE.Example.Migrations
{
    [DbContext(typeof(ExampleDBContext))]
    [Migration("20191123203839_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BE.Example.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(4);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("CountryId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryId = 1,
                            Code = "AR",
                            Name = "Argentina"
                        },
                        new
                        {
                            CountryId = 2,
                            Code = "BR",
                            Name = "Brazil"
                        },
                        new
                        {
                            CountryId = 3,
                            Code = "MX",
                            Name = "Mexico"
                        });
                });

            modelBuilder.Entity("BE.Example.Models.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LanguageId = 1,
                            Code = "es",
                            Name = "Spanish; Castilian"
                        });
                });

            modelBuilder.Entity("BE.Example.Models.Literal", b =>
                {
                    b.Property<int>("LiteralId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(256);

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("ExampleURL");

                    b.Property<int>("ModuleId");

                    b.Property<bool>("Plural");

                    b.HasKey("LiteralId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Literals");

                    b.HasData(
                        new
                        {
                            LiteralId = 1,
                            Code = "login_button",
                            Description = "The label of the login button",
                            ExampleURL = "/login#login_button",
                            ModuleId = 1,
                            Plural = false
                        });
                });

            modelBuilder.Entity("BE.Example.Models.LiteralTranslation", b =>
                {
                    b.Property<int>("LiteralTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId");

                    b.Property<bool>("InReview");

                    b.Property<int>("LanguageId");

                    b.Property<int>("LiteralId");

                    b.Property<string>("ValueMany");

                    b.Property<string>("ValueOne");

                    b.Property<string>("ValueZero");

                    b.HasKey("LiteralTranslationId");

                    b.HasIndex("CountryId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LiteralId");

                    b.ToTable("LiteralTranslations");

                    b.HasData(
                        new
                        {
                            LiteralTranslationId = 1,
                            InReview = false,
                            LanguageId = 1,
                            LiteralId = 1,
                            ValueMany = "Se encontraron %quantity resultados.",
                            ValueOne = "Se encontró un resultado",
                            ValueZero = "No se encontraron resultados."
                        });
                });

            modelBuilder.Entity("BE.Example.Models.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("ModuleId");

                    b.ToTable("Modules");

                    b.HasData(
                        new
                        {
                            ModuleId = 1,
                            Name = "Login & Registration"
                        });
                });

            modelBuilder.Entity("BE.Example.Models.Variable", b =>
                {
                    b.Property<int>("VariableId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LiteralId");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("VariableId");

                    b.HasIndex("LiteralId");

                    b.ToTable("Variables");

                    b.HasData(
                        new
                        {
                            VariableId = 1,
                            LiteralId = 1,
                            Name = "%quantity"
                        });
                });

            modelBuilder.Entity("BE.Example.Models.Literal", b =>
                {
                    b.HasOne("BE.Example.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BE.Example.Models.LiteralTranslation", b =>
                {
                    b.HasOne("BE.Example.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("BE.Example.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BE.Example.Models.Literal", "Literal")
                        .WithMany()
                        .HasForeignKey("LiteralId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BE.Example.Models.Variable", b =>
                {
                    b.HasOne("BE.Example.Models.Literal", "Literal")
                        .WithMany()
                        .HasForeignKey("LiteralId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
