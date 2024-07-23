using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    float fadeTime = 0.5f;
    WaitForSeconds fadeInTime = new WaitForSeconds(0.3f);
    bool isStart = false;

    public void SetStart(bool _isStart) { isStart = _isStart; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup)
        {
            Debug.Log("canvasGroup이 null입니다.");
            return;
        }
        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        StartFadeInOut();
    }

    void StartFadeInOut()
    {
        if (isStart)
            StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeTime);
            yield return null;
            time += Time.deltaTime;
        }
        canvasGroup.alpha = 1f;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        yield return fadeInTime;
        float time = 0f;
        while (time < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeTime);
            yield return null;
            time += Time.deltaTime;
        }
        canvasGroup.alpha = 0f;
        isStart = false;
    }

    void KeyController()
    {
        if (Input.GetKeyDown(KeyCode.G))
            StartFadeInOut();
    }
}
