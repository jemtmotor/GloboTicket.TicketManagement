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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId);
            builder.Property(x => x.OrderPlaced);
            builder.Property(x => x.OrderTotal);
            builder.Property(x => x.OrderPaid);
        }
    }
}
