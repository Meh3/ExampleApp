using ErpApp.TruckModule.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Command;

public record CreateTruckCommand(string Code, string Name, string? Description) : IRequest<int>;

public class CreateTruckHandler(TruckCreationService domainService) : IRequestHandler<CreateTruckCommand, int>
{
    public async Task<int> Handle(CreateTruckCommand request, CancellationToken cancellationToken) =>
        await domainService.CreateTruck(new(request.Code, request.Name, request.Description), cancellationToken);
}
