namespace Blazoraco.Web.Client.Components;

using Blazoraco.Web.Client.Counters;
using Microsoft.AspNetCore.Components;

public sealed partial class BlazorCounterSharedState : IDisposable
{
    [Inject]
    public required CounterState CounterState { get; set; }

    protected override void OnInitialized() => CounterState.OnCurrentCountChange += StateHasChanged;

    private void IncrementCount() => CounterState.CurrentCount++;

    void IDisposable.Dispose() => CounterState.OnCurrentCountChange -= StateHasChanged;
}
