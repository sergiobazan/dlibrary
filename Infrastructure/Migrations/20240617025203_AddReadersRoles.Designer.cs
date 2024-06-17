﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240617025203_AddReadersRoles")]
    partial class AddReadersRoles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("publish_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_books_category_id");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_categories_id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Domain.Loans.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("borrow_date");

                    b.Property<Guid>("ReaderId")
                        .HasColumnType("uuid")
                        .HasColumnName("reader_id");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("return_date");

                    b.HasKey("Id")
                        .HasName("pk_loans");

                    b.HasIndex("ReaderId")
                        .HasDatabaseName("ix_loans_reader_id");

                    b.ToTable("loans", (string)null);
                });

            modelBuilder.Entity("Domain.Reader.Reader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.HasKey("Id")
                        .HasName("pk_readers");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_readers_email");

                    b.ToTable("readers", (string)null);
                });

            modelBuilder.Entity("Domain.Reader.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_roles_id");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0a1a389c-2b6a-411e-a969-3fa836079efc"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("a314f90a-291d-45ff-827b-17ce4fd4dfff"),
                            Name = "Reader"
                        });
                });

            modelBuilder.Entity("Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("process_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_message");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_outbox_message_id");

                    b.ToTable("outbox_message", (string)null);
                });

            modelBuilder.Entity("ReaderRole", b =>
                {
                    b.Property<Guid>("ReadersId")
                        .HasColumnType("uuid")
                        .HasColumnName("readers_id");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid")
                        .HasColumnName("roles_id");

                    b.HasKey("ReadersId", "RolesId")
                        .HasName("pk_reader_role");

                    b.HasIndex("RolesId")
                        .HasDatabaseName("ix_reader_role_roles_id");

                    b.ToTable("reader_role", (string)null);
                });

            modelBuilder.Entity("Domain.Books.Book", b =>
                {
                    b.HasOne("Domain.Categories.Category", null)
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_categories_category_id");

                    b.OwnsOne("Domain.Books.Author", "Author", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("DateOfBirth")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("author_date_of_birth");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("author_last_name");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("author_name");

                            b1.HasKey("BookId");

                            b1.ToTable("books");

                            b1.WithOwner()
                                .HasForeignKey("BookId")
                                .HasConstraintName("fk_books_books_id");
                        });

                    b.Navigation("Author")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Loans.Loan", b =>
                {
                    b.HasOne("Domain.Reader.Reader", null)
                        .WithMany("Loans")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_loans_readers_reader_id");
                });

            modelBuilder.Entity("Domain.Reader.Reader", b =>
                {
                    b.OwnsOne("Domain.Reader.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("ReaderId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_last_name");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_name");

                            b1.HasKey("ReaderId");

                            b1.ToTable("readers");

                            b1.WithOwner()
                                .HasForeignKey("ReaderId")
                                .HasConstraintName("fk_readers_readers_id");
                        });

                    b.Navigation("FullName");
                });

            modelBuilder.Entity("ReaderRole", b =>
                {
                    b.HasOne("Domain.Reader.Reader", null)
                        .WithMany()
                        .HasForeignKey("ReadersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reader_role_readers_readers_id");

                    b.HasOne("Domain.Reader.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reader_role_role_roles_id");
                });

            modelBuilder.Entity("Domain.Categories.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Domain.Reader.Reader", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
