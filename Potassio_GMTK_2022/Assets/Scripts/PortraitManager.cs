using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    [SerializeField]
    private Image portraitImg;

    [SerializeField]
    private List<Sprite> portraitImages;

    public IEnumerator SwitchPortrait(int index, float speed)
    {
        if (portraitImg.sprite != null)
            yield return AnimationUtils.FadeImgOut(portraitImg, speed);
        if (portraitImages.Count <= 0 || index == -1)
        {
            portraitImg.sprite = null;
            portraitImg.enabled = false;
            yield break;
        }
        portraitImg.enabled = true;
        portraitImg.sprite = portraitImages[index];
        yield return AnimationUtils.FadeImgIn(portraitImg, speed);
    }
}
