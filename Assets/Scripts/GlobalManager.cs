using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;

    // ---------------------------- //

    public int DayCounter { get; private set; } = 0;

    // TODO: revert back to 4
    public int FinalDay { get; private set; } = 4;

    public int CurrentCharacterIndex { get; private set; } = 0;

    public List<Character> CharacterList { get; private set; } = new();

    public StoryState ActiveStoryState;

    public Dictionary<Coachmark, bool> CoachmarkShown = new();

    public int PlayerSubmissionMistake;

    public string PlayerName = "You";

    public bool hasRestartedDay = false;

    public bool hasFinishedGame = false;

    // ---------------------------- //

    [SerializeField] private TextAsset _characterListCSVFile;

    // ---------------------------- //

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ActiveStoryState = new StoryState
        {
            Type = StoryStateType.DayResult,
            DayResult = DayResult.Success
        };

        // ActiveStoryState = new StoryState
        // {
        //     Type = StoryStateType.DayResult,
        //     DayResult = DayResult.Failed
        // };

        // ActiveStoryState = new StoryState
        // {
        //     Type = StoryStateType.Ending,
        //     Ending = EndingResult.EndingC
        // };
    }

    void Start()
    {
        CharacterList = CSVReader.instance.TranslateCSVtoData(_characterListCSVFile);
    }

    public void IncreaseDayCounter()
    {
        DayCounter++;
    }

    public void SetActiveTransition(int transitionIndex)
    {
        ActiveStoryState = new StoryState
        {
            Type = StoryStateType.Transition,
            TransitionIndex = transitionIndex
        };
    }

    public void SetActiveDayResult(DayResult dayResult)
    {
        ActiveStoryState = new StoryState
        {
            Type = StoryStateType.DayResult,
            DayResult = dayResult
        };
    }

    public void SetActiveEnding(EndingResult ending)
    {
        ActiveStoryState = new StoryState
        {
            Type = StoryStateType.Ending,
            Ending = ending
        };
    }

    public void IncreaseCharacterIndex()
    {
        CurrentCharacterIndex++;
    }

    // public void ResetCharacterIndexDay()
    // {
    //     CurrentCharacterIndex -= 4;
    // }

    public void InitializeCharacterIndex()
    {
        switch (DayCounter)
        {
            case 1:
                CurrentCharacterIndex = 0;
                break;
            case 2:
                CurrentCharacterIndex = 4;
                break;
            case 3:
                CurrentCharacterIndex = 8;
                break;
            case 4:
                CurrentCharacterIndex = 12;
                break;
            default:
                break;
        }
    }

    public void ResetDayCounter()
    {
        DayCounter = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play the click sound effect globally
        SoundManager.instance.PlaySFX(SoundManager.SFX.ButtonClickA);
    }
}
