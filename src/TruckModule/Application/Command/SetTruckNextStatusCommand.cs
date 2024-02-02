using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record SetTruckNextStatusCommand(int TruckId) : IRequest;

internal class SetTruckNextStatusHandler(ITruckRepository repository) : IRequestHandler<SetTruckNextStatusCommand>
{
    public async Task Handle(SetTruckNextStatusCommand command, CancellationToken cancellationToken)
    {
        var truck = await repository.GetTrack(command.TruckId, cancellationToken);

        truck.SetNextValidStatus();

        await repository.UpdateTruck(truck, cancellationToken);
    }
}
