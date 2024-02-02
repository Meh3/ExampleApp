using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record CreateTruckCommand(TrackDescriptiveData TruckData) : IRequest<int>;

internal class CreateTruckHandler(TruckCreationService businessService) : IRequestHandler<CreateTruckCommand, int>
{
    public async Task<int> Handle(CreateTruckCommand command, CancellationToken cancellationToken) =>
        await businessService.CreateTruck(command.TruckData, cancellationToken);
}
