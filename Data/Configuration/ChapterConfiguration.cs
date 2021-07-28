using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable("Chapters");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();

            builder.HasOne(t => t.Document).WithMany(pc => pc.Chapters)
              .HasForeignKey(pc => pc.DocumentId);

        }
    }
}