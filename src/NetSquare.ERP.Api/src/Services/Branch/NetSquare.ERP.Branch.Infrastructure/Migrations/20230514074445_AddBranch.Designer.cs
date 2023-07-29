﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetSquare.ERP.Branch.Infrastructure.Data;

#nullable disable

namespace NetSquare.ERP.Branch.Infrastructure.Migrations
{
    [DbContext(typeof(BranchDbContext))]
    [Migration("20230514074445_AddBranch")]
    partial class AddBranch
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("erp")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetSquare.ERP.Branch.Domain.Entities.Branch", b =>
                {
                    b.Property<Guid>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BranchId")
                        .HasColumnOrder(0);

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar")
                        .HasColumnName("BranchAddress");

                    b.Property<Guid>("BranchCity")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BranchCity");

                    b.Property<Guid>("BranchManager")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BranchManager");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar")
                        .HasColumnName("BranchName");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("CreatedOn")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedOn");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("BranchId");

                    b.ToTable("Branch", "erp");
                });

            modelBuilder.Entity("NetSquare.ERP.Branch.Domain.Entities.BranchSeedingEntry", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("__BranchSeedingHistory", "erp");
                });
#pragma warning restore 612, 618
        }
    }
}