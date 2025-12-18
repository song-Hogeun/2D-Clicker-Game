using System;
using UnityEngine;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private float moveY = 1.0f;
    [SerializeField] private float duration = 1.0f;

    public void Init(float damage, Transform pos)
    {
        textMesh.text = damage.ToString("0");

        Color startColor = textMesh.color;
        startColor.a = 1f;
        textMesh.color = startColor;

        transform.position = pos.position;

        // 위로 이동
        transform.DOMoveY(pos.position.y + moveY, duration);

        // 알파값 직접 제어
        DOVirtual.Float(1f, 0f, duration, a =>
        {
            Color c = textMesh.color;
            c.a = a;
            textMesh.color = c;
        }).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}