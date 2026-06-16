using CollectManagement.Application.Features.Analyse.Contracts;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Employes;
using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Application.Interfaces.Repositories.Utilisateurs;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Analyse.Enums;
using CollectManagement.Domain.Bus;
using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Domain.Utilisateurs.ValueObjects;

namespace CollectManagement.Application.Features.Analyse.Queries.RunAnalyseQuery;

public sealed class RunAnalyseQueryHandler
    : IRequestHandler<RunAnalyseQuery, AnalyseQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IBusRepository _busRepository;
    private readonly IEmployeRepository _employeRepository;
    private readonly IRattachementRepository _rattachementRepository;

    public RunAnalyseQueryHandler(
        IUnitOfWork unitOfWork,
        ILoggedInUserService loggedInUserService,
        IUtilisateurRepository utilisateurRepository,
        IBusRepository busRepository,
        IEmployeRepository employeRepository,
        IRattachementRepository rattachementRepository)
    {
        _unitOfWork = unitOfWork;
        _loggedInUserService = loggedInUserService;
        _utilisateurRepository = utilisateurRepository;
        _busRepository = busRepository;
        _employeRepository = employeRepository;
        _rattachementRepository = rattachementRepository;
    }

    public async Task<AnalyseQueryResponse> Handle(
        RunAnalyseQuery request,
        CancellationToken cancellationToken)
    {
        var societeId = await ResolveSocieteId(cancellationToken).ConfigureAwait(false);
        var dateFrom = request.Request.DateFrom ?? DateTime.UtcNow.AddDays(-30);
        var dateTo = request.Request.DateTo ?? DateTime.UtcNow;

        var selectedFields = (request.Request.Fields ?? [])
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        return request.ReportType switch
        {
            AnalyseReportType.Bus => await RunBusReport(
                societeId,
                dateFrom,
                dateTo,
                selectedFields,
                cancellationToken),
            AnalyseReportType.Employe => await RunEmployeReport(
                societeId,
                dateFrom,
                dateTo,
                selectedFields,
                cancellationToken),
            AnalyseReportType.Trace => await RunTraceReport(
                societeId,
                dateFrom,
                dateTo,
                selectedFields,
                cancellationToken),
            _ => new AnalyseQueryResponse([], [], new Dictionary<string, decimal>()),
        };
    }

    private async Task<AnalyseQueryResponse> RunBusReport(
        SocieteId societeId,
        DateTime dateFrom,
        DateTime dateTo,
        List<string> selectedFields,
        CancellationToken cancellationToken)
    {
        var buses = (await _busRepository.GetAllAsync(cancellationToken).ConfigureAwait(false))
            .Where(b => b.SocieteId == societeId)
            .ToList();

        var events = await _unitOfWork
            .GetRepository<BusRuntimeEvent>()
            .GetManyAsync(e => e.OccurredAtUtc >= dateFrom && e.OccurredAtUtc <= dateTo, cancellationToken)
            .ConfigureAwait(false);

        var eventsByBus = (events ?? [])
            .GroupBy(e => e.BusId)
            .ToDictionary(
                g => g.Key,
                g => new
                {
                    EventsCount = g.Count(),
                    LastEventAtUtc = g.Max(x => x.OccurredAtUtc),
                    MaxOccupancy = g.Max(x => x.Occupancy ?? 0),
                });

        var fields = GetBusFields();
        var columns = BuildColumns(fields, selectedFields);

        var rows = new List<Dictionary<string, object?>>();
        foreach (var bus in buses)
        {
            eventsByBus.TryGetValue(bus.BusId, out var agg);

            var data = new Dictionary<string, object?>
            {
                ["busId"] = bus.BusId.Value.ToString(),
                ["numeroIMM"] = bus.NumeroIMM,
                ["modelBus"] = bus.ModelBus,
                ["imei"] = bus.IMEI,
                ["capacite"] = bus.Capacite,
                ["currentOccupancy"] = bus.CurrentOccupancy,
                ["occupancyRatio"] = bus.Capacite is > 0
                    ? Math.Round((decimal)bus.CurrentOccupancy / (decimal)bus.Capacite, 4)
                    : null,
                ["codeCircuit"] = bus.CodeCircuit,
                ["isActive"] = bus.IsActive,
                ["latitude"] = bus.Latitude,
                ["longitude"] = bus.Longitude,
                ["lastPositionAt"] = bus.LastPositionAt,
                ["eventsCount"] = agg?.EventsCount ?? 0,
                ["lastEventAtUtc"] = agg?.LastEventAtUtc,
                ["maxOccupancyInRange"] = agg?.MaxOccupancy ?? 0,
            };

            rows.Add(FilterRow(data, selectedFields));
        }

        return new AnalyseQueryResponse(columns, rows, ComputeTotals(columns, rows));
    }

    private async Task<AnalyseQueryResponse> RunEmployeReport(
        SocieteId societeId,
        DateTime dateFrom,
        DateTime dateTo,
        List<string> selectedFields,
        CancellationToken cancellationToken)
    {
        var employes = (await _employeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false))
            .Where(e => e.SocieteId == societeId)
            .ToList();

        var dateFromLocal = dateFrom.Date;
        var dateToLocal = dateTo.Date;

        var rattachementsEmploye = await _unitOfWork
            .GetRepository<RattachementEmploye>()
            .GetManyAsync(
                x => x.SocieteId == societeId && x.DateDebut >= dateFromLocal && x.DateDebut <= dateToLocal,
                cancellationToken)
            .ConfigureAwait(false);

        var aggByMatricule = (rattachementsEmploye ?? [])
            .GroupBy(x => x.Matricule)
            .ToDictionary(
                g => g.Key,
                g => new
                {
                    Count = g.Count(),
                    TotalHeures = g.Sum(x => x.NombreHeure ?? 0m),
                    TotalCout = g.Sum(x => x.CoutGlobal ?? 0m),
                });

        var fields = GetEmployeFields();
        var columns = BuildColumns(fields, selectedFields);

        var rows = new List<Dictionary<string, object?>>();
        foreach (var emp in employes)
        {
            aggByMatricule.TryGetValue(emp.Matricule, out var agg);

            var data = new Dictionary<string, object?>
            {
                ["employeId"] = emp.EmployeId.Value.ToString(),
                ["matricule"] = emp.Matricule,
                ["nom"] = emp.Nom,
                ["prenom"] = emp.Prenom,
                ["rfid"] = emp.RFID,
                ["codeCircuit"] = emp.CodeCircuit,
                ["codePointCollecte"] = emp.CodePointCollecte,
                ["codeBus"] = emp.CodeBus,
                ["codeShift"] = emp.CodeShift,
                ["adresse"] = emp.Adresse,
                ["codeGouvernorat"] = emp.CodeGouvernorat,
                ["codeRegion"] = emp.CodeRegion,
                ["latitude"] = emp.Latitude,
                ["longitude"] = emp.Longitude,
                ["assignmentsCount"] = agg?.Count ?? 0,
                ["totalHeures"] = agg?.TotalHeures ?? 0m,
                ["totalCout"] = agg?.TotalCout ?? 0m,
            };

            rows.Add(FilterRow(data, selectedFields));
        }

        return new AnalyseQueryResponse(columns, rows, ComputeTotals(columns, rows));
    }

    private async Task<AnalyseQueryResponse> RunTraceReport(
        SocieteId societeId,
        DateTime dateFrom,
        DateTime dateTo,
        List<string> selectedFields,
        CancellationToken cancellationToken)
    {
        var dateFromLocal = dateFrom.Date;
        var dateToLocal = dateTo.Date;

        var rattachements = await _unitOfWork
            .GetRepository<Rattachement>()
            .GetManyAsync(
                x => x.SocieteId == societeId && x.DateRattachement >= dateFromLocal && x.DateRattachement <= dateToLocal,
                cancellationToken)
            .ConfigureAwait(false);

        var fields = GetTraceFields();
        var columns = BuildColumns(fields, selectedFields);

        var rows = new List<Dictionary<string, object?>>();
        foreach (var r in rattachements ?? [])
        {
            var data = new Dictionary<string, object?>
            {
                ["rattachementId"] = r.RattachementId.Value.ToString(),
                ["numeroRattachement"] = r.NumeroRattachement,
                ["dateRattachement"] = r.DateRattachement,
                ["numeroChantier"] = r.NumeroChantier,
                ["codeClient"] = r.CodeClient,
                ["isInternal"] = r.IsInternal,
                ["cout"] = r.Cout,
                ["type"] = r.Type,
                ["nature"] = r.Nature,
                ["responsable"] = r.Responsable,
                ["status"] = r.Status,
                ["dateCloture"] = r.DateCloture,
                ["emplacement"] = r.Emplacement,
                ["reference"] = r.Reference,
                ["isActive"] = r.IsActive,
            };

            rows.Add(FilterRow(data, selectedFields));
        }

        return new AnalyseQueryResponse(columns, rows, ComputeTotals(columns, rows));
    }

    private static Dictionary<string, object?> FilterRow(
        Dictionary<string, object?> allValues,
        List<string> selectedFields)
    {
        if (selectedFields.Count == 0)
        {
            return allValues;
        }

        var selectedSet = new HashSet<string>(selectedFields, StringComparer.OrdinalIgnoreCase);
        return allValues
            .Where(kv => selectedSet.Contains(kv.Key))
            .ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    private static List<AnalyseColumnDto> BuildColumns(
        List<AnalyseColumnDto> allFields,
        List<string> selectedFields)
    {
        if (selectedFields.Count == 0)
        {
            return allFields;
        }

        var selectedSet = new HashSet<string>(selectedFields, StringComparer.OrdinalIgnoreCase);
        return allFields.Where(f => selectedSet.Contains(f.Key)).ToList();
    }

    private static Dictionary<string, decimal> ComputeTotals(
        List<AnalyseColumnDto> columns,
        List<Dictionary<string, object?>> rows)
    {
        var totals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        foreach (var col in columns.Where(c => c.IsNumeric))
        {
            decimal sum = 0m;
            foreach (var row in rows)
            {
                if (!row.TryGetValue(col.Key, out var val) || val is null)
                {
                    continue;
                }

                if (val is decimal d)
                {
                    sum += d;
                }
                else if (val is int i)
                {
                    sum += i;
                }
                else if (val is long l)
                {
                    sum += l;
                }
                else if (val is double db)
                {
                    sum += (decimal)db;
                }
                else if (val is float f)
                {
                    sum += (decimal)f;
                }
                else if (val is string s && decimal.TryParse(s, out var parsed))
                {
                    sum += parsed;
                }
            }

            totals[col.Key] = sum;
        }

        return totals;
    }

    private static List<AnalyseColumnDto> GetBusFields() =>
        [
            new AnalyseColumnDto("numeroIMM", "Bus", "string", false),
            new AnalyseColumnDto("modelBus", "Modèle", "string", false),
            new AnalyseColumnDto("imei", "IMEI", "string", false),
            new AnalyseColumnDto("capacite", "Capacité", "number", true),
            new AnalyseColumnDto("currentOccupancy", "Occupation", "number", true),
            new AnalyseColumnDto("occupancyRatio", "Taux", "number", true),
            new AnalyseColumnDto("codeCircuit", "Circuit", "string", false),
            new AnalyseColumnDto("isActive", "Actif", "boolean", false),
            new AnalyseColumnDto("latitude", "Latitude", "number", true),
            new AnalyseColumnDto("longitude", "Longitude", "number", true),
            new AnalyseColumnDto("lastPositionAt", "Dernière position", "datetime", false),
            new AnalyseColumnDto("eventsCount", "Events (période)", "number", true),
            new AnalyseColumnDto("lastEventAtUtc", "Dernier event", "datetime", false),
            new AnalyseColumnDto("maxOccupancyInRange", "Max occupation (période)", "number", true),
        ];

    private static List<AnalyseColumnDto> GetEmployeFields() =>
        [
            new AnalyseColumnDto("matricule", "Matricule", "string", false),
            new AnalyseColumnDto("nom", "Nom", "string", false),
            new AnalyseColumnDto("prenom", "Prénom", "string", false),
            new AnalyseColumnDto("rfid", "RFID", "string", false),
            new AnalyseColumnDto("codeCircuit", "Circuit", "string", false),
            new AnalyseColumnDto("codePointCollecte", "Point", "string", false),
            new AnalyseColumnDto("codeBus", "Bus", "string", false),
            new AnalyseColumnDto("codeShift", "Shift", "string", false),
            new AnalyseColumnDto("codeGouvernorat", "Gouvernorat", "string", false),
            new AnalyseColumnDto("codeRegion", "Région", "string", false),
            new AnalyseColumnDto("assignmentsCount", "Affectations (période)", "number", true),
            new AnalyseColumnDto("totalHeures", "Heures (période)", "number", true),
            new AnalyseColumnDto("totalCout", "Coût (période)", "number", true),
        ];

    private static List<AnalyseColumnDto> GetTraceFields() =>
        [
            new AnalyseColumnDto("numeroRattachement", "N° Rattachement", "string", false),
            new AnalyseColumnDto("dateRattachement", "Date", "date", false),
            new AnalyseColumnDto("numeroChantier", "Chantier", "string", false),
            new AnalyseColumnDto("codeClient", "Client", "string", false),
            new AnalyseColumnDto("isInternal", "Interne", "boolean", false),
            new AnalyseColumnDto("cout", "Coût", "number", true),
            new AnalyseColumnDto("type", "Type", "string", false),
            new AnalyseColumnDto("nature", "Nature", "string", false),
            new AnalyseColumnDto("responsable", "Responsable", "string", false),
            new AnalyseColumnDto("status", "Statut", "string", false),
            new AnalyseColumnDto("dateCloture", "Clôture", "date", false),
            new AnalyseColumnDto("emplacement", "Emplacement", "string", false),
            new AnalyseColumnDto("reference", "Référence", "string", false),
            new AnalyseColumnDto("isActive", "Actif", "boolean", false),
        ];

    private async Task<SocieteId> ResolveSocieteId(CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(_loggedInUserService.UserId, out var userId))
        {
            return new SocieteId(Ulid.Empty);
        }

        var utilisateur = await _utilisateurRepository
            .GetOneAsync(new UtilisateurId(userId), cancellationToken)
            .ConfigureAwait(false);

        return utilisateur?.SocieteId ?? new SocieteId(Ulid.Empty);
    }
}

