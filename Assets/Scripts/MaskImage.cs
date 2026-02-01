using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaskImage : MonoBehaviour
{
    public Image uiImage;
    public float blinkInterval = 0.5f;
    public float totalDuration = 10f;

    public void OnEnable()
    {
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        float timer = 0f;

        while (timer < totalDuration)
        {
            uiImage.enabled = !uiImage.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // Ensure it's fully hidden at the end
        uiImage.enabled = false;
    }
}
