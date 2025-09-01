using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.Blazor.Client.Auth;

namespace ShareLabo.Presentation.Blazor.Client.Pages
{
    public partial class Home
    {
        private string? _errorMessage;
        private PostDetailReadModel? _postDetail;

        private string? _postId;

        private async Task GetPostDetailAsync()
        {
            _errorMessage = string.Empty;

            try
            {
                var authenticationState = await AuthenticationState;
                var userIdStr = authenticationState.User.FindFirst(x => x.Type == ShareLaboClaim.UserId)?.Value;

                if(string.IsNullOrWhiteSpace(userIdStr))
                {
                    return;
                }

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
        public required IPostDetailFindByIdQueryService PostDetailFindByIdQueryService { get; set; }
    }
}
