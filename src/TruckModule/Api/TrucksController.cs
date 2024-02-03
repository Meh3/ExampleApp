using ErpApp.TruckModule.Application.Query;
using ErpApp.TruckModule.Domain;
using MediatR;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Api;

public class TrucksController : ODataController
{
    private readonly IMediator _mediator;

    public TrucksController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [EnableQuery(PageSize = 10)]
    public async Task<IQueryable<Truck>> Get()
    {
        return await _mediator.Send(new GetQuerableTrucksQuery());
        // of course insted of call mediator we could call directly to the repository here but I wanted to maintain separation
    }

    [EnableQuery]
    public async Task<SingleResult<Truck>> Get([FromODataUri] int key)
    {
        var query = await _mediator.Send(new GetQuerableTrucksQuery());
        return SingleResult.Create(query.Where(x => x.Id == key));
    }
}

internal static class ODataModelBuilderExtension
{
    public static ODataConventionModelBuilder BuildTruckModel(this ODataConventionModelBuilder builder)
    {
        var entitySet = builder.EntitySet<Truck>("Trucks");
        entitySet.EntityType.HasKey(x => x.Id);
        entitySet.EntityType.Ignore(x => x.Events);
        entitySet.EntityType.Ignore(x => x.IsDeleted);
        entitySet.EntityType.Ignore(x => x.NextStatus);
        return builder;
    }
}