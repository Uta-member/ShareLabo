using CSStack.PrimeBlazor.Bootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ShareLabo.Presentation.Blazor.Client.Common
{
    public abstract class ShareLaboComponentBase : ComponentBase
    {
        [Inject]
        public required DialogService DialogService { get; set; }

        [Inject]
        public required IJSRuntime JSRuntime { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Inject]
        public required NotificationService NotificationService { get; set; }
    }
}
