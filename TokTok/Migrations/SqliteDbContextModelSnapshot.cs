﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TokTok.Database;

namespace TokTok.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    partial class SqliteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("TokTok.Models.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Hidden");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("TokTok.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChannelId");

                    b.Property<DateTime>("SentDate");

                    b.Property<string>("Text");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TokTok.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TokTok.Models.UserInChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChannelId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserInChannels");
                });
#pragma warning restore 612, 618
        }
    }
}
