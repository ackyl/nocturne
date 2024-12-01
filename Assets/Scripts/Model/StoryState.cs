using System;

public enum StoryStateType
{
    None,
    Transition,
    DayResult,
    Ending
}

public struct StoryState
{
    public StoryStateType Type;
    public int? TransitionIndex;
    public DayResult? DayResult;
    public EndingResult? Ending;

    public void ActOnActiveState(Action<int> onTransition, Action<DayResult> onDayResult, Action<EndingResult> onEnding)
    {
        switch (Type)
        {
            case StoryStateType.Transition when TransitionIndex.HasValue:
                onTransition?.Invoke(TransitionIndex.Value);
                break;
            case StoryStateType.DayResult when DayResult.HasValue:
                onDayResult?.Invoke(DayResult.Value);
                break;
            case StoryStateType.Ending when Ending.HasValue:
                onEnding?.Invoke(Ending.Value);
                break;
            default:
                onTransition?.Invoke(0);
                break;
        }
    }

    public override string ToString()
    {
        return Type switch
        {
            StoryStateType.Transition => $"Active Transition: {TransitionIndex}",
            StoryStateType.DayResult => $"Active DayResult: {DayResult}",
            StoryStateType.Ending => $"Active Ending: {Ending}",
            _ => "No active state."
        };
    }
}