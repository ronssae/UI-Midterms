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
    public float Pulse_Strength, Pulse_Duration, Scale_Duration;
    public Vector3 InitialPosition, TargetPosition;
    public float MoveDuration;

    public void Start()
    {
        DOTween.Init();
    }
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
        float targetAlpha = isFaded ? 1f : 0f;
        image.DOFade(targetAlpha, Fade_Duration);
        isFaded = !isFaded;
    }
    public void Pulse()
    {
        image.transform.DOScale(ScaleSize, Scale_Duration).SetEase(Ease.InOutBounce);
        image.DOFade(Pulse_Strength, Pulse_Duration).OnComplete(()=> Pulseback());
    }
    public void Pulseback()
    {
        image.transform.DOScale(ScaleSmallSize, Scale_Duration).SetEase(Ease.InOutBounce);
        image.DOFade(1f, Pulse_Duration);
    }
    public void Swing()
    {
        Sequence SwingSequence = DOTween.Sequence();

        SwingSequence.Append(image.rectTransform.DORotate(new Vector3(0f, 0f, 15f), 0.5f).SetEase(Ease.InOutQuad));
        SwingSequence.AppendInterval(0.1f);
        SwingSequence.Append(image.rectTransform.DORotate(new Vector3(0f, 0f, -15f), 0.5f).SetEase(Ease.InOutQuad));
        SwingSequence.AppendInterval(0.1f);
        SwingSequence.Append(image.rectTransform.DORotate(new Vector3(0f, 0f, 0), 0.5f).SetEase(Ease.InOutQuad));

    }
    public void Party()
    {
        Flip();
        Pulse();
        Shake();
    }
}
