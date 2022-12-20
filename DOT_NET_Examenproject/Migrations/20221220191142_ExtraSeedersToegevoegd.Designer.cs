﻿// <auto-generated />
using DOT_NET_Examenproject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DOTNETExamenproject.Migrations
{
    [DbContext(typeof(DOT_NET_ExamenprojectContext))]
    [Migration("20221220191142_ExtraSeedersToegevoegd")]
    partial class ExtraSeedersToegevoegd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DOT_NET_Examenproject.Models.Bedrijf", b =>
                {
                    b.Property<int>("BedrijfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BedrijfId"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NrTel")
                        .HasColumnType("int");

                    b.Property<int>("NrTva")
                        .HasColumnType("int");

                    b.HasKey("BedrijfId");

                    b.ToTable("Bedrijf");
                });

            modelBuilder.Entity("DOT_NET_Examenproject.Models.Klant", b =>
                {
                    b.Property<int>("KlantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KlantId"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NrTel")
                        .HasColumnType("int");

                    b.Property<int>("NrTva")
                        .HasColumnType("int");

                    b.HasKey("KlantId");

                    b.ToTable("Klant");
                });

            modelBuilder.Entity("DOT_NET_Examenproject.Models.Offerte", b =>
                {
                    b.Property<int>("OfferteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfferteId"));

                    b.Property<int>("BedrijfId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("KlantId")
                        .HasColumnType("int");

                    b.Property<string>("TitelOfferte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotaalBedrag")
                        .HasColumnType("real");

                    b.HasKey("OfferteId");

                    b.HasIndex("BedrijfId");

                    b.HasIndex("KlantId");

                    b.ToTable("Offerte");
                });

            modelBuilder.Entity("DOT_NET_Examenproject.Models.Offerte", b =>
                {
                    b.HasOne("DOT_NET_Examenproject.Models.Bedrijf", "Bedrijf")
                        .WithMany()
                        .HasForeignKey("BedrijfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DOT_NET_Examenproject.Models.Klant", "Klant")
                        .WithMany()
                        .HasForeignKey("KlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bedrijf");

                    b.Navigation("Klant");
                });
#pragma warning restore 612, 618
        }
    }
}
