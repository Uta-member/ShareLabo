using CSStack.TADA;
using Microsoft.AspNetCore.Components;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

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

            var userId = UserId.Create(Guid.NewGuid().ToString());

            UserAccountId userAccountId = default!;
            UserName userName = default!;
            AccountPassword accountPassword = default!;

            try
            {
                userAccountId = UserAccountId.Create(_userRegisterViewModel.AccountId);
                userName = UserName.Create(_userRegisterViewModel.Name);
                accountPassword = AccountPassword.Create(_userRegisterViewModel.Password);
            }
            catch
            {
                _isProcessing = false;
                _message = "アカウント情報の入力に誤りがあります";
                return;
            }

            try
            {
                await SelfAuthUserCreateCommandService.ExecuteAsync(
                    new ISelfAuthUserCreateCommandService.Req()
                    {
                        AccountPassword = accountPassword,
                        UserAccountId = userAccountId,
                        UserId = userId,
                        UserName = userName,
                        OperateInfo =
                            new OperateInfo()
                                {
                                    OperatedDateTime = DateTime.Now,
                                    Operator = userId,
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
