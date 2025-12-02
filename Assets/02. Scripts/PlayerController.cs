using System;
using UnityEngine;

// 필수 컴포넌트
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDamageable
{
    // 컴포넌트
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

    // 기본 스탯
    private float applySpeed;
    private float moveSpeed = 3f;
    private float maxHP = 100f;
    private float currentHP;
    
    // 공격 관련
    private float attackPower = 5f;
    private float attackDelay = 2f;
    private float currentAttackTimer;

    // 상태 변수
    private bool canMove = true;
    private bool isDeath = false;

    #region 생성 주기
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        applySpeed = moveSpeed;
        currentHP = maxHP;
    }

    private void Update()
    {
        CheckMonster();
    }

    private void FixedUpdate()
    {
        OnMove();
    }
    #endregion

    #region 행동 코드
    // 이동 코드
    private void OnMove()
    {
        if (!canMove)
        {
            anim.SetBool(MoveAnimParam, false);    
            return;
        }
        
        rb.linearVelocityX = applySpeed;
        anim.SetBool(MoveAnimParam, true);
    }
    
    // 공격 함수
    private void OnAttack()
    {
        if (!canMove)
        {
            rb.linearVelocityX = 0;
            anim.SetTrigger(AttackAnimParam);
        }
    }
    #endregion

    #region 기타 함수
    // 앞에 몬스터가 있는가
    private void CheckMonster()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right, 1);
        
        // 앞에 콜라이더가 있을 시
        if (hit.collider != null)
        {
            // 이동을 멈추고 공격 실행
            canMove = false;
            OnAttack();
        }

        else
        {
            canMove = true;
        }
    }
    
    // 데미지 처리 함수
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        
        anim.SetTrigger(DamageAnimParam);

        if (currentHP <= 0)
        {
            isDeath = true;
            
            rb.linearVelocityX = 0;
            anim.SetTrigger(DeathAnimParam);
        }
    }
    #endregion
}
