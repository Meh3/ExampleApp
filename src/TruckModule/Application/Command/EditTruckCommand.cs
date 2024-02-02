using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record EditTruckCommand(int TruckId, TrackDescriptiveData TruckData) : IRequest;

internal class EditTruckHandler(TruckEditionService businessService) : IRequestHandler<EditTruckCommand>
{
    public async Task Handle(EditTruckCommand command, CancellationToken cancellationToken) =>
        await businessService.EditDescriptiveData(command.TruckId, command.TruckData, cancellationToken);
}
