using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MaskImage : MonoBehaviour
{
    public Image uiImage;
    public float totalDuration = 10f;     
    public float blinkStartTime = 7f;     
    public float blinkInterval = 0.3f;   

    public void onUI()
    {
        uiImage.enabled = true;
        StartCoroutine(TimerRoutine());
    }

    IEnumerator TimerRoutine()
    {
        Debug.Log("Mask UI Triggered");
        yield return new WaitForSeconds(blinkStartTime);

        float blinkTime = totalDuration - blinkStartTime;
        float timer = 0f;

        while (timer < blinkTime)
        {
            uiImage.enabled = !uiImage.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
    
        uiImage.enabled = false;
    }
}
