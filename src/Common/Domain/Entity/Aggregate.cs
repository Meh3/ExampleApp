using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.Common.Domain;

public abstract class Aggregate
{
    public Queue<DomainEvent> Events { get; protected set; } = [];

    public void AddDomainEvent(DomainEvent domainEvent) => Events.Enqueue(domainEvent);
}
