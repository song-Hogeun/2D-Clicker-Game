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
    private float attackDistance = 1.2f;
    private float currentAttackTimer;

    // 상태 변수
    private bool canMove = false;
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
        if (isDeath)
            return;
        
        CheckPlayer();  // 플레이어가 주변에 있는지 체크
        Think();        // 이후 행동
    }
    #endregion
    
    #region 행동 코드

    private void Think()
    {
        if (CheckPlayer())
        {
            anim.SetBool(MoveAnimParam, true);
        }
        else
        {
            anim.SetBool(MoveAnimParam, false);
            OnAttack();
        }
        
        Debug.Log(CheckPlayer());
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
    private bool CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            rb.position,
            Vector2.left, // 방향은 상황에 맞게 수정
            attackDistance,
            playerLayer
        );
        
        Debug.DrawRay(transform.position, Vector2.left * attackDistance, Color.yellow); 

        // Player 감지 → 이동 멈추고 공격 상태
        if (hit.collider != null) canMove = false;
        // Player 없음 → 이동 모드
        else canMove = true;

        return canMove;
    }
    
    // 데미지 처리 함수
    public void TakeDamage(float damage)
    {
        if (isDeath) return;
        
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
