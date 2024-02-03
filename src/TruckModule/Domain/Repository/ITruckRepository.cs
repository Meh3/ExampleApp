using ErpApp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Domain;

// simplified version to not have to deal with:
// - unit of work and sending events in the same transaction
// - checking uniqueness of truck code separately in domain service (we rely on the infrastructure layer to do that)
public interface ITruckRepository
{
    public Task<int?> AddTruckAndSendEventIfCodeIsUnique(Truck truck, Func<int, DomainEvent> domainEvent, CancellationToken cancellationToken);

    public Task<bool> TryUpdateTruckIfCodeIsUnique(Truck truck, CancellationToken cancellationToken);

    public Task UpdateTruck(Truck truck, CancellationToken cancellationToken);

    public Task<Truck> GetTrack(int truckId, CancellationToken cancellationToken);
}
