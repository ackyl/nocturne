using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    // ---------------------------- //

    [SerializeField] private Image simpleFadeImage; // Reference to the UI Image
    [SerializeField] private float fadeDuration = 0.425f; // Duration of fade in/out

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

        if (simpleFadeImage == null)
        {
            Debug.LogError("Transition Image is not assigned!");
        }
    }

    void Start()
    {
        simpleFadeImage.color = ColorPalette.Black;
    }

    /// Fades the screen to black.
    public void FadeIn(System.Action onComplete = null)
    {
        simpleFadeImage.gameObject.SetActive(true); // Ensure it's active

        simpleFadeImage.rectTransform.anchoredPosition = Vector2.zero;

        simpleFadeImage.DOFade(0f, 0);

        simpleFadeImage.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    /// Fades the screen from black to transparent.
    public void FadeOut(System.Action onComplete = null)
    {
        simpleFadeImage.DOFade(1f, 0);

        simpleFadeImage.rectTransform.anchoredPosition = Vector2.zero;

        simpleFadeImage.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            simpleFadeImage.gameObject.SetActive(false); // Disable when not visible
            onComplete?.Invoke();
        });
    }

    /// Combined Fade Out and Fade In with delay in between.
    public void FadeOutIn(float delay, System.Action onComplete = null)
    {
        FadeIn(() =>
        {
            Invoke(nameof(FadeOutWithCallback), delay);
            void FadeOutWithCallback()
            {
                FadeOut(onComplete);
            }
        });
    }

    public void PixelReveal(System.Action onComplete = null)
    {
        FadeOut();

        int gridSize = 60; // Number of rows/columns
        float cellSize = simpleFadeImage.rectTransform.rect.width / gridSize;
        Transform parent = simpleFadeImage.transform;

        float pixelDuration = 0.5f;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                // Create square
                GameObject pixel = new GameObject("Pixel", typeof(RectTransform), typeof(Image));
                pixel.transform.SetParent(parent);
                RectTransform rt = pixel.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellSize, cellSize);
                rt.anchorMin = rt.anchorMax = Vector2.zero;
                rt.anchoredPosition = new Vector2(i * cellSize, j * cellSize);

                Image img = pixel.GetComponent<Image>();
                img.color = Color.black;
                img.DOFade(0f, pixelDuration).SetDelay(Random.Range(0f, pixelDuration)).OnComplete(() =>
                {
                    Destroy(pixel);
                });
            }
        }

        DOVirtual.DelayedCall(pixelDuration, () =>
        {
            onComplete?.Invoke();
        });
    }

    public void SlideOut(System.Action onComplete = null)
    {
        simpleFadeImage.gameObject.SetActive(true);
        simpleFadeImage.rectTransform.anchoredPosition = Vector2.zero;

        SoundManager.instance.PlaySFX(SoundManager.SFX.SlideB);

        simpleFadeImage.rectTransform.DOAnchorPos(new Vector2(0, Screen.height), 0.8f).OnComplete(() =>
        {
            simpleFadeImage.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    public void SlideIn(System.Action onComplete = null)
    {
        simpleFadeImage.gameObject.SetActive(true);
        simpleFadeImage.rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
        simpleFadeImage.DOFade(1f, 0);

        SoundManager.instance.PlaySFX(SoundManager.SFX.SlideA);

        simpleFadeImage.rectTransform.DOAnchorPos(new Vector2(0, 0), fadeDuration).OnComplete(() =>
        {
            simpleFadeImage.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    public void ShrinkFade(System.Action onComplete = null)
    {
        simpleFadeImage.gameObject.SetActive(true);
        // simpleFadeImage.DOFade(0f, fadeDuration);
        simpleFadeImage.rectTransform.DOScale(0f, fadeDuration).OnComplete(() =>
        {
            simpleFadeImage.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }
}
