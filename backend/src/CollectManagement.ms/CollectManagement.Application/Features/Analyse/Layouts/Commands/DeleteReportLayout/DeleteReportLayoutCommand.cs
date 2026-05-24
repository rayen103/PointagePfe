namespace CollectManagement.Application.Features.Analyse.Layouts.Commands.DeleteReportLayout;

public record DeleteReportLayoutCommand(Ulid ReportLayoutId) : IRequest;

