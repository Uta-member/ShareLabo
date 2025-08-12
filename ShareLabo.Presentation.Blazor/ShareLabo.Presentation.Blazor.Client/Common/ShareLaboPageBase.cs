namespace ShareLabo.Presentation.Blazor.Client.Common
{
    public abstract class ShareLaboPageBase : ShareLaboComponentBase
    {
        protected string PageTitle => $"{PageTitleCore} - ShareLabo";

        protected abstract string PageTitleCore { get; }
    }
}
