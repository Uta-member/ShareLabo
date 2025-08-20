using Microsoft.AspNetCore.Components;

namespace ShareLabo.Presentation.Blazor.Client.Parts
{
    public partial class Icon
    {
        [Parameter]
        [EditorRequired]
        public required string IconName { get; set; }
    }
}
