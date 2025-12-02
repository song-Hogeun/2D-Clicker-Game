using System;
using UnityEngine;

// 필수 컴포넌트
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Animator anim;

    /// <summary>
    /// Animator 파라미터
    /// </summary>
    private readonly string MoveAnimParam = "1_Move";
    private readonly string AttackAnimParam = "2_Attack";
    private readonly string DamageAnimParam = "3_Damage";
    private readonly string DeathAnimParam = "4_Death";
    private readonly string DebuffAnimParam = "5_Debuff";

    private float applySpeed;
    private float moveSpeed = 3f;
    private float attackPower = 5f;

    private bool canMove = true;
    private bool isDeath = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        applySpeed = moveSpeed;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    // 이동 코드
    private void OnMove()
    {
        if (!canMove) return;
        
        rb.linearVelocityX = applySpeed;
        anim.SetBool(MoveAnimParam, true);
    }

    // 앞에 몬스터가 있는가
    private void CheckMonster()
    {
        
    }

    private void OnAttack()
    {
        if (!canMove)
        {
            anim.SetTrigger(AttackAnimParam);
        }
    }

    public void TakeDamage(float damage)
    {
        
    }
}
