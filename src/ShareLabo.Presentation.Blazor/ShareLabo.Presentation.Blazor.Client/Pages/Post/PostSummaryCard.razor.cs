using Microsoft.AspNetCore.Components;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.Blazor.Client.Pages.Post
{
    public partial class PostSummaryCard
    {
        [Parameter]
        [EditorRequired]
        public required PostSummaryReadModel Post { get; set; }
    }
}
