using ErpApp.TruckModule.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Infrastructure;

public class TruckConfiguration : IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.ToTable("Trucks", schema: "truckModule")
            .HasKey(x => x.Id)
            .HasName("TruckId");

        builder.Property(x => x.Status);
        builder.Property(x => x.IsDeleted);

        builder.Ignore(x => x.Events);

        builder.OwnsOne(x => x.DescriptiveData, y =>
        {
            y.HasIndex(z => z.Code).IsUnique();
            y.Property(z => z.Code).HasMaxLength(10);
            y.Property(z => z.Name).HasMaxLength(20);
            y.Property(z => z.Description).HasMaxLength(100);

        });
    }
}
