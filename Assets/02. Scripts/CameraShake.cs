using System.Collections;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    [Header("Shake Settings")]
    [SerializeField] private float defaultDuration = 0.15f;
    [SerializeField] private float defaultMagnitude = 0.2f;

    private Vector3 originPosition;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        base.Awake();
        originPosition = transform.localPosition;
    }

    /// <summary>
    /// 기본 설정으로 흔들기
    /// </summary>
    public void Shake()
    {
        Shake(defaultDuration, defaultMagnitude);
    }

    /// <summary>
    /// 커스텀 흔들기
    /// </summary>
    public void Shake(float duration, float magnitude)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            Vector2 randomOffset = Random.insideUnitCircle * magnitude;
            transform.localPosition = originPosition + (Vector3)randomOffset;

            yield return null;
        }

        transform.localPosition = originPosition;
        shakeRoutine = null;
    }
}