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
    internal class TimescaleResultsConfiguration : IEntityTypeConfiguration<TimescaleResults>
    {
        public void Configure(EntityTypeBuilder<TimescaleResults> builder)
        {
            builder.ToTable("Results");

            
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");

            builder.Property(f => f.TimeDelta).IsRequired();

            builder.Property(f => f.EarliestTime).IsRequired();

            builder.Property(f => f.AverageExecutionTime).IsRequired();

            builder.Property(f => f.MedianValue).IsRequired();

            builder.Property(f => f.MaximumValue).IsRequired();

            builder.Property(f => f.MinimumValue).IsRequired();

            builder.Property(f => f.AverageValue).IsRequired();

            builder.Property<int>("FileId").IsRequired();

        }
    }
}
