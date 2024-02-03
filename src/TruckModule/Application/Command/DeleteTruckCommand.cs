using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record DeleteTruckCommand(int TruckId) : IRequest;

public class DeleteTruckHandler(ITruckRepository repository) : IRequestHandler<DeleteTruckCommand>
{
    public async Task Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetTrack(request.TruckId, cancellationToken);

        truck.Delete(); 

        await repository.UpdateTruck(truck, cancellationToken);
    }
}
