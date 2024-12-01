using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool clicked;
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.MainMenu, 0, 0);
    }

    public void ProceedToStory()
    {
        if (!clicked)
        {
            GlobalManager.instance.SetActiveDayResult(DayResult.Success);
            LevelLoader.instance.LoadStoryScene();
            SoundManager.instance.PlaySFX(SoundManager.SFX.ButtonClickA);
        }

        clicked = true;
    }
}
