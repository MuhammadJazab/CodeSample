﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetSquare.ERP.HumanResource.Infrastructure.Data;

#nullable disable

namespace NetSquare.ERP.HumanResource.Infrastructure.Migrations
{
    [DbContext(typeof(HumanResourceDbContext))]
    partial class HumanResourceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("erp")
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetSquare.ERP.HumanResource.Domain.Entities.HumanResourceSeedingEntry", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("__HumanResourceSeedingHistory", "erp");
                });
#pragma warning restore 612, 618
        }
    }
}
