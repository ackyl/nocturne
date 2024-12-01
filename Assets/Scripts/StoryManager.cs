using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : SerializedMonoBehaviour
{
    public static StoryManager instance;

    // ---------------------------- //

    [SerializeField] private RectTransform _storyUI;

    [SerializeField] private RectTransform _popUpUI;

    [SerializeField] private TextMeshProUGUI _popUpText;

    [SerializeField] private TextMeshProUGUI _popUpActionText;

    [SerializeField] private RectTransform _failedUI;

    [SerializeField] private RectTransform _failedBackground;

    [SerializeField] private RectTransform _failedCharacter;

    [SerializeField] private RectTransform _failedGun;

    [SerializeField] private RectTransform _endingAUI;

    [SerializeField] private Image _endingACharacterUI;

    [SerializeField] private Sprite[] _endingACharacters;

    [SerializeField] private RectTransform _textFamilyUI;

    [SerializeField] private RectTransform _endingCUI;

    public TextMeshProUGUI visitorChatUI;

    public TextMeshProUGUI playerResponseUI1;

    public TextMeshProUGUI playerResponseUI2;

    public TextAsset[] transitionScripts = new TextAsset[3];

    [Title("Ending")]
    [DictionaryDrawerSettings(KeyLabel = "Ending Result", ValueLabel = "Script")]
    public Dictionary<EndingResult, TextAsset> endingScripts;

    [Title("Day Result")]
    [DictionaryDrawerSettings(KeyLabel = "Day Result", ValueLabel = "Script")]
    public Dictionary<DayResult, TextAsset> dayResultScripts;


    // ---------------------------- //

    private Story _story;

    private string _visitorChat;

    private string _playerResponse1;

    private string _playerResponse2;

    private string _protagonistName = "You";

    private string _dialogueState;

    private bool _dialogueFinishTyping = false;

    private bool _transitionToScene = false;

    private float _initialTextPositionY;

    private int _endingACharacterIndex = 0;

    private Tween wiggle1;

    private Tween wiggle2;

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
        GlobalManager.instance.ActiveStoryState.ActOnActiveState(
            onTransition: index => Debug.Log("Initializing Transition!"),
            onDayResult: dayResult =>
            {
                if (dayResult == DayResult.Success)
                {
                    StartDialogue(transitionScripts[GlobalManager.instance.DayCounter]);
                }
                else
                {
                    StartDialogue(dayResultScripts[dayResult]);
                }
            },
            onEnding: ending => StartDialogue(endingScripts[ending])
        );

        _initialTextPositionY = _textFamilyUI.anchoredPosition.y;

        _textFamilyUI.anchoredPosition = new Vector2(_textFamilyUI.anchoredPosition.x, 0);

        WiggleOptionChoice();
    }

    private void WiggleOptionChoice()
    {
        wiggle1 = playerResponseUI1.rectTransform.DOAnchorPosY(1f, 0.4f)
            .SetRelative() // Move relative to its current position
            .SetEase(Ease.InOutSine) // Smooth wiggle effect
            .SetLoops(-1, LoopType.Yoyo); // Loop forever, up and down

        wiggle2 = playerResponseUI2.rectTransform.DOAnchorPosY(1f, 0.4f)
            .SetRelative() // Move relative to its current position
            .SetEase(Ease.InOutSine) // Smooth wiggle effect
            .SetLoops(-1, LoopType.Yoyo); // Loop forever, up and down
    }

    void InitializeEnding(EndingResult endingResult)
    {
        _transitionToScene = false;

        if (endingResult == EndingResult.EndingA)
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM.EndingA);

            _endingACharacterUI.sprite = _endingACharacters[_endingACharacterIndex];

            _endingAUI.GetComponent<CanvasGroup>().DOFade(1, 1f).OnComplete(() =>
            {
                NextVisitorDialogue();
            });
        }
        else if (endingResult == EndingResult.EndingC)
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM.EndingC);

            _endingCUI.GetComponent<CanvasGroup>().DOFade(1, 1f).OnComplete(() =>
            {
                NextVisitorDialogue();
            });
        }
        else
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM.Spectre, 0);

            _storyUI.GetComponent<CanvasGroup>().DOFade(1, 1f).OnComplete(() =>
            {
                NextVisitorDialogue();
            });
        }
    }

    void InitializeDayResult(DayResult dayResult)
    {
        _transitionToScene = false;

        if (dayResult == DayResult.Failed)
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM.Clairette, 0);

            _failedGun.GetComponent<Image>().DOFade(0, 0);
            _failedUI.GetComponent<CanvasGroup>().DOFade(1, 1f).OnComplete(() =>
            {
                // StartDialogue(dayResultScripts[dayResult]);
                NextVisitorDialogue();
            });
        }
        else
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM.Spectre, 0);

            _storyUI.GetComponent<CanvasGroup>().DOFade(1, 1f).OnComplete(() =>
            {
                // StartDialogue(transitionScripts[GlobalManager.instance.DayCounter]);
                NextVisitorDialogue();
            });
        }
    }

    void StartDialogue(TextAsset textAsset)
    {
        // initiate _story and protagonist_name, visitor_name is inside the ink script
        _dialogueState = "start";
        _story = new Story(textAsset.text);
        _story.variablesState["protagonist_name"] = _protagonistName;
        _story.variablesState["visitor_name"] = "Spectre";
        _story.variablesState["mistake"] = GlobalManager.instance.PlayerSubmissionMistake;

        // handle dialogue state specific changes
        _story.ObserveVariable("dialogue_state", (string varName, object newValue) =>
        {
            _dialogueState = newValue.ToString();

            if (_dialogueState == "end")
            {
                GlobalManager.instance.ActiveStoryState.ActOnActiveState(
                    onTransition: index => LevelLoader.instance.LoadGameplayScene(),
                    onDayResult: dayResult => OnDayResultEnds(dayResult),
                    onEnding: ending => OnEndingEnds(ending)
                );
            }

            if (_dialogueState == "gun")
            {
                _failedGun.GetComponent<Image>().DOFade(1, 1f);
                SoundManager.instance.PlaySFX(SoundManager.SFX.GunReload);
            }

            // if (_dialogueState == "scene")
            // {
            //     GlobalManager.instance.ActiveStoryState.ActOnActiveState(
            //         onTransition: index => Debug.Log("Initializing Transition!"),
            //         onDayResult: dayResult => InitializeDayResult(dayResult),
            //         onEnding: ending => InitializeEnding(ending)
            //     );
            // }

            if (_dialogueState == "switch")
            {
                _endingACharacterIndex++;

                _endingACharacterUI.DOFade(0, 0.25f).OnComplete(() =>
                {
                    _endingACharacterUI.sprite = _endingACharacters[_endingACharacterIndex];
                    _endingACharacterUI.DOFade(1, 0.25f);
                });
            }

            if (_dialogueState == "transition")
            {
                _transitionToScene = true;
            }
        });

        // update character name when talking value changes
        _story.ObserveVariable("talking", (string varName, object newValue) =>
        {
            if (newValue.ToString() == _protagonistName)
            {
                visitorChatUI.color = ColorPalette.White;
            }
            else
            {
                visitorChatUI.color = ColorPalette.Yellow;
            }
        });

        ContinueStory();
    }

    private void OnEndingEnds(EndingResult endingResult)
    {
        if (endingResult == EndingResult.EndingA)
        {
            Image _popUpImage = _popUpUI.GetComponent<Image>();
            _popUpImage.color = ColorPalette.Red3;
            Color currentColor = _popUpImage.color;
            _popUpImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.1f);

            _popUpText.text = "You failed to detect something suspicious today and let the enemy infiltrate.\nYou completed Ending 1.";
            _popUpActionText.text = "Back to main menu";

            _popUpUI.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(() =>
            {
                _popUpUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
            });
        }
        else if (endingResult == EndingResult.EndingB)
        {
            _popUpText.text = "You successfully detect enemy infiltration.\nYou completed Ending 2.";
            _popUpActionText.text = "Back to main menu";

            _popUpUI.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(() =>
            {
                _popUpUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
            });
        }
        else
        {
            _popUpText.text = "You completed the Secret Ending.";
            _popUpActionText.text = "Back to main menu";

            _popUpUI.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(() =>
            {
                _popUpUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
            });
        }

        GlobalManager.instance.ResetDayCounter();
    }

    private void OnDayResultEnds(DayResult dayResult)
    {
        if (dayResult == DayResult.Failed)
        {
            Image _popUpImage = _popUpUI.GetComponent<Image>();
            _popUpImage.color = ColorPalette.Red3;
            Color currentColor = _popUpImage.color;
            _popUpImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.1f);

            _popUpText.text = "You failed to keep Nocturne safe.";
            _popUpActionText.text = $"Replay Day {GlobalManager.instance.DayCounter}";

            SoundManager.instance.PlaySFX(SoundManager.SFX.GunShot);
            _popUpUI.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(() =>
            {
                _popUpUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
            });
        }
        else
        {
            LevelLoader.instance.LoadGameplayScene();
        }
    }

    public void OnPopUpClick()
    {
        SoundManager.instance.PlayClickSound(0);

        // pop up only show on failed transition and ending
        GlobalManager.instance.ActiveStoryState.ActOnActiveState(
            onTransition: index => LevelLoader.instance.LoadGameplayScene(),
            onDayResult: dayResult => LevelLoader.instance.LoadGameplayScene(false),
            onEnding: ending => LevelLoader.instance.LoadMainMenu(true)
        );
    }

    public void ContinueStory()
    {
        if (_story.canContinue)
        {
            if (_transitionToScene)
            {
                visitorChatUI.DOFade(0, 1);
                playerResponseUI1.DOFade(0, 1);
                playerResponseUI2.DOFade(0, 1).OnComplete(() =>
                {
                    ClearAllUI();

                    _textFamilyUI.anchoredPosition = new Vector2(_textFamilyUI.anchoredPosition.x, _initialTextPositionY);

                    GlobalManager.instance.ActiveStoryState.ActOnActiveState(
                        onTransition: index => Debug.Log("Initializing Transition!"),
                        onDayResult: dayResult => InitializeDayResult(dayResult),
                        onEnding: ending => InitializeEnding(ending)
                    );

                    DOVirtual.DelayedCall(1, () =>
                    {
                        visitorChatUI.DOFade(1, 1);
                        playerResponseUI1.DOFade(1, 1);
                        playerResponseUI2.DOFade(1, 1);
                    });
                });
            }
            else
            {
                NextVisitorDialogue();
            }
        }
    }

    void NextVisitorDialogue()
    {
        _visitorChat = _story.Continue();
        ClearChoiceUI();

        TypeText(visitorChatUI, _visitorChat).OnStart(() =>
        {
            _dialogueFinishTyping = false;
        }).OnComplete(() =>
        {
            NextPlayerChoices();
            _dialogueFinishTyping = true;
        });
    }

    public void MakeChoice(int choiceIndex)
    {
        if (_dialogueState != "card" && _dialogueFinishTyping)
        {
            if (_story.canContinue || _story.currentChoices.Count > 0)
            {
                if (_story.currentChoices.Count > choiceIndex)
                {
                    SoundManager.instance.PlayClickSound(3);
                    _story.ChooseChoiceIndex(choiceIndex);
                    ContinueStory();
                }
            }
        }
    }

    public void NextPlayerChoices()
    {
        if (_story.currentChoices.Count > 0)
        {
            // if text is X, dont show anything. used for card choices.
            _playerResponse1 = _story.currentChoices[0].text == "x" ? "" : _story.currentChoices[0].text;
            _playerResponse2 = _story.currentChoices.Count > 1 ? _story.currentChoices[1].text : "";
        }
        else
        {
            _playerResponse1 = "";
            _playerResponse2 = "";
        }

        UpdateChoiceUI();
    }

    private void ClearChoiceUI()
    {
        playerResponseUI1.text = "";
        playerResponseUI2.text = "";
    }

    private void ClearAllUI()
    {
        visitorChatUI.text = "";
        playerResponseUI1.text = "";
        playerResponseUI2.text = "";
    }

    private void UpdateChoiceUI()
    {
        playerResponseUI1.text = _playerResponse1;
        playerResponseUI2.text = _playerResponse2;
    }

    private Tween TypeText(TextMeshProUGUI textComponent, string fullText, float speed = 0.02f, bool withSound = false)
    {
        textComponent.text = "";
        float totalDuration = fullText.Length * speed;

        // play talking sounds
        StartCoroutine(PlayRandomTypingSoundCoroutine(totalDuration));

        // need to do .OnComplete when this function called so return the tween
        return DOTween.To(() => 0, x => SetText(textComponent, fullText, x), fullText.Length, totalDuration).SetEase(Ease.Linear);
    }

    private void SetText(TextMeshProUGUI textComponent, string fullText, int characterCount)
    {
        textComponent.text = fullText.Substring(0, characterCount);
    }

    private IEnumerator PlayRandomTypingSoundCoroutine(float totalDuration)
    {
        float interval = 0.14f;
        float elapsedTime = 0f;

        while (elapsedTime < totalDuration)
        {
            // Play the dialogue sound
            SoundManager.instance.PlaySFX(SoundManager.SFX.DialogueA, 0.5f, 0.6f);

            // Wait for the interval
            yield return new WaitForSeconds(interval);

            // Increment elapsed time
            elapsedTime += interval;
        }
    }

    private TextAsset GetEndingScript(EndingResult ending)
    {
        if (endingScripts.TryGetValue(ending, out var script))
        {
            return script;
        }

        return null;
    }

    private TextAsset GetDayResultScript(DayResult dayResult)
    {
        if (dayResultScripts.TryGetValue(dayResult, out var script))
        {
            return script;
        }

        return null;
    }

    private void OnDestroy()
    {
        wiggle1.Kill();
        wiggle2.Kill();
    }
}
