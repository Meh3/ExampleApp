using ErpApp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Domain;

public record TruckCreatedEvent(int TruckId) : DomainEvent;
