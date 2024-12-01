using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CoachmarkManager : SerializedMonoBehaviour
{
    public static CoachmarkManager instance;

    // ---------------------------- //
    [SerializeField] private RectTransform _coachMarkUI;

    [DictionaryDrawerSettings(KeyLabel = "SFX", ValueLabel = "AudioSource")]
    [SerializeField]
    private Dictionary<Coachmark, CanvasGroup> _coachmarks;

    private CanvasGroup _canvasGroup;

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
        foreach (var coachmark in _coachmarks)
        {
            coachmark.Value.blocksRaycasts = false;
        }

        _canvasGroup = _coachMarkUI.GetComponent<CanvasGroup>();
    }

    public void ShowCoachmark(Coachmark coachmark)
    {
        if (GlobalManager.instance.CoachmarkShown.TryGetValue(coachmark, out bool isShown) && isShown)
        {
            return;
        }

        if (_coachmarks.TryGetValue(coachmark, out CanvasGroup canvasGroup))
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1f, 0.5f);
            ShowGroupCoachmark();
            GlobalManager.instance.CoachmarkShown[coachmark] = true;
        }
        else
        {
            Debug.LogWarning($"Coachmark {coachmark} not found in the dictionary.");
        }
    }

    public void HideCoachmarkString(string coachmarkName)
    {
        if (System.Enum.TryParse(coachmarkName, out Coachmark coachmark))
        {
            HideCoachmark(coachmark);
        }
    }

    public void HideCoachmark(Coachmark coachmark)
    {
        if (_coachmarks.TryGetValue(coachmark, out CanvasGroup canvasGroup))
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0f, 0.5f);
            HideGroupCoachmark();

            if (coachmark == Coachmark.Code)
            {
                DOVirtual.DelayedCall(1.25f, () =>
                {
                    EncounterManager.instance.GenerateNextCharacter();
                });
            }
        }
        else
        {
            Debug.LogWarning($"Coachmark {coachmark} not found in the dictionary.");
        }
    }

    private void ShowGroupCoachmark()
    {
        _canvasGroup.DOFade(1f, 0.5f).OnComplete(() =>
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        });
    }

    private void HideGroupCoachmark()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0f, 0.5f);
    }
}
