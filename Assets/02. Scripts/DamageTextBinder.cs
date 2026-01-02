using UnityEngine;

public class DamageTextBinder : MonoBehaviour, IDamageVisualListener
{
    public void OnDamaged(float damage, Vector3 pos)
    {
        DamageTextManager.Instance.Spawn(damage, pos);
    }
}