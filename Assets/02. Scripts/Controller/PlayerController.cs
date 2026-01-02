using UnityEngine;

[RequireComponent(typeof(DamageTextBinder))]
public class PlayerController : BaseCharacter
{
    private float offset = 0.8f;
    private Vector3 spawnPos;
    
    // 감지할 레이어
    [SerializeField] private LayerMask targetLayer;

    #region 생성 주기

    private void Awake()
    {
        base.Awake();
        spawnPos = new Vector3(
            rootPos.position.x, 
            rootPos.position.y + 0.8f, 0f);
        
        OnDamaged += (d, spawnPos) =>
            DamageTextManager.Instance.Spawn(d, spawnPos);
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
}
