using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Features.Analyse.Contracts;
using CollectManagement.Application.Features.Analyse.Layouts.Commands.DeleteReportLayout;
using CollectManagement.Application.Features.Analyse.Layouts.Commands.UpsertReportLayout;
using CollectManagement.Application.Features.Analyse.Layouts.Queries.GetReportLayouts;
using CollectManagement.Application.Features.Analyse.Queries.RunAnalyseQuery;
using CollectManagement.Domain.Analyse.Enums;
using CollectManagement.WebAPI.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class AnalyseEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/analyse");

        var bus = routeGroupBuilder.MapGroup("bus").RequireNavigationPermission("analyse.bi-bus");
        bus.MapGet("layouts", GetLayoutsBus);
        bus.MapPost("layouts", UpsertLayoutBus);
        bus.MapPost("layouts/{id}/delete", DeleteLayoutBus);
        bus.MapPost("query", RunBusQuery);

        var employe = routeGroupBuilder.MapGroup("employe").RequireNavigationPermission("analyse.bi-employe");
        employe.MapGet("layouts", GetLayoutsEmploye);
        employe.MapPost("layouts", UpsertLayoutEmploye);
        employe.MapPost("layouts/{id}/delete", DeleteLayoutEmploye);
        employe.MapPost("query", RunEmployeQuery);

        var trace = routeGroupBuilder.MapGroup("trace").RequireNavigationPermission("analyse.trace");
        trace.MapGet("layouts", GetLayoutsTrace);
        trace.MapPost("layouts", UpsertLayoutTrace);
        trace.MapPost("layouts/{id}/delete", DeleteLayoutTrace);
        trace.MapPost("query", RunTraceQuery);
    }

    private static Task<IResult> GetLayoutsBus(ISender sender, CancellationToken cancellationToken) =>
        GetLayouts(sender, AnalyseReportType.Bus, cancellationToken);

    private static Task<IResult> GetLayoutsEmploye(ISender sender, CancellationToken cancellationToken) =>
        GetLayouts(sender, AnalyseReportType.Employe, cancellationToken);

    private static Task<IResult> GetLayoutsTrace(ISender sender, CancellationToken cancellationToken) =>
        GetLayouts(sender, AnalyseReportType.Trace, cancellationToken);

    private static async Task<IResult> GetLayouts(
        ISender sender,
        AnalyseReportType reportType,
        CancellationToken cancellationToken)
    {
        var layouts = await sender
            .Send(new GetReportLayoutsQuery(reportType), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<List<ReportLayoutDto>>(layouts));
    }

    private static Task<IResult> UpsertLayoutBus(
        [FromBody] [Required] UpsertReportLayoutCommand command,
        ISender sender,
        CancellationToken cancellationToken) =>
        UpsertLayout(command with { ReportType = AnalyseReportType.Bus }, sender, cancellationToken);

    private static Task<IResult> UpsertLayoutEmploye(
        [FromBody] [Required] UpsertReportLayoutCommand command,
        ISender sender,
        CancellationToken cancellationToken) =>
        UpsertLayout(command with { ReportType = AnalyseReportType.Employe }, sender, cancellationToken);

    private static Task<IResult> UpsertLayoutTrace(
        [FromBody] [Required] UpsertReportLayoutCommand command,
        ISender sender,
        CancellationToken cancellationToken) =>
        UpsertLayout(command with { ReportType = AnalyseReportType.Trace }, sender, cancellationToken);

    private static async Task<IResult> UpsertLayout(
        UpsertReportLayoutCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var saved = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<ReportLayoutDto>(saved));
    }

    private static Task<IResult> DeleteLayoutBus(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        DeleteLayout(id, sender, cancellationToken);

    private static Task<IResult> DeleteLayoutEmploye(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        DeleteLayout(id, sender, cancellationToken);

    private static Task<IResult> DeleteLayoutTrace(
        [FromRoute] [Required] Ulid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        DeleteLayout(id, sender, cancellationToken);

    private static async Task<IResult> DeleteLayout(
        Ulid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteReportLayoutCommand(id), cancellationToken).ConfigureAwait(false);
        return Results.Ok(new ApiResponse<bool>(true));
    }

    private static Task<IResult> RunBusQuery(
        [FromBody] [Required] AnalyseQueryRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
        RunQuery(AnalyseReportType.Bus, request, sender, cancellationToken);

    private static Task<IResult> RunEmployeQuery(
        [FromBody] [Required] AnalyseQueryRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
        RunQuery(AnalyseReportType.Employe, request, sender, cancellationToken);

    private static Task<IResult> RunTraceQuery(
        [FromBody] [Required] AnalyseQueryRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
        RunQuery(AnalyseReportType.Trace, request, sender, cancellationToken);

    private static async Task<IResult> RunQuery(
        AnalyseReportType reportType,
        AnalyseQueryRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new RunAnalyseQuery(reportType, request), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<AnalyseQueryResponse>(response));
    }
}

