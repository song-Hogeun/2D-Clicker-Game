using UnityEngine;

public class EnemyController : BaseCharacter
{
    [SerializeField] private EnemyStat stat;

    [SerializeField] private int kill_Gold;
    [SerializeField] private int kill_Exp;
    
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
        kill_Gold = stat.kill_Gold;
        kill_Exp = stat.kill_Exp;

        base.OnSpawn();
    }

    protected override void Die()
    {
        base.Die();
        
        DataManager.Instance.AddGold(kill_Gold);
        DataManager.Instance.AddExp(kill_Exp);
        Invoke("DieAction", 0.5f);
    }
    
    private void DieAction()
    {
        OnDead?.Invoke(this);
    }
}
