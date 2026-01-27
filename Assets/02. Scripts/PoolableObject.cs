using UnityEngine;

public abstract class PoolableObject : MonoBehaviour, IPoolable
{
    public abstract void OnSpawn();
    public abstract void OnDespawn();
}