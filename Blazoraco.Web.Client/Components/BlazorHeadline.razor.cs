namespace Blazoraco.Web.Client.Components;

using Microsoft.AspNetCore.Components;

public sealed partial class BlazorHeadline
{
    [Parameter]
    public required string Headline { get; set; }
}
