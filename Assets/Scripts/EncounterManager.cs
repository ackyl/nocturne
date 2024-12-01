using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance;

    // ---------------------------- //

    public RectTransform characterUI;

    public RectTransform[] cardUI;

    public Sprite[] characterSprites;

    public TextAsset[] characterDialogues;

    public Sprite[] documentSignSprites;

    public CardType? SelectedCard { get; private set; } = null;

    // ---------------------------- //

    private int _activeIndex = 99;

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

    public void GenerateVisitor(Character character)
    {
        // change sprite
        characterUI.GetComponent<Image>().sprite = characterSprites[GlobalManager.instance.CurrentCharacterIndex];

        // reset position
        characterUI.anchoredPosition = new Vector3(-500, -27, 0);

        SoundManager.instance.PlaySFX(SoundManager.SFX.EnterBell);
        SoundManager.instance.PlaySFX(SoundManager.SFX.StepA);

        // get into position
        characterUI.DOAnchorPos(new Vector3(0, -27, 0), 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            DialogueManager.instance.StartDialogue(characterDialogues[GlobalManager.instance.CurrentCharacterIndex], character.fullName, character.doc, documentSignSprites);
        });
    }

    public void GenerateNextCharacter()
    {
        GenerateVisitor(GlobalManager.instance.CharacterList[GlobalManager.instance.CurrentCharacterIndex]);
        _activeIndex = 99;
        CardUnhover(0);
        CardUnhover(1);
    }

    public void HideCharacter()
    {
        SoundManager.instance.PlaySFX(SoundManager.SFX.StepA);
        characterUI.DOAnchorPos(new Vector3(500, -27, 0), 1f).SetEase(Ease.InQuad);
    }

    public void CardClick(int index)
    {
        if (index == _activeIndex)
        {
            CardUnhover(_activeIndex);
            _activeIndex = 99;
        }
        else
        {
            _activeIndex = index;
            CardHover(_activeIndex);
            CardUnhover(_activeIndex == 0 ? 1 : 0);
        }

        if (_activeIndex == 0)
        {
            SelectedCard = CardType.Standard;
        }
        else if (_activeIndex == 1)
        {
            SelectedCard = CardType.VIP;
        }
        else
        {
            SelectedCard = null;
        }
    }

    public void CardHover(int index)
    {
        RectTransform card = cardUI[index];
        card.DOAnchorPosY(42, 0.2f).SetEase(Ease.Linear);
    }

    public void CardUnhover(int index)
    {
        RectTransform card = cardUI[index];

        if (_activeIndex != index)
        {
            card.DOAnchorPosY(24, 0.2f).SetEase(Ease.Linear);
        }
    }
}
