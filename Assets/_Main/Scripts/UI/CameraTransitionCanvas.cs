using UnityEngine;
using UnityEngine.UI;

public class CameraTransitionCanvas : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionSpeed = 3f;

    private Color targetColor;
    private float targetFillAmount;

    private void Awake()
    {
        targetColor = transitionImage.color;
        targetFillAmount = transitionImage.fillAmount;
    }

    private void Update()
    {
        transitionImage.fillAmount = Mathf.Lerp(transitionImage.fillAmount, targetFillAmount, Time.deltaTime * transitionSpeed);
        transitionImage.color = Color.Lerp(transitionImage.color, targetColor, Time.deltaTime * transitionSpeed);
    }

    public void ForwardTransition()
    {
        targetFillAmount = 1f;
        targetColor = new Color(0f, 0f, 0f, 1f);
    }

    public void ReverseTransition()
    {
        targetFillAmount = 0f;
        targetColor = new Color(0f, 0f, 0f, 0f);
    }
}
