using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApp.Models.DataDb;

namespace WebApp.Migrations.DataDb
{
    [DbContext(typeof(DataDbContext))]
    partial class DataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Angular2Core.Models.DataDb.Localizations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Container")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Localizations","Model");
                });
        }
    }
}
