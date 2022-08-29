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
    internal class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("SYSDATE");
            builder.Property(x => x.ModifiedAt).IsRequired().HasDefaultValueSql("SYSDATE");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.TypeName).IsRequired().HasMaxLength(50);
        }
    }
}
