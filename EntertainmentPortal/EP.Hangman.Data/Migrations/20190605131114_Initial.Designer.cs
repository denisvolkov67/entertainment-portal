﻿// <auto-generated />
using EP.Hangman.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EP.Hangman.Data.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20190605131114_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("EP.Hangman.Data.Models.GameDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlphabetAsString");

                    b.Property<string>("CorrectLettersAsString");

                    b.Property<string>("PickedWord");

                    b.Property<int>("UserErrors");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
