using ErpApp.TruckModule.Domain;
using ErpApp.TruckModule.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Application.Query;

public record GetQuerableTrucksQuery() : IRequest<IQueryable<Truck>>;

public class GetQuerableTrucksHandler(TruckDbContext dbContext) : IRequestHandler<GetQuerableTrucksQuery, IQueryable<Truck>>
{
    public async Task<IQueryable<Truck>> Handle(GetQuerableTrucksQuery request, CancellationToken cancellationToken) =>
        await Task.FromResult(dbContext.Trucks.AsNoTracking().AsQueryable().Where(x => !x.IsDeleted));
}
