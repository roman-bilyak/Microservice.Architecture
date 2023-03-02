using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.ReviewService.Reviews;

internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews")
            .HasKey(x => x.Id);

        builder.Property(x => x.MovieId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Comment)
            .IsRequired()
            .HasMaxLength(Review.MaxCommentLength);
    }
}
