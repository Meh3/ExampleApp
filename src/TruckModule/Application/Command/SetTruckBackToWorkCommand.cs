using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record SetTruckBackToWorkCommand(int TruckId, TruckStatus Status) : IRequest;

public class SetTruckBackToWorkHandler(ITruckRepository repository) : IRequestHandler<SetTruckBackToWorkCommand>
{
    public async Task Handle(SetTruckBackToWorkCommand request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetTrack(request.TruckId, cancellationToken);

        truck.SetStatusFromOutOfService(request.Status);

        await repository.UpdateTruck(truck, cancellationToken);
    }
}
