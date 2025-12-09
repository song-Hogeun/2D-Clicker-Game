using System;
using UnityEngine;
using static Constants;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IDamageable
{
    // 컴포넌트
    private Rigidbody2D rb;
    private Animator anim;
    
    // 기본 스탯
    private float maxHP = 100f;
    private float currentHP;
    
    // 공격 관련
    private float attackPower = 5f;
    private float attackDelay = 5f;
    private float attackDistance = 0.8f;
    private float currentAttackTimer;

    // 상태 변수
    private bool canMove = true;
    private bool isAttack = false;
    private bool isDeath = false;
    
    // 감지할 레이어
    [SerializeField] private LayerMask playerLayer;
    
    #region 생성 주기
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
        currentHP = maxHP;
    }
    
    private void Update()
    {
        CheckPlayer();
        
        if (isAttack)
            OnAttack();
    }
    
    private void FixedUpdate()
    {
        OnMove();
    }
    #endregion
    
    #region 행동 코드
    private void OnMove()
    {
        if (isAttack) return;
        anim.SetBool(MoveAnimParam, canMove);
    }
    
    private void OnAttack()
    {
        if (currentAttackTimer > 0f) 
        {
            currentAttackTimer -= Time.deltaTime;
            return;
        }
        
        anim.SetTrigger(AttackAnimParam);
        
        currentAttackTimer = attackDelay;
    }

    #endregion
    
    #region 기타 함수
    private void CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            rb.position,
            Vector2.left, // 방향은 상황에 맞게 수정
            attackDistance,
            playerLayer
        );

        if (hit.collider != null)
        {
            // Player 감지 → 이동 멈추고 공격 상태
            canMove = false;
            isAttack = true;
        }
        else
        {
            // Player 없음 → 이동 모드
            canMove = true;
            isAttack = false;
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
            
            anim.SetTrigger(DeathAnimParam);
        }
    }
    #endregion
}
