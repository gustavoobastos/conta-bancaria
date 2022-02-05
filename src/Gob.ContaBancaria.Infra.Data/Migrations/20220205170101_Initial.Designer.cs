﻿// <auto-generated />
using System;
using Gob.ContaBancaria.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gob.ContaBancaria.Infra.Data.Migrations
{
    [DbContext(typeof(ContaBancariaDbContext))]
    [Migration("20220205170101_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Gob.ContaBancaria.Domain.Models.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTitular")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdTitular");

                    b.ToTable("Conta", (string)null);
                });

            modelBuilder.Entity("Gob.ContaBancaria.Domain.Models.Lancamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdConta")
                        .HasColumnType("int");

                    b.Property<int?>("IdContaOrigem")
                        .HasColumnType("int");

                    b.Property<Guid>("IdTransacao")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TipoLancamento")
                        .HasColumnType("int");

                    b.Property<int>("TipoOperacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdConta");

                    b.HasIndex("IdContaOrigem");

                    b.HasIndex("TipoLancamento");

                    b.ToTable("Lancamento", (string)null);
                });

            modelBuilder.Entity("Gob.ContaBancaria.Domain.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Pessoa", (string)null);
                });

            modelBuilder.Entity("Gob.ContaBancaria.Domain.Models.Conta", b =>
                {
                    b.HasOne("Gob.ContaBancaria.Domain.Models.Pessoa", "Titular")
                        .WithMany("Contas")
                        .HasForeignKey("IdTitular")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Titular");
                });

            modelBuilder.Entity("Gob.ContaBancaria.Domain.Models.Lancamento", b =>
                {
                    b.HasOne("Gob.ContaBancaria.Domain.Models.Conta", "Conta")
                        .WithMany("Lancamentos")
                        .HasForeignKey("IdConta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gob.ContaBancaria.Domain.Models.Conta", "ContaOrigem")
                        .WithMany()
                        .HasForeignKey("IdContaOrigem");

                    b.Navigation("Conta");

                    b.Navigation("ContaOrigem");
                });

            modelBuilder.Entity("Gob.ContaBancaria.Domain.Models.Conta", b =>
                {
                    b.Navigation("Lancamentos");
                });

            modelBuilder.Entity("Gob.ContaBancaria.Domain.Models.Pessoa", b =>
                {
                    b.Navigation("Contas");
                });
#pragma warning restore 612, 618
        }
    }
}
