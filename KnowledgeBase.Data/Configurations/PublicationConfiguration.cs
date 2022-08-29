using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KnowledgeBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Configurations
{
    public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("SYSDATE");
            builder.Property(x => x.ModifiedAt).IsRequired().HasDefaultValueSql("SYSDATE");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(1500);
            builder.Property(x => x.AuthorId).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.OrganizationId).IsRequired();
            builder.Property(x => x.DocumentTypeId).IsRequired();
            builder.Property(x => x.TopicId).IsRequired();
        }
    }
}
