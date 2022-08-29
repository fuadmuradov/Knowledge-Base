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
    public class PublicationToTagConfiguration : IEntityTypeConfiguration<PublicationToTag>
    {
        public void Configure(EntityTypeBuilder<PublicationToTag> builder)
        {
            builder.Property(x => x.PublicationId).IsRequired();
            builder.Property(x=>x.TagId).IsRequired();
        }
    }
}
