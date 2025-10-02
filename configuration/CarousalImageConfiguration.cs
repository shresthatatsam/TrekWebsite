
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRoles.Models; // Adjust based on where your CarousalImage model is

namespace UserRoles.configuration
{

    public class CarousalImageConfiguration : IEntityTypeConfiguration<CarousalImage>
    {
        public void Configure(EntityTypeBuilder<CarousalImage> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(ci => ci.Caption)
                   .HasMaxLength(255);

            builder.Property(ci => ci.SubCaption)
                   .HasMaxLength(255);

            builder.Property(ci => ci.IsActive)
                   .HasDefaultValue(true);

            builder.Property(ci => ci.carousalEnum)
                   .IsRequired().HasConversion<int>();
        }
    }

}
