using UnityEngine;

public interface IDamageVisualListener
{
    void OnDamaged(float damage, Vector3 pos);
}