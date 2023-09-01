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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder) 
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);

            builder.Property(e => e.EventId).IsRequired();
            builder.HasKey(e => e.EventId);
            builder.Property(e => e.Artist);
            builder.Property(e => e.ImageUrl);
            builder.Property(e => e.LastModifiedBy);
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.CategoryId);
            builder.Property(e => e.CreatedDate);
            builder.Property(e => e.Date);
            builder.Property(e => e.Description);
            builder.Property(e => e.Price);

            builder.HasOne(e => e.Category).WithMany(e => e.Events).HasForeignKey(e=>e.CategoryId).IsRequired();

        }
    }
}
