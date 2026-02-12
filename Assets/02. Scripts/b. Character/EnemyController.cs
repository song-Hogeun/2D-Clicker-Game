using UnityEngine;

public class EnemyController : BaseCharacter
{
    [SerializeField] private EnemyStat stat;
    
    [Header("Enemy Type")]
    [SerializeField] private bool isBoss;

    [Header("Enemy Name")]
    [SerializeField] private string enemyName;
    
    public string EnemyName => enemyName;
    public bool IsBoss => isBoss;
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    
    #region 생성 주기

    private void Awake()
    {
        base.Awake();
    }
    
    #endregion
  
    protected override Vector2 GetMoveDirection()
    {
        return Vector2.left;
    }

    protected override LayerMask GetTargetLayer()
    {
        return targetLayer;
    }
    
    public override void OnSpawn()
    {
        maxHP = stat.maxHP;
        speed = stat.speed;
        attackPower = stat.attackPower;
        attackDelay = stat.attackDelay;
        attackDistance = stat.attackDistance;
        name = stat.enemyName;

        base.OnSpawn();
    }

    protected override void Die()
    {
        base.Die();
        
        Invoke("DieAction", 0.5f);
    }
    
    private void DieAction()
    {
        OnDead?.Invoke(this);
    }
}
