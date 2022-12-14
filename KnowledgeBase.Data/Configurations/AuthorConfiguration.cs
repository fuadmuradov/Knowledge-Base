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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("SYSDATE");
            builder.Property(x => x.ModifiedAt).IsRequired().HasDefaultValueSql("SYSDATE");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        }
    }
}
