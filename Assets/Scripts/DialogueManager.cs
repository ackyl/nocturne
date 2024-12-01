using UnityEngine;
using System.Collections;
using Ink.Runtime;
using TMPro;
using DG.Tweening;
using Unity.Collections;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;
using System.Reflection;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    // ---------------------------- //

    public TextAsset inkJSON;

    public TextMeshProUGUI visitorChatUI;

    public TextMeshProUGUI playerResponseUI1;

    public TextMeshProUGUI playerResponseUI2;

    public TextMeshProUGUI characterName;

    public RectTransform documentUI;

    public CanvasGroup canvasGroup;

    public RectTransform documentButtonUI;

    public RectTransform cardDrawerUI;

    public Image docSignImage;

    // ---------------------------- //

    private Story _story;

    private string _visitorChat;

    private string _playerResponse1;

    private string _playerResponse2;

    private Vector3 _originalDocumentUIPosition;

    private Vector3 _originalDocumentButtonPosition;

    private string _protagonistName = "You";

    private string _dialogueState;

    private bool _documentShown = false;

    private bool _dialogueFinishTyping = false;

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
        // hide document on initial dialogue
        // record document initial position
        _originalDocumentUIPosition = documentUI.anchoredPosition;

        canvasGroup.DOFade(0, 0).SetEase(Ease.InQuad);

        _dialogueState = "start";

        _originalDocumentButtonPosition = documentButtonUI.anchoredPosition;
        documentButtonUI.anchoredPosition = new Vector2(documentButtonUI.anchoredPosition.x, 120f);
        documentButtonUI.GetComponent<EventTrigger>().enabled = false;

        WiggleOptionChoice();

        _protagonistName = GlobalManager.instance.PlayerName;
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

    private void ShowDocumentButton()
    {
        documentButtonUI.DOAnchorPos(_originalDocumentButtonPosition, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            documentButtonUI.GetComponent<EventTrigger>().enabled = true;
        });
    }

    private void HideDocumentButton()
    {
        documentButtonUI.GetComponent<EventTrigger>().enabled = false;
        documentButtonUI.DOAnchorPos(new Vector2(_originalDocumentButtonPosition.x, 120f), 1f).SetEase(Ease.OutQuad);
    }

    private void ShowcardDrawer()
    {
        SoundManager.instance.PlaySFX(SoundManager.SFX.DrawerB);
        cardDrawerUI.DOAnchorPosY(0f, 1.25f).SetEase(Ease.OutQuad);
    }

    private void HidecardDrawer()
    {
        SoundManager.instance.PlaySFX(SoundManager.SFX.DrawerA);
        cardDrawerUI.DOAnchorPosY(186f, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            CoachmarkManager.instance.ShowCoachmark(Coachmark.Card);
        });
    }

    public void StartDialogue(TextAsset dialogueScript, string visitorName, Document doc, Sprite[] signs)
    {
        // initiate _story and protagonist_name, visitor_name is inside the ink script
        _story = new Story(dialogueScript.text);
        _story.variablesState["protagonist_name"] = _protagonistName;
        _story.variablesState["visitor_name"] = visitorName;

        // handle dialogue state specific changes
        _story.ObserveVariable("dialogue_state", (string varName, object newValue) =>
        {
            _dialogueState = newValue.ToString();

            if (newValue.ToString() == "get_document")
            {
                ShowDocument();
                ShowDocumentButton();
            }

            if (newValue.ToString() == "finish_document")
            {
                HideDocument();
            }

            if (newValue.ToString() == "card")
            {
                HidecardDrawer();
            }

            if (newValue.ToString() == "card_given")
            {
                ShowcardDrawer();
            }

            if (newValue.ToString() == "coachmark")
            {
                CoachmarkManager.instance.ShowCoachmark(Coachmark.Dialogue);
            }

            if (newValue.ToString() == "end")
            {
                EncounterManager.instance.HideCharacter();
                ClearAllUI();
                HideDocumentButton();
                HideDocument(true);
                // ShowcardDrawer();

                // if the 4th character we end the day
                if ((GlobalManager.instance.CurrentCharacterIndex + 1) % 4 == 0)
                {
                    wiggle1.Kill();
                    wiggle2.Kill();
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        DayManager.instance.AnalyzePlayerSubmissions();
                    });
                }
                else
                {
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        EncounterManager.instance.GenerateNextCharacter();
                    });
                }

                GlobalManager.instance.IncreaseCharacterIndex();
            }
        });

        // update character name when talking value changes
        _story.ObserveVariable("talking", (string varName, object newValue) =>
        {
            if (newValue.ToString() == _protagonistName)
            {
                characterName.color = ColorPalette.White;
                visitorChatUI.color = ColorPalette.White;
            }
            else
            {
                characterName.color = ColorPalette.Yellow;
                visitorChatUI.color = ColorPalette.Yellow;
            }

            characterName.text = newValue.ToString();
        });

        docSignImage.sprite = signs[GlobalManager.instance.CurrentCharacterIndex];
        UpdateDocumentDetails(doc);

        ContinueStory();
    }

    private void UpdateDocumentDetails(Document doc)
    {
        FieldInfo[] fields = typeof(Document).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            for (int i = 0; i < documentUI.childCount; i++)
            {
                Transform child = documentUI.GetChild(i);

                // Check if the child's name matches the target name
                if (child.name == $"{field.Name}_value")
                {
                    child.GetComponent<TextMeshProUGUI>().text = field.GetValue(doc).ToString();
                }
            }
        }
    }

    public void ContinueStory()
    {
        if (_story.canContinue)
        {
            NextVisitorDialogue();
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

    public void NextPlayerChoices()
    {
        if (_story.currentChoices.Count > 0)
        {
            // if text is X, dont show anything. used for card choices.
            _playerResponse1 = _story.currentChoices[0].text == "x" ? "" : _story.currentChoices[0].text;
            _playerResponse2 = _story.currentChoices.Count > 1 ? _story.currentChoices[1].text == "x" ? "" : _story.currentChoices[1].text : "";
        }
        else
        {
            _playerResponse1 = "";
            _playerResponse2 = "";
        }

        UpdateChoiceUI();
    }


    public void MakeChoice(int choiceIndex)
    {
        // TODO: add && isDialogueFinishTyping
        if (_dialogueState != "card" && _dialogueFinishTyping)
        {
            if (_story.canContinue || _story.currentChoices.Count > 0)
            {
                if (_story.currentChoices.Count > choiceIndex)
                {
                    _story.ChooseChoiceIndex(choiceIndex);
                    ContinueStory();
                }
            }
        }
    }

    private void ClearChoiceUI()
    {
        playerResponseUI1.text = "";
        playerResponseUI2.text = "";
    }

    private void ClearAllUI()
    {
        characterName.text = "";
        playerResponseUI1.text = "";
        playerResponseUI2.text = "";
    }

    private void UpdateChoiceUI()
    {
        // visitorChatUI.text = _visitorChat
        playerResponseUI1.text = _playerResponse1;

        if (_playerResponse2 == "Skip")
        {
            if (GlobalManager.instance.hasFinishedGame || GlobalManager.instance.hasRestartedDay)
            {
                playerResponseUI2.GetComponent<EventTrigger>().enabled = true;
                playerResponseUI2.text = _playerResponse2;
            }
            else
            {
                playerResponseUI2.text = "";
                playerResponseUI2.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            playerResponseUI2.GetComponent<EventTrigger>().enabled = true;
            playerResponseUI2.text = _playerResponse2;
        }
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

    public void ShowDocument()
    {
        SoundManager.instance.PlaySFX(SoundManager.SFX.DocumentSlideA, 0.9f, 1f);

        _documentShown = true;

        // Reset position and opacity
        documentUI.anchoredPosition = _originalDocumentUIPosition + new Vector3(600, 0, 0); // Offset start position
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Animate to visible position and opacity
        canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            // Enable interaction once fully visible
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });

        documentUI.DOAnchorPos(_originalDocumentUIPosition, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            CoachmarkManager.instance.ShowCoachmark(Coachmark.Document);
        }); // Move to original position

        documentButtonUI.GetComponentInChildren<TextMeshProUGUI>().text = ">";
    }

    public void HideDocument(bool muteSound = false)
    {
        if (!muteSound)
        {
            SoundManager.instance.PlaySFX(SoundManager.SFX.DocumentSlideA, 0.8f, 0.9f);
        }

        _documentShown = false;

        // Disable interaction immediately
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Animate to hidden position and opacity
        documentUI.DOAnchorPos(new Vector3(600, documentUI.anchoredPosition.y, 0), 1f).SetEase(Ease.InQuad); // Move downward
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.InQuad);

        documentButtonUI.GetComponentInChildren<TextMeshProUGUI>().text = "<";
    }

    public void OnDocumentShowHideClick()
    {
        if (_dialogueState != "start")
        {
            if (_documentShown)
            {
                HideDocument();
            }
            else
            {
                ShowDocument();
            }
        }
    }

    public void SubmitCard()
    {
        if (_dialogueState == "card" && EncounterManager.instance.SelectedCard != null)
        {
            if (EncounterManager.instance.SelectedCard == CardType.VIP)
            {
                _story.ChooseChoiceIndex(0);
            }
            else
            {
                _story.ChooseChoiceIndex(1);
            }

            ContinueStory();

            DayManager.instance.OnPlayerSubmitCard();
        }
    }

    private IEnumerator PlayRandomTypingSoundCoroutine(float totalDuration)
    {
        float interval = 0.14f;
        float elapsedTime = 0f;

        while (elapsedTime < totalDuration)
        {
            // Play the dialogue sound
            SoundManager.instance.PlaySFX(SoundManager.SFX.DialogueA, 0.4f, 0.5f);

            // Wait for the interval
            yield return new WaitForSeconds(interval);

            // Increment elapsed time
            elapsedTime += interval;
        }
    }
}
