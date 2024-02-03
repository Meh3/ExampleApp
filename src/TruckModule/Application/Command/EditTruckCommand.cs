using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record EditTruckCommand(int TruckId, string Code, string Name, string? Description) : IRequest;

public class EditTruckHandler(TruckEditionService domainService) : IRequestHandler<EditTruckCommand>
{
    public async Task Handle(EditTruckCommand request, CancellationToken cancellationToken) =>
        await domainService.EditDescriptiveData(request.TruckId, new(request.Code, request.Name, request.Description), cancellationToken);
}
