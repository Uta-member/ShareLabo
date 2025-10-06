using BlazorPathHelper;
using ShareLabo.Presentation.Blazor.Client.Pages;
using ShareLabo.Presentation.Blazor.Client.Pages.User;

namespace ShareLabo.Presentation.Blazor.Client
{
    [BlazorPath]
    public partial class ShareLaboPagePath
    {
        [Page<Home>]
        public const string Home = "/";

        public const string Logout = "/Account/Logout";

        [Page<UserRegisterPage>]
        public const string UserRegister = "/User/Register";
    }
}
