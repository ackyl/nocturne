using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;
using System;

public class SoundManager : SerializedMonoBehaviour
{
    public enum SFX
    {
        DialogueA,
        DialogueB,
        TypingA,
        TypingB,
        ButtonClickA,
        ButtonClickB,
        ButtonClickC,
        TransitionA,
        TransitionB,
        EnterBell,
        StepA,
        StepB,
        DocumentSlideA,
        DocumentSlideB,
        GunShot,
        SlideA,
        SlideB,
        DrawerA,
        DrawerB,
        GunReload,
        SplashA,
        SplashB
    }

    public enum BGM
    {
        MainMenu,
        Spectre,
        LobbyA,
        LobbyB,
        LobbyC,
        Clairette,
        EndingA,
        EndingB,
        EndingC
    }

    // Singleton instance
    public static SoundManager instance;

    [DictionaryDrawerSettings(KeyLabel = "SFX", ValueLabel = "AudioSource")]
    [SerializeField]
    private Dictionary<SFX, AudioSource> sfxDictionary;

    [DictionaryDrawerSettings(KeyLabel = "BGM", ValueLabel = "AudioSource")]
    [SerializeField]
    private Dictionary<BGM, AudioSource> bgmDictionary;

    private AudioSource currentBGM; // Currently playing BGM
    private Tween currentBGMTween; // Tween reference for current BGM transitions

    void Awake()
    {
        // Set up the singleton instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persists between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Play an SFX sound
    public void PlaySFX(SFX sfx, float minPitch = 0, float maxPitch = 0)
    {
        if (sfxDictionary.TryGetValue(sfx, out AudioSource audioSourceTemplate))
        {
            // Create a new instance of AudioSource by cloning the base AudioSource
            AudioSource newSource = Instantiate(audioSourceTemplate, transform);
            if (minPitch != 0 && maxPitch != 0)
            {
                newSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            }
            newSource.Play();

            // Use DoTween to destroy the AudioSource after it finishes
            if (!newSource.loop)
            {
                newSource.DOFade(0, newSource.clip.length).OnComplete(() =>
                {
                    Destroy(newSource.gameObject);
                });
            }
        }
        else
        {
            Debug.LogWarning($"SFX {sfx} not found in SoundManager.");
        }
    }

    // Play a BGM with fade-in and start delay
    public void PlayBGM(BGM bgm, float fadeInDuration = 1f, float startDelay = 0f)
    {
        if (bgmDictionary.TryGetValue(bgm, out AudioSource audioSourceTemplate))
        {
            if (currentBGM != null)
            {
                StopBGM(1f); // Fade out current BGM before starting a new one
            }

            // Create a new BGM instance
            currentBGM = Instantiate(audioSourceTemplate, transform);
            currentBGM.volume = 0f;

            // Use DoTween to fade in after a delay
            currentBGM.DOFade(audioSourceTemplate.volume, fadeInDuration)
                .SetDelay(startDelay)
                .OnStart(() => currentBGM.Play());
        }
        else
        {
            Debug.LogWarning($"BGM {bgm} not found in SoundManager.");
        }
    }

    // Stop the current BGM with a fade-out duration
    public void StopBGM(float fadeOutDuration = 1f, System.Action onComplete = null)
    {
        if (currentBGM != null)
        {
            currentBGMTween?.Kill();

            currentBGMTween = currentBGM.DOFade(0, fadeOutDuration).OnComplete(() =>
            {
                currentBGM.Stop();
                Destroy(currentBGM.gameObject);
                currentBGM = null;

                // Invoke the callback
                onComplete?.Invoke();
            });
        }
        else
        {
            // Invoke the callback immediately if no BGM is playing
            onComplete?.Invoke();
        }
    }


    // Stop all sounds instantly (optional global mute)
    public void StopAllSounds()
    {
        foreach (Transform child in transform)
        {
            AudioSource source = child.GetComponent<AudioSource>();
            if (source)
            {
                source.DOFade(0, 0.5f).OnComplete(() =>
                {
                    source.Stop();
                    Destroy(source.gameObject);
                });
            }
        }
    }

    public void PlayClickSound(int index)
    {
        switch (index)
        {
            case 0:
                PlaySFX(SFX.ButtonClickA);
                break;
            case 1:
                PlaySFX(SFX.ButtonClickB);
                break;
            case 2:
                PlaySFX(SFX.ButtonClickC);
                break;
            default:
                PlaySFX(SFX.ButtonClickA);
                break;
        };
    }

    internal void StopBGM(Action value)
    {
        throw new NotImplementedException();
    }
}
