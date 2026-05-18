using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Common;

namespace CollectManagement.Domain.Bus;

public class BusRuntimeEvent : AuditableEntity
{
    public BusRuntimeEventId BusRuntimeEventId { get; private set; }

    public BusId BusId { get; private set; }

    public string EventType { get; private set; }

    public string Description { get; private set; }

    public string? IMEI { get; private set; }

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    public int? Occupancy { get; private set; }

    public DateTime OccurredAtUtc { get; private set; }

    private BusRuntimeEvent(
        BusRuntimeEventId busRuntimeEventId,
        BusId busId,
        string eventType,
        string description,
        string? imei,
        double? latitude,
        double? longitude,
        int? occupancy,
        DateTime occurredAtUtc)
    {
        BusRuntimeEventId = busRuntimeEventId;
        BusId = busId;
        EventType = eventType;
        Description = description;
        IMEI = imei;
        Latitude = latitude;
        Longitude = longitude;
        Occupancy = occupancy;
        OccurredAtUtc = occurredAtUtc;
    }

    public static BusRuntimeEvent Create(
        BusRuntimeEventId busRuntimeEventId,
        BusId busId,
        string eventType,
        string description,
        string? imei,
        double? latitude,
        double? longitude,
        int? occupancy,
        DateTime occurredAtUtc)
    {
        return new BusRuntimeEvent(
            busRuntimeEventId,
            busId,
            eventType,
            description,
            imei,
            latitude,
            longitude,
            occupancy,
            occurredAtUtc);
    }

#pragma warning disable CS8618
    private BusRuntimeEvent()
    {
    }
#pragma warning restore CS8618
}
