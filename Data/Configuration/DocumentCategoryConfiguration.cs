using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    class DocumentCategoryConfiguration : IEntityTypeConfiguration<DocumentCategory>
    {
        public void Configure(EntityTypeBuilder<DocumentCategory> builder)
        {
            builder.HasKey(t => new { t.DocumentId, t.CategoryId });

            builder.ToTable("DocumentCategories");

            builder.HasOne(t => t.Document).WithMany(pc => pc.DocumentCategories)
                .HasForeignKey(pc => pc.DocumentId);

            builder.HasOne(t => t.Category).WithMany(pc => pc.DocumentCategories)
              .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
