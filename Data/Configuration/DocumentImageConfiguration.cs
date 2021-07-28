using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    class DocumentImageConfiguration : IEntityTypeConfiguration<DocumentImage>
    {
        public void Configure(EntityTypeBuilder<DocumentImage> builder)
        {
            builder.ToTable("DocumentImages");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(x => x.Caption).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.HasOne(t => t.Document).WithOne(pc => pc.DocumentImage)
                .HasForeignKey<DocumentImage>(pc => pc.DocumentId);


        }
    }
}