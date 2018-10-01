﻿// <auto-generated />
using System;
using Exs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exs.Infra.Data.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20180927162309_criando-table-filme-locacao")]
    partial class criandotablefilmelocacao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Exs.Domain.Entities.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("GeneroId");

                    b.Property<int?>("LocacaoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.HasIndex("LocacaoId");

                    b.ToTable("Filme");
                });

            modelBuilder.Entity("Exs.Domain.Entities.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("Exs.Domain.Entities.Locacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.Property<DateTime>("DataLocacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Locacao");
                });

            modelBuilder.Entity("Exs.Domain.Entities.LocacaoFilme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CascadeMode");

                    b.Property<int>("FilmeId")
                        .HasColumnType("int");

                    b.Property<int>("LocacaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LocacaoFilme");
                });

            modelBuilder.Entity("Exs.Domain.Entities.Filme", b =>
                {
                    b.HasOne("Exs.Domain.Entities.Genero", "Genero")
                        .WithMany("Filmes")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exs.Domain.Entities.Locacao")
                        .WithMany("Filmes")
                        .HasForeignKey("LocacaoId");
                });
#pragma warning restore 612, 618
        }
    }
}
