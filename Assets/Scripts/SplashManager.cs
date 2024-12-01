using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class SplashManager : MonoBehaviour
{
    [SerializeField] private RectTransform _sun;
    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        _sun.DOAnchorPosY(0, 0);
        _text.DOFade(0, 0);

        SoundManager.instance.PlaySFX(SoundManager.SFX.SplashA);
        _sun.DOAnchorPosY(60, 1f).OnComplete(() =>
        {
            SoundManager.instance.PlaySFX(SoundManager.SFX.SplashB);

            _text.DOFade(1, 1f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(2f, () =>
                {
                    LevelLoader.instance.LoadMainMenu();
                });
            });
        });
    }
}
