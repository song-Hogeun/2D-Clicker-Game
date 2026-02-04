using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectManager : Singleton<ScreenEffectManager>
{
    [Header("Overlay")]
    [SerializeField] private Image overlayImage; // 전체 화면 덮는 Image (Canvas)
    
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 0.5f;

    private Coroutine currentEffect;

    private void Awake()
    {
        base.Awake();
        
        // 시작 시 투명
        SetAlpha(0f);
    }

    /// <summary>
    /// 보스 등장 연출
    /// </summary>
    public void PlayBossIntroEffect()
    {
        if (currentEffect != null)
            StopCoroutine(currentEffect);

        currentEffect = StartCoroutine(BossIntroSequence());
    }

    /// <summary>
    /// 화면 암전
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(Fade(1f));
    }

    /// <summary>
    /// 암전 해제
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(Fade(0f));
    }

    /// <summary>
    /// 컬러 플래시
    /// </summary>
    public void Flash(Color color, float duration)
    {
        if (currentEffect != null)
            StopCoroutine(currentEffect);

        currentEffect = StartCoroutine(FlashRoutine(color, duration));
    }

    /* =========================
     * Boss Intro Sequence
     * ========================= */

    private IEnumerator BossIntroSequence()
    {
        // 순간 슬로우
        Time.timeScale = 0.05f;

        // 붉은 플래시
        overlayImage.color = new Color(1f, 0f, 0f, 0.6f);
        yield return new WaitForSecondsRealtime(0.15f);

        // 암전
        yield return FadeRoutine(1f);

        // 카메라 흔들림
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(0.25f, 0.2f);
        }

        yield return new WaitForSecondsRealtime(0.3f);

        // 암전 해제
        yield return FadeRoutine(0f);

        // 시간 정상화
        Time.timeScale = 1f;
    }

    /* =========================
     * Internal Effects
     * ========================= */

    private IEnumerator Fade(float targetAlpha)
    {
        yield return FadeRoutine(targetAlpha);
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = overlayImage.color.a;
        float elapsed = 0f;

        Color color = overlayImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / fadeDuration;

            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            overlayImage.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        overlayImage.color = color;
    }

    private IEnumerator FlashRoutine(Color color, float duration)
    {
        overlayImage.color = color;
        yield return new WaitForSecondsRealtime(duration);
        SetAlpha(0f);
    }

    /* =========================
     * Utils
     * ========================= */

    private void SetAlpha(float alpha)
    {
        Color c = overlayImage.color;
        c.a = alpha;
        overlayImage.color = c;
    }
}
