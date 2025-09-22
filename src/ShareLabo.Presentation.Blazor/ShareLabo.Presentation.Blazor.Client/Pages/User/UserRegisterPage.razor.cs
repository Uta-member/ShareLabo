using CSStack.TADA;
using Microsoft.AspNetCore.Components;
using ShareLabo.Application.Toolkit;
using ShareLabo.Application.UseCase.CommandService.User;

namespace ShareLabo.Presentation.Blazor.Client.Pages.User
{
    public partial class UserRegisterPage
    {
        private bool _isProcessing = false;
        private string _message = string.Empty;
        private UserRegisterViewModel _userRegisterViewModel = new();

        private async Task RegisterAsync()
        {
            _isProcessing = true;
            _message = string.Empty;
            await InvokeAsync(StateHasChanged);

            try
            {
                var userIdStr = Guid.NewGuid().ToString();
                await SelfAuthUserCreateCommandService.ExecuteAsync(
                    new ISelfAuthUserCreateCommandService.Req()
                    {
                        AccountPassword = _userRegisterViewModel.Password,
                        UserAccountId = _userRegisterViewModel.AccountId,
                        UserId = userIdStr,
                        UserName = _userRegisterViewModel.Name,
                        OperateInfo =
                            new OperateInfoWriteModel()
                                {
                                    OperatedDateTime = DateTime.Now,
                                    Operator = userIdStr,
                                }
                    });
                NotificationService.Notify("ユーザ登録", "ユーザ登録が完了しました");
                await Task.Delay(1000);
                NavigationManager.NavigateTo(ShareLaboPagePath.Helper.Login(), true);
            }
            catch(ObjectAlreadyExistException)
            {
                _message = "すでに存在するアカウントIDです";
            }
            catch
            {
                _message = "ユーザ登録に失敗しました";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        protected override string PageTitleCore => "ユーザ登録";

        [Inject]
        public required ISelfAuthUserCreateCommandService SelfAuthUserCreateCommandService { get; set; }

        private sealed record UserRegisterViewModel
        {
            public string AccountId { get; set; } = string.Empty;

            public string Name { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;
        }
    }
}
