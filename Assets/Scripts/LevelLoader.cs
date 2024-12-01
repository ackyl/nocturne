using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

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
    }

    void Start()
    {
        TransitionManager.instance.FadeOut();
    }

    public void LoadMainMenu(bool finishedGame = false)
    {
        if (finishedGame)
        {
            GlobalManager.instance.hasFinishedGame = true;
        }

        SoundManager.instance.StopBGM(0.5f, () =>
        {
            TransitionManager.instance.FadeIn(() =>
                    {
                        Debug.Log(">> Loading Story Scene..");
                        SceneManager.LoadScene(1);
                        TransitionManager.instance.FadeOut();
                    });
        });
    }

    public void LoadStoryScene()
    {
        SoundManager.instance.StopBGM(0.5f, () =>
        {
            TransitionManager.instance.FadeIn(() =>
                    {
                        Debug.Log(">> Loading Story Scene..");
                        SceneManager.LoadScene(2);
                        TransitionManager.instance.FadeOut();
                    });
        });
    }

    public void LoadGameplayScene(bool incrementDay = true)
    {
        SoundManager.instance.StopBGM(0.5f, () =>
        {
            TransitionManager.instance.SlideIn(() =>
            {
                if (incrementDay)
                {
                    GlobalManager.instance.hasRestartedDay = false;
                    GlobalManager.instance.IncreaseDayCounter();
                }
                else
                {
                    GlobalManager.instance.hasRestartedDay = true;
                }

                Debug.Log($">> Loading Gameplay Scene. Day {GlobalManager.instance.DayCounter}");
                SceneManager.LoadScene(3);
                TransitionManager.instance.SlideOut();
            });
        });
    }
}
