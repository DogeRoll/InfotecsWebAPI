using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfotecsWebAPI.Persistence.Configuration
{
    internal class TimescaleValuesConfiguration : IEntityTypeConfiguration<TimescaleValues>
    {
        public void Configure(EntityTypeBuilder<TimescaleValues> builder)
        {
            builder.ToTable("Values");

            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");
            

            builder.Property(r => r.Date).IsRequired();

            builder.Property(r => r.ExecutionTime).IsRequired();

            builder.Property(r => r.Value).IsRequired();

            builder.Property<int>("FileId").IsRequired();

            /*
            builder.Property<int>("FileId").IsRequired();
            builder.HasIndex("FileId");*/
        }
    }
}
