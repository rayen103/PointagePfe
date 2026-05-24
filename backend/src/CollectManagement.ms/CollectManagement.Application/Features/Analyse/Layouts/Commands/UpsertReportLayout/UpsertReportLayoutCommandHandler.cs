using CollectManagement.Application.Features.Analyse.Contracts;
using CollectManagement.Application.Interfaces.Repositories.Utilisateurs;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Analyse;
using CollectManagement.Domain.Analyse.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Domain.Utilisateurs.ValueObjects;

namespace CollectManagement.Application.Features.Analyse.Layouts.Commands.UpsertReportLayout;

public sealed class UpsertReportLayoutCommandHandler
    : IRequestHandler<UpsertReportLayoutCommand, ReportLayoutDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IUtilisateurRepository _utilisateurRepository;

    public UpsertReportLayoutCommandHandler(
        IUnitOfWork unitOfWork,
        ILoggedInUserService loggedInUserService,
        IUtilisateurRepository utilisateurRepository)
    {
        _unitOfWork = unitOfWork;
        _loggedInUserService = loggedInUserService;
        _utilisateurRepository = utilisateurRepository;
    }

    public async Task<ReportLayoutDto> Handle(
        UpsertReportLayoutCommand request,
        CancellationToken cancellationToken)
    {
        var societeId = await ResolveSocieteId(cancellationToken).ConfigureAwait(false);
        var repository = _unitOfWork.GetRepository<ReportLayout>();

        ReportLayout layout;
        if (request.ReportLayoutId is not null && request.ReportLayoutId.Value != Ulid.Empty)
        {
            layout = await repository
                .GetAsync(x => x.ReportLayoutId == new ReportLayoutId(request.ReportLayoutId.Value), cancellationToken)
                .ConfigureAwait(false);

            if (layout is null)
            {
                layout = ReportLayout.Create(
                    new ReportLayoutId(request.ReportLayoutId.Value),
                    request.ReportType,
                    request.Name,
                    request.ConfigJson,
                    request.IsDefault,
                    societeId);

                await repository.AddAsync(layout, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                layout.Update(request.Name, request.ConfigJson, request.IsDefault);
                repository.Update(layout);
            }
        }
        else
        {
            layout = ReportLayout.Create(
                new ReportLayoutId(Ulid.NewUlid()),
                request.ReportType,
                request.Name,
                request.ConfigJson,
                request.IsDefault,
                societeId);

            await repository.AddAsync(layout, cancellationToken).ConfigureAwait(false);
        }

        if (request.IsDefault)
        {
            var layouts = await repository
                .GetManyAsync(
                    x => x.SocieteId == societeId && x.ReportType == request.ReportType && x.ReportLayoutId != layout.ReportLayoutId,
                    cancellationToken)
                .ConfigureAwait(false);

            foreach (var other in layouts ?? [])
            {
                if (other.IsDefault)
                {
                    other.Update(other.Name, other.ConfigJson, false);
                    repository.Update(other);
                }
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new ReportLayoutDto(
            layout.ReportLayoutId.Value,
            layout.ReportType,
            layout.Name,
            layout.ConfigJson,
            layout.IsDefault);
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

