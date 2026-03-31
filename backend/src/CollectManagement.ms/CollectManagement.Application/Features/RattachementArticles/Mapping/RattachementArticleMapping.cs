using CollectManagement.Application.Features.RattachementArticles.Commands.CreateRattachementArticle;
using CollectManagement.Application.Features.RattachementArticles.Commands.UpdateRattachementArticle;
using CollectManagement.Application.Features.RattachementArticles.Queries.GetOneRattachementArticle;
using CollectManagement.Application.Features.RattachementArticles.Queries.GetPagedListRattachementArticle;
using CollectManagement.Domain.Rattachements;

namespace CollectManagement.Application.Features.RattachementArticles.Mapping;

public class RattachementArticleMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RattachementArticle, CreateRattachementArticleResponse>()
            .Map(d => d.RattachementArticleId, s => s.RattachementArticleId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<RattachementArticle, UpdateRattachementArticleResponse>()
            .Map(d => d.RattachementArticleId, s => s.RattachementArticleId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value);

        config.NewConfig<RattachementArticle, GetPagedListRattachementArticleDto>()
            .Map(d => d.RattachementArticleId, s => s.RattachementArticleId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<RattachementArticle, GetOneRattachementArticleDto>()
            .Map(d => d.RattachementArticleId, s => s.RattachementArticleId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
