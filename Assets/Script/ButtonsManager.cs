using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ButtonsManager : MonoBehaviour
{
    public Image image;
    private bool isZoomOut = false;
    public float Zoom_Value;
    private bool isFlipped = false;
    public float Flip_Degree;
    private bool isShaking = false;
    public float Shake_Duration, Shake_Strength;
    private bool isFaded = false;
    public float Fade_Duration;
    public Vector3 ScaleSize, ScaleSmallSize;
    public float Pulse_Duration, Scale_Duration;


    public void Zoom()
    {
        float targetScale = isZoomOut ? Zoom_Value : 0f;
        image.transform.DOScale(targetScale, 0.25f);
        isZoomOut = !isZoomOut;
    }
    public void Flip()
    {
        float flipValue = isFlipped ? 0f : Flip_Degree;
        Vector3 targetRotation = new Vector3(flipValue, 0f, 0f);
        image.transform.DORotate(targetRotation, 0.25f);
        isFlipped = !isFlipped;
    }
    public void Shake()
    {
        if (!isShaking)
        {
            image.rectTransform.DOShakeAnchorPos(Shake_Duration, Shake_Strength).OnComplete(() => isShaking = false);
            isShaking = true;
        }
    }
    public void Fade()
    {
        if (!isFaded)
        {
            // Fade the image in
            image.DOFade(1f, Fade_Duration)
                .OnComplete(() => isFaded = true);
        }
        else
        {
            // Fade the image out
            image.DOFade(0, Fade_Duration)
                .OnComplete(() => isFaded = false);
        }
    }
    public void Pulse()
    {
        image.transform.DOScale(ScaleSize, Scale_Duration).SetEase(Ease.InSine);
        image.DOFade(0.5f, Pulse_Duration).OnComplete(()=> Pulseback());
    }
    public void Pulseback()
    {
        image.transform.DOScale(ScaleSmallSize, Scale_Duration).SetEase(Ease.InSine);
        image.DOFade(1f, Pulse_Duration);
    }
}
