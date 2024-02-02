using ErpApp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Domain;

// no interface for service as i'm not plannig to test separate app layer.
// service is needed in this case as we shouldn't throw domain exception form app layer.
public class TruckEditionService(ITruckRepository truckRepository)
{
    public async Task EditDescriptiveData(int truckId, TrackDescriptiveData truckData, CancellationToken cancellationToken)
    {
        var truck = await truckRepository.GetTrack(truckId, cancellationToken);

        truck.UpdateDescriptiveData(truckData);

        if (!await truckRepository.UpdateTruckIfCodeIsUnique(truck, cancellationToken))
        {
            throw new DomainValidationException("Truck code must be unique.");
        }
    }
}
