using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Text;
using Sirenix.OdinInspector;

public class CodeManager : SerializedMonoBehaviour
{
    public static CodeManager instance;

    // ---------------------------- //

    public TextMeshProUGUI codeText;

    // ---------------------------- //

    [Title("Rich Text Entries")]
    [TextArea(3, 10)]
    [SerializeField, ListDrawerSettings()]
    private List<string> _dailyCodes = new();

    [Title("Rich Text Entries")]
    [TextArea(3, 10)]
    [SerializeField, ListDrawerSettings()]
    private List<string> _finalDailyCodes = new();

    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
    private List<int> revealOrder;

    private bool _textIsColored = false;

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

    public Tween GenerateDailyCode()
    {
        StartCoroutine(PlayCodeSoundCoroutine(1.25f));
        return TypeTextWithGlitch(codeText, _dailyCodes[GlobalManager.instance.DayCounter - 1], 2.4f);
    }


    private Tween TypeTextWithGlitch(TextMeshProUGUI textComponent, string fullText, float totalDuration)
    {
        textComponent.text = "";

        // Initialize reveal order for randomization
        revealOrder = new List<int>();
        for (int i = 0; i < fullText.Length; i++)
        {
            revealOrder.Add(i);
        }

        // Shuffle the reveal order
        Shuffle(revealOrder);

        return DOTween.To(() => 0, x => SetTextWithGlitch(textComponent, fullText, x), fullText.Length, totalDuration)
                      .SetEase(Ease.OutExpo);
    }

    private void SetTextWithGlitch(TextMeshProUGUI textComponent, string fullText, float progress)
    {
        int visibleCharacters = Mathf.FloorToInt(progress); // Number of characters to display correctly
        StringBuilder currentText = new StringBuilder(new string(' ', fullText.Length)); // Create a placeholder with spaces

        // Reveal characters in the shuffled order
        for (int i = 0; i < visibleCharacters; i++)
        {
            int index = revealOrder[i];
            currentText[index] = fullText[index];
        }

        // Replace remaining characters with random ones
        for (int i = visibleCharacters; i < fullText.Length; i++)
        {
            int index = revealOrder[i];
            currentText[index] = chars[Random.Range(0, chars.Length)];
        }

        if (progress == fullText.Length)
        {
            textComponent.text = currentText.ToString();
            // if (!_textIsColored)
            // {
            //     _textIsColored = true;
            //     DOVirtual.DelayedCall(0.5f, () =>
            //     {
            //         codeText.text = _finalDailyCodes[GlobalManager.instance.DayCounter - 1];
            //     });
            // }
        }
        else
        {
            textComponent.text = currentText.ToString();
        }
    }



    private void Shuffle<T>(List<T> list)
    {
        // Fisher-Yates shuffle algorithm
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private IEnumerator PlayCodeSoundCoroutine(float totalDuration)
    {
        float interval = 0.125f;
        float elapsedTime = 0f;

        AudioSource typingAudio = gameObject.GetComponent<AudioSource>();

        while (elapsedTime < totalDuration)
        {
            // Wait for the interval
            yield return new WaitForSeconds(interval);

            // Play the typing sound with random pitch
            typingAudio.Play();

            // Increment elapsed time
            elapsedTime += interval;
        }
    }
}
