using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Domain;

public enum TruckStatus
{
    OutOfService,
    Loading,
    ToJob,
    AtJob,
    Returning,
}
