namespace CollectManagement.Application.Features.Bus.Commands.CreateBus;

public class CreateBusResponse
{
    public Ulid BusId { get; set; }
    public string NumeroIMM { get; set; } = string.Empty;
    public string? ModelBus { get; set; }
    public string? IMEI { get; set; }
    public int? Capacite { get; set; }
    public string? CodeCircuit { get; set; }
    public bool AppSagem { get; set; }
    public bool IsActive { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Ulid SocieteId { get; set; }
}
