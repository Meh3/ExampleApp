using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record SetTruckNextStatusCommand(int TruckId) : IRequest<TruckStatus>;

public class SetTruckNextStatusHandler(ITruckRepository repository) : IRequestHandler<SetTruckNextStatusCommand, TruckStatus>
{
    public async Task<TruckStatus> Handle(SetTruckNextStatusCommand request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetTrack(request.TruckId, cancellationToken);

        var nextStatus = truck.SetNextValidStatus();

        await repository.UpdateTruck(truck, cancellationToken);

        return nextStatus;
    }
}
