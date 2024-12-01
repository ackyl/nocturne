using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;

    // ---------------------------- //

    private List<PlayerSubmission> _playerSubmissions = new();

    // ---------------------------- //

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GlobalManager.instance.InitializeCharacterIndex();

        SoundManager.instance.PlayBGM(SoundManager.BGM.LobbyA, 2, 0);

        DOVirtual.DelayedCall(0.75f, () =>
            {
                CodeManager.instance.GenerateDailyCode().OnComplete(() =>
                {
                    // If coachmark is already shown, proceed to generate the next character immediately
                    if (GlobalManager.instance.CoachmarkShown.TryGetValue(Coachmark.Code, out bool isShown) && isShown)
                    {
                        DOVirtual.DelayedCall(1.25f, () =>
                        {
                            EncounterManager.instance.GenerateNextCharacter();
                        });
                    }
                    else
                    {
                        CoachmarkManager.instance.ShowCoachmark(Coachmark.Code);
                    }
                });
            }
        );

        Debug.Log($">> Current day: {GlobalManager.instance.DayCounter}");
    }

    public void OnPlayerSubmitCard()
    {
        CardType cardType = EncounterManager.instance.SelectedCard ?? CardType.Standard;

        PlayerSubmission submission = new PlayerSubmission
        {
            characterId = GlobalManager.instance.CharacterList[GlobalManager.instance.CurrentCharacterIndex].id,
            identity = GlobalManager.instance.CharacterList[GlobalManager.instance.CurrentCharacterIndex].identity
        };

        if (GlobalManager.instance.DayCounter == GlobalManager.instance.FinalDay)
        {
            submission.MapSubmission(cardType);
        }
        else
        {
            submission.MapSubmission(cardType);
        }

        _playerSubmissions.Add(submission);
    }

    public void AnalyzePlayerSubmissions()
    {
        // analyzing not final day
        int hasFalseSubmission = 0;
        bool hasSecretEnding = false;

        foreach (var submission in _playerSubmissions)
        {
            if (!submission.submissionResult)
            {
                hasFalseSubmission++;
            }

            if (submission.secretEnding)
            {
                hasSecretEnding = true;
            }
        }

        if (hasSecretEnding)
        {
            GlobalManager.instance.SetActiveEnding(EndingResult.EndingC);
        }
        else
        {
            if (GlobalManager.instance.DayCounter == GlobalManager.instance.FinalDay)
            {
                if (hasFalseSubmission > 0)
                {
                    GlobalManager.instance.SetActiveEnding(EndingResult.EndingA);
                }
                else
                {
                    GlobalManager.instance.SetActiveEnding(EndingResult.EndingB);
                }
            }
            else
            {
                if (hasFalseSubmission > 0)
                {
                    GlobalManager.instance.SetActiveDayResult(DayResult.Failed);
                }
                else
                {
                    GlobalManager.instance.SetActiveDayResult(DayResult.Success);
                }
            }
        }

        GlobalManager.instance.PlayerSubmissionMistake = hasFalseSubmission;

        ProceedToStory();
    }

    private void ProceedToStory()
    {
        if (GlobalManager.instance.DayCounter == 3)
        {
            // skip story
        }

        Debug.Log(">> Proceed to next story/transition.");

        LevelLoader.instance.LoadStoryScene();
    }

    public void OnCompleteDailyEncounter()
    {
        AnalyzePlayerSubmissions();
    }
}
