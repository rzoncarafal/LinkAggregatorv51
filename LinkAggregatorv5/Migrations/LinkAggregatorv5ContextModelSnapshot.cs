﻿// <auto-generated />
using LinkAggregatorv5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LinkAggregatorv5.Migrations
{
    [DbContext(typeof(LinkAggregatorv5Context))]
    partial class LinkAggregatorv5ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("LinkAggregatorv5.Models.Link", b =>
                {
                    b.Property<int>("IdLink")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Description");

                    b.Property<int>("LikeCount");

                    b.Property<string>("LinkURL");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("WhoAdd");

                    b.HasKey("IdLink");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("LinkAggregatorv5.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("Password");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
