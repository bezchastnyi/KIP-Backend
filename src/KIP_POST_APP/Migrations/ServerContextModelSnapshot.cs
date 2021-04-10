﻿// <auto-generated />
using System;
using System.Collections.Generic;
using KIP_POST_APP.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KIP_POST_APP.Migrations
{
    [DbContext(typeof(ServerContext))]
    partial class ServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Audience", b =>
                {
                    b.Property<int>("AudienceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AudienceName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("BuildingID")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfSeats")
                        .HasColumnType("integer");

                    b.HasKey("AudienceID");

                    b.HasIndex("BuildingID");

                    b.ToTable("Audience");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Building", b =>
                {
                    b.Property<int>("BuildingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BuildingName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("BuildingShortName")
                        .HasColumnType("varchar(5)");

                    b.HasKey("BuildingID");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Cathedra", b =>
                {
                    b.Property<int>("CathedraID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CathedraName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CathedraShortName")
                        .HasColumnType("varchar(7)");

                    b.Property<int>("FacultyID")
                        .HasColumnType("integer");

                    b.HasKey("CathedraID");

                    b.HasIndex("FacultyID");

                    b.ToTable("Cathedra");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Faculty", b =>
                {
                    b.Property<int>("FacultyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FacultyShortName")
                        .HasColumnType("varchar(7)");

                    b.HasKey("FacultyID");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Group", b =>
                {
                    b.Property<int>("GroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Course")
                        .HasColumnType("integer");

                    b.Property<int>("FacultyID")
                        .HasColumnType("integer");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("ProfScheduleID")
                        .HasColumnType("integer");

                    b.HasKey("GroupID");

                    b.HasIndex("FacultyID");

                    b.HasIndex("ProfScheduleID");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Prof", b =>
                {
                    b.Property<int>("ProfID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CathedraID")
                        .HasColumnType("integer");

                    b.Property<string>("ProfName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProfPatronymic")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProfSurname")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("ProfID");

                    b.HasIndex("CathedraID");

                    b.ToTable("Prof");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.ProfSchedule", b =>
                {
                    b.Property<int>("ProfScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AudienceID")
                        .HasColumnType("integer");

                    b.Property<int?>("BuildingID")
                        .HasColumnType("integer");

                    b.Property<List<Nullable<int>>>("GroupID")
                        .HasColumnType("integer[]");

                    b.Property<int>("ProfID")
                        .HasColumnType("integer");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("day")
                        .HasColumnType("integer");

                    b.Property<int>("week")
                        .HasColumnType("integer");

                    b.HasKey("ProfScheduleID");

                    b.HasIndex("AudienceID");

                    b.HasIndex("BuildingID");

                    b.HasIndex("ProfID");

                    b.ToTable("ProfSchedule");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.StudentSchedule", b =>
                {
                    b.Property<int>("StudentScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AudienceID")
                        .HasColumnType("integer");

                    b.Property<int?>("BuildingID")
                        .HasColumnType("integer");

                    b.Property<int>("GroupID")
                        .HasColumnType("integer");

                    b.Property<int?>("ProfID")
                        .HasColumnType("integer");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("day")
                        .HasColumnType("integer");

                    b.Property<int>("week")
                        .HasColumnType("integer");

                    b.HasKey("StudentScheduleID");

                    b.HasIndex("AudienceID");

                    b.HasIndex("BuildingID");

                    b.HasIndex("GroupID");

                    b.HasIndex("ProfID");

                    b.ToTable("StudentSchedule");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Audience", b =>
                {
                    b.HasOne("KIP_POST_APP.Models.KIP.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Cathedra", b =>
                {
                    b.HasOne("KIP_POST_APP.Models.KIP.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Group", b =>
                {
                    b.HasOne("KIP_POST_APP.Models.KIP.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KIP_POST_APP.Models.KIP.ProfSchedule", null)
                        .WithMany("Group")
                        .HasForeignKey("ProfScheduleID");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.Prof", b =>
                {
                    b.HasOne("KIP_POST_APP.Models.KIP.Cathedra", "Cathedra")
                        .WithMany()
                        .HasForeignKey("CathedraID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cathedra");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.ProfSchedule", b =>
                {
                    b.HasOne("KIP_POST_APP.Models.KIP.Audience", "Audience")
                        .WithMany()
                        .HasForeignKey("AudienceID");

                    b.HasOne("KIP_POST_APP.Models.KIP.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingID");

                    b.HasOne("KIP_POST_APP.Models.KIP.Prof", "Prof")
                        .WithMany()
                        .HasForeignKey("ProfID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audience");

                    b.Navigation("Building");

                    b.Navigation("Prof");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.StudentSchedule", b =>
                {
                    b.HasOne("KIP_POST_APP.Models.KIP.Audience", "Audience")
                        .WithMany()
                        .HasForeignKey("AudienceID");

                    b.HasOne("KIP_POST_APP.Models.KIP.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingID");

                    b.HasOne("KIP_POST_APP.Models.KIP.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KIP_POST_APP.Models.KIP.Prof", "Prof")
                        .WithMany()
                        .HasForeignKey("ProfID");

                    b.Navigation("Audience");

                    b.Navigation("Building");

                    b.Navigation("Group");

                    b.Navigation("Prof");
                });

            modelBuilder.Entity("KIP_POST_APP.Models.KIP.ProfSchedule", b =>
                {
                    b.Navigation("Group");
                });
#pragma warning restore 612, 618
        }
    }
}
