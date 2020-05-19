﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.ApplicationDbContext;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommonLayer.Models.CollaboratorInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("NoteID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("CommonLayer.Models.LabelInfo", b =>
                {
                    b.Property<int>("LabelID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("LabelName")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("UserID");

                    b.HasKey("LabelID");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("CommonLayer.Models.NotesLabel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("LabelId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("NotesId");

                    b.HasKey("Id");

                    b.HasIndex("LabelId");

                    b.HasIndex("NotesId");

                    b.ToTable("NotesLabels");
                });

            modelBuilder.Entity("CommonLayer.Models.UserInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("ProfilePic");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommonLayer.Models.UserNotesInfo", b =>
                {
                    b.Property<int>("NotesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Archived");

                    b.Property<string>("Color");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<bool>("Pin");

                    b.Property<DateTime?>("Reminder");

                    b.Property<string>("Title");

                    b.Property<bool>("Trash");

                    b.Property<int>("UserId");

                    b.HasKey("NotesId");

                    b.ToTable("UserNotes");
                });

            modelBuilder.Entity("CommonLayer.Models.NotesLabel", b =>
                {
                    b.HasOne("CommonLayer.Models.LabelInfo")
                        .WithMany("Notes")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CommonLayer.Models.UserNotesInfo")
                        .WithMany("Labels")
                        .HasForeignKey("NotesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
