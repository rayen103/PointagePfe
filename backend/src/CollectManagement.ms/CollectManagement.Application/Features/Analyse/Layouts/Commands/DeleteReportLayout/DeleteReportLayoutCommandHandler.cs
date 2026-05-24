using CollectManagement.Application.Shared;
using CollectManagement.Domain.Analyse;
using CollectManagement.Domain.Analyse.ValueObjects;

namespace CollectManagement.Application.Features.Analyse.Layouts.Commands.DeleteReportLayout;

public sealed class DeleteReportLayoutCommandHandler
    : IRequestHandler<DeleteReportLayoutCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReportLayoutCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteReportLayoutCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<ReportLayout>();
        await repo
            .DeleteAsync(x => x.ReportLayoutId == new ReportLayoutId(request.ReportLayoutId), cancellationToken)
            .ConfigureAwait(false);

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}

