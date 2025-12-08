using System;
using UnityEngine;
using static Constants;

// 필수 컴포넌트
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDamageable
{
    // 플레이어 전투 타입
    public enum EPlayerType
    {
        Warrior,
        Archor,
        Wizard
    }
    
    // 컴포넌트
    private Rigidbody2D rb;
    private Animator anim;

    // 기본 스탯
    private float applySpeed;
    private float moveSpeed = 3f;
    private float maxHP = 100f;
    private float currentHP;
    
    // 공격 관련
    private float attackPower = 15f;
    private float attackDelay = 2f;
    private float attackDistance;
    private float currentAttackTimer;

    // 상태 변수
    private bool canMove = true;
    private bool isDeath = false;

    public float AttackPower => attackPower;

    // 플레이어 전투 타입
    [SerializeField] private EPlayerType playerType;
    
    // 감지할 레이어
    [SerializeField] private LayerMask monsterLayer;

    #region 생성 주기
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        InitStat(playerType);
        
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
    
    private void OnAttack()
    {
        if (currentAttackTimer >= 0) 
        {
            currentAttackTimer -= Time.deltaTime;
            return;
        }

        rb.linearVelocityX = 0;
        anim.SetTrigger(AttackAnimParam);

        currentAttackTimer = attackDelay;
    }
    #endregion

    #region 기타 함수

    private void InitStat(EPlayerType playerType)
    {
        switch (playerType)
        {
            case EPlayerType.Warrior:
                attackPower = 15f;
                attackDelay = 1f;
                attackDistance = 1f;
                break;
            
            case EPlayerType.Archor:
                attackPower = 50f;
                attackDelay = 3.5f;
                attackDistance = 2.2f;
                break;
            
            case EPlayerType.Wizard:
                attackPower = 100f;
                attackDelay = 5f;
                attackDistance = 3.4f;
                break;
        }
    }
    
    // 앞에 몬스터가 있는가
    private void CheckMonster()
    {
        // 오른쪽 방향으로 Player 레이어만 레이캐스트
        RaycastHit2D hit = Physics2D.Raycast(
            rb.position,
            Vector2.right,
            attackDistance,
            monsterLayer
        );

        if (hit.collider != null)
        {
            // Player 감지 → 공격 모드
            canMove = false;
            OnAttack();
        }
        else
        {
            // Player 없음 → 이동
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
