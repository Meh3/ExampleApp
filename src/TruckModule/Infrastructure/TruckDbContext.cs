using ErpApp.Common.Domain;
using ErpApp.TruckModule.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ErpApp.TruckModule.Infrastructure;

public partial class TruckDbContext : DbContext, ITruckRepository
{
    private const int SqlUniqueConstraintViolation = 2601;
    public DbSet<Truck> Trucks { get; set; }


    public TruckDbContext(DbContextOptions<TruckDbContext> options) : base(options) { }

    public async Task<int?> AddTruckAndSendEventIfCodeIsUnique(Truck truck, Func<int, DomainEvent> domainEvent, CancellationToken cancellationToken)
    {
        try
        {
            var added = Trucks.Add(truck);
            await SaveChangesAsync(true, cancellationToken);

            SentDomainEvents(truck, domainEvent(added.Entity.Id));

            return added.Entity.Id;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == SqlUniqueConstraintViolation)
        {
            return null;
        }

    }

    public async Task<Truck> GetTrack(int truckId, CancellationToken cancellationToken) =>
        await Trucks.FirstAsync(t => t.Id == truckId, cancellationToken);

    public async Task UpdateTruck(Truck truck, CancellationToken cancellationToken)
    {
        Trucks.Update(truck);
        await SaveChangesAsync(true, cancellationToken);

        SentDomainEvents(truck);
    }

    public async Task<bool> TryUpdateTruckIfCodeIsUnique(Truck truck, CancellationToken cancellationToken)
    {
        try
        {
            Trucks.Update(truck);
            await SaveChangesAsync(true, cancellationToken);

            SentDomainEvents(truck);

            return true;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == SqlUniqueConstraintViolation)
        {
            return false;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TruckDbContext).Assembly);
    }

    private static void SentDomainEvents(IEntity entity, params DomainEvent[] additionalEvents)
    {
        if (entity is Aggregate aggregate)
        {
            var events = aggregate.Events;
        }

        // here events and additionalEvents can be sent to some dispatcher
    }
}
