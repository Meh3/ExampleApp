using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record SetTruckOutOfServiceCommand(int TruckId) : IRequest;

internal class SetTruckOutOfServiceHandler(ITruckRepository repository) : IRequestHandler<SetTruckOutOfServiceCommand>
{
    public async Task Handle(SetTruckOutOfServiceCommand command, CancellationToken cancellationToken)
    {
        var truck = await repository.GetTrack(command.TruckId, cancellationToken);

        truck.SetOutOfServiceStatus();

        await repository.UpdateTruck(truck, cancellationToken);
    }
}
