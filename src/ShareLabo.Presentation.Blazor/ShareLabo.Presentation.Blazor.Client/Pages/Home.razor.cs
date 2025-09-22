using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.Blazor.Client.Pages
{
    public partial class Home
    {
        private string? _errorMessage;
        private PostDetailReadModel? _postDetail;

        private string? _postId;

        private async Task GetGeneralPostsAsync()
        {
            try
            {
                await GeneralPostsGetQueryService.ExecuteAsync(
                    new IGeneralPostsGetQueryService.Req()
                    {
                        Length = 10,
                        UserId = "dummy-user-id",
                    });
            }
            catch(Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }

        private async Task GetPostDetailAsync()
        {
            _errorMessage = string.Empty;

            try
            {
                if(_postId == null)
                {
                    return;
                }

                var postDetailRes = await PostDetailFindByIdQueryService.ExecuteAsync(
                    new IPostDetailFindByIdQueryService.Req()
                    {
                        PostId = _postId,
                    });

                postDetailRes.PostDetailOptional.TryGetValue(out _postDetail);
            }
            catch(Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }

        [CascadingParameter]
        public required Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        public required IGeneralPostsGetQueryService GeneralPostsGetQueryService { get; set; }

        [Inject]
        public required IPostDetailFindByIdQueryService PostDetailFindByIdQueryService { get; set; }
    }
}
