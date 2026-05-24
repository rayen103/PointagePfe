using CollectManagement.Application.Features.Analyse.Contracts;
using CollectManagement.Application.Interfaces.Repositories.Utilisateurs;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Analyse;
using CollectManagement.Domain.Analyse.Enums;
using CollectManagement.Domain.Analyse.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Domain.Utilisateurs.ValueObjects;

namespace CollectManagement.Application.Features.Analyse.Layouts.Queries.GetReportLayouts;

public sealed class GetReportLayoutsQueryHandler
    : IRequestHandler<GetReportLayoutsQuery, List<ReportLayoutDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IUtilisateurRepository _utilisateurRepository;

    public GetReportLayoutsQueryHandler(
        IUnitOfWork unitOfWork,
        ILoggedInUserService loggedInUserService,
        IUtilisateurRepository utilisateurRepository)
    {
        _unitOfWork = unitOfWork;
        _loggedInUserService = loggedInUserService;
        _utilisateurRepository = utilisateurRepository;
    }

    public async Task<List<ReportLayoutDto>> Handle(
        GetReportLayoutsQuery request,
        CancellationToken cancellationToken)
    {
        var societeId = await ResolveSocieteId(cancellationToken).ConfigureAwait(false);

        var layouts = await _unitOfWork
            .GetRepository<ReportLayout>()
            .GetManyAsync(
                x => x.SocieteId == societeId && x.ReportType == request.ReportType,
                cancellationToken)
            .ConfigureAwait(false);

        return (layouts ?? [])
            .OrderByDescending(x => x.IsDefault)
            .ThenBy(x => x.Name)
            .Select(x => new ReportLayoutDto(
                x.ReportLayoutId.Value,
                x.ReportType,
                x.Name,
                x.ConfigJson,
                x.IsDefault))
            .ToList();
    }

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

