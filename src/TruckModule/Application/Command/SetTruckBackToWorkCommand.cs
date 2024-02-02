using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record SetTruckBackToWorkCommand(int TruckId, TruckStatus Status) : IRequest;

internal class SetTruckBackToWorkhandler(ITruckRepository repository) : IRequestHandler<SetTruckBackToWorkCommand>
{
    public async Task Handle(SetTruckBackToWorkCommand command, CancellationToken cancellationToken)
    {
        var truck = await repository.GetTrack(command.TruckId, cancellationToken);

        truck.SetStatusFromOutOfService(command.Status);

        await repository.UpdateTruck(truck, cancellationToken);
    }
}
