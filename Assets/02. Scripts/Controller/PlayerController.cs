using UnityEngine;

[RequireComponent(typeof(DamageTextBinder))]
public class PlayerController : BaseCharacter
{
    #region 생성 주기

    private void Awake()
    {
        base.Awake();
        OnDamaged += (d, v) =>
            DamageTextManager.Instance.Spawn(d, v);
    }

    #endregion
    
    protected override Vector2 GetMoveDirection()
    {
        return Vector2.right;
    }
    
    protected override LayerMask GetTargetLayer()
    {
        return targetLayer;
    }
    
    public override void OnSpawn()
    {
        base.OnSpawn();
        // Enemy 리셋
    }
    
    protected override void Die()
    {
        base.Die();
        PoolManager.Instance.Release(this);
    }
}
