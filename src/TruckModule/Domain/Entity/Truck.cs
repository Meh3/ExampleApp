using ErpApp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Domain;

public class Truck : Aggregate, IEntity
{
    public int Id { get; private set; }  // although Code is unique, it's not a good idea to use it as Id because it can be mutable and unoptimnal from db cluster index point of view .

    public TrackDescriptiveData DescriptiveData { get; private set; } = null!;

    public TruckStatus Status { get; private set; } = TruckStatus.OutOfService;

    public TruckStatus? NextStatus => Status switch
    {
        TruckStatus.OutOfService => null,
        TruckStatus.Loading => TruckStatus.ToJob,
        TruckStatus.ToJob => TruckStatus.AtJob,
        TruckStatus.AtJob => TruckStatus.Returning,
        TruckStatus.Returning => TruckStatus.Loading,
        _ => throw new NotImplementedException("Missing case."),
    };

    public bool IsDeleted { get; private set; } // soft deletion only possible


    private Truck() { }

    public static Truck Create(TrackDescriptiveData descriptiveData) =>
        new()
        {
            DescriptiveData = descriptiveData,
        };

    public void UpdateDescriptiveData(TrackDescriptiveData descriptiveData) =>
        DescriptiveData = descriptiveData;

    public void SetOutOfServiceStatus()
    {
        ThrowIfDeleted();

        Status = Status == TruckStatus.OutOfService
            ? throw new DomainValidationException("Truck is already out of service.") // I'm assuming this is a valid business rule but it doesn't have to be.
            : TruckStatus.OutOfService;

        AddDomainEvent(new TruckIsOutOfServiceEvent(Id));
    }
        
    public void SetStatusFromOutOfService(TruckStatus status)
    {
        ThrowIfDeleted();

        if (Status != TruckStatus.OutOfService)
            throw new DomainValidationException($"You can't set arbitrary status when track in not out of service. Use {nameof(SetNextValidStatus)} instead.");

        if (status == TruckStatus.OutOfService)
            throw new DomainValidationException("You can't set out of service status from out of service status.");

        Status = status;

        AddDomainEvent(new TruckWorkStatusChangedEvent(Id, Status));
    }

    public TruckStatus SetNextValidStatus()
    {
        ThrowIfDeleted();

        if (Status == TruckStatus.OutOfService)
            throw new DomainValidationException($"You can't set next valid status when track is out of service. Use {nameof(SetStatusFromOutOfService)} instead.");

        Status = NextStatus!.Value;

        AddDomainEvent(new TruckWorkStatusChangedEvent(Id, Status));

        return Status;
    }

    public void Delete()
    {
        ThrowIfDeleted();

        if (Status != TruckStatus.OutOfService)
        {
            throw new DomainValidationException("Can't delete truck that is not out of service."); // I'm assuming this is a valid business rule but it doesn't have to be.
        }

        IsDeleted = true;

        AddDomainEvent(new TruckDeletedEvent(Id));
    }

    private void ThrowIfDeleted()
    {
        if (IsDeleted)
            throw new DomainValidationException("Truck is deleted.");

    }
}
