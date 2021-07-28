using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Configuration
{
    class DocumentUserConfiguration : IEntityTypeConfiguration<DocumentUser>
    {
        public void Configure(EntityTypeBuilder<DocumentUser> builder)
        {
            builder.HasKey(t => new { t.DocumentId, t.UserId });

            builder.ToTable("DocumentUsers");

            builder.HasOne(t => t.Document).WithMany(pc => pc.DocumentUsers)
                .HasForeignKey(pc => pc.DocumentId);

            builder.HasOne(t => t.User).WithMany(pc => pc.DocumentUsers)
              .HasForeignKey(pc => pc.UserId);
            builder.Property(x => x.ExpirationDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.RequestExtension).IsRequired().HasDefaultValue(false);
        }
    }
}