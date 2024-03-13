namespace Blazoraco.Web.Client.Counters;

public class CounterState
{
    private int currentCount = 0;

    public event Action? OnCurrentCountChange;
    public int CurrentCount
    {
        get => currentCount;
        set
        {
            currentCount = value;
            OnCurrentCountChange?.Invoke();
        }
    }
}
