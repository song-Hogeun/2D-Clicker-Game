using System;
using UnityEngine;

public class UpgradePopup : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    // 추가 능력치
    private float plusPower;
    private float plusAttackSpeed;
    private float plusMoveSpeed;
    private float plusCriticalChance;
    private float plusCriticalDamage;
    
    // 능력치 강화 한계치
    private float powerLimit;
    private float attackSpeedLimit;
    private float moveSpeedLimit;
    private float criticalChanceLimit;
    private float criticalDamageLimit;

    // 강화 비용
    private float cost;

    /// <summary>
    /// 공격력 강화
    /// </summary>
    private void UpgradePower()
    {
        
    }

    /// <summary>
    /// 공격속도 강화
    /// </summary>
    private void UpgradeAttackSpeed()
    {
        
    }

    /// <summary>
    /// 이동속도 강화
    /// </summary>
    private void UpgradeMoveSpeed()
    {
        
    }

    /// <summary>
    /// 크리티컬 확률 강화
    /// </summary>
    private void UpgradeCriticalChance()
    {
        
    }
    
    /// <summary>
    /// 크리티컬 데미지 강화
    /// </summary>
    private void UpgradeCriticalDamage()
    {
        
    }
}
