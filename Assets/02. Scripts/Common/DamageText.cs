using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMesh text;
    [SerializeField] private float lifeTime = 1f;

    public void Init(float damage)
    {
        text.text = damage.ToString("F0");
        Destroy(gameObject, lifeTime);
    }
}