using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InfotecsWebAPI.Domain.Entities;

namespace InfotecsWebAPI.Persistence.Configuration
{
    internal class TimescaleFileConfiguration : IEntityTypeConfiguration<TimescaleFile>
    {
        public void Configure(EntityTypeBuilder<TimescaleFile> builder)
        {
            builder.ToTable("Files");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(f => f.FileName).IsUnique();

            builder.HasOne(f => f.Results)
                .WithOne()
                .HasForeignKey<TimescaleResults>("FileId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.Values)
                .WithOne()
                .HasForeignKey("FileId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
