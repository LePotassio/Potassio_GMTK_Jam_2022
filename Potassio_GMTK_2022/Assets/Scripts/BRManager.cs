using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BRManager : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImg;

    [SerializeField]
    private List<Sprite> backgroundImages;

    public IEnumerator SwitchBackground(int index, float speed)
    {
        if (backgroundImg.sprite != null && backgroundImg.color.a > 0)
            yield return AnimationUtils.FadeImgOut(backgroundImg, speed);
        if (backgroundImages.Count <= 0 || index == -1)
        {
            backgroundImg.sprite = null;
            yield break;
        }
        backgroundImg.sprite = backgroundImages[index];
        yield return AnimationUtils.FadeImgIn(backgroundImg, speed);
    }
}
