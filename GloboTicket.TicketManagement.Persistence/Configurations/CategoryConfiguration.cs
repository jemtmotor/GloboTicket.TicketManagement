using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x=> x.CategoryId).IsRequired();
            builder.HasKey(x => x.CategoryId);

            builder.Property(x=> x.Name).IsRequired();
            builder.Property(x => x.LastModifiedBy);
            builder.Property(x => x.CreatedDate);
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.LastModifiedDate);
            
        }
    }
}
