using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Author).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.View).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.IsShow).IsRequired().HasDefaultValue(true);



        }

    }
}
