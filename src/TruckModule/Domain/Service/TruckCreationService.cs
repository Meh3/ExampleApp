using ErpApp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Domain;

// no interface for service as i'm not plannig to test separate app layer.
// service is needed in this case as we shouldn't throw domain exception form app layer.
public class TruckCreationService(ITruckRepository truckRepository)
{
    public async Task<int> CreateTruck(TrackDescriptiveData truckData, CancellationToken cancellationToken)
    {
        var truck = Truck.Create(truckData);
        var truckId = await truckRepository.AddTruckAndSendEventIfCodeIsUnique(truck, id => new TruckCreatedEvent(id), cancellationToken) 
            ?? throw new DomainValidationException("Truck code must be unique.");

        return truckId;
    }
}
