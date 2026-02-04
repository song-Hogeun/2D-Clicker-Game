using System;
using DG.Tweening;
using UnityEngine;
using static Constants;

/// <summary>
/// 모든 전투 캐릭터의 공통 부모
/// Player / Enemy / Boss 공통 규칙만 담당
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseCharacter : MonoBehaviour, IDamageable
{
    // ===== Components =====
    [SerializeField] protected Transform rootPos;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer[] spriteRenderers;               // 사망 시 페이드 효과 적용 위해 스프라이트 받아옴
    [SerializeField] protected LayerMask targetLayer;         // 감지할 레이어

    // ===== Stats =====
    [Header("Base Stat")] 
    [SerializeField] protected float speed;             // 이동 속도
    [SerializeField] protected float maxHP = 100f;      // 최대 체력
    [SerializeField] protected float currentHP;                          // 현재 체력
    protected float applySpeed;                         // 적용 될 이동속도

    // ===== Combat =====
    [Header("Combat")]
    [SerializeField] protected float attackPower = 10f;         // 공격력
    [SerializeField] protected float attackDelay = 1f;          // 공격 딜레이
    [SerializeField] protected float attackDistance = 1.2f;     // 공격 가능한 거리
    protected float attackTimer;                                // 공격 딜레이 타이머

    public float AttackPower => attackPower;
    
    // ===== State =====
    protected bool isDead;                              // 죽었는지 판단
    
    // ===== Action =====
    public Action<float, Vector3> OnDamaged;
    public Action<BaseCharacter> OnDead;

    #region 생성 주기

    protected virtual void Awake()
    {
        // 초기화
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // HP 적용
        currentHP = maxHP;
        applySpeed = speed;
    }

    protected virtual void Update()
    {
        if (isDead) return;

        attackTimer -= Time.deltaTime;
        Think();
    }

    #endregion

    #region 코드 흐름
    
    /// <summary>
    /// 캐릭터의 판단 흐름
    /// </summary>
    protected virtual void Think()
    {
        bool detected = DetectTarget();
        
        // 타겟 감지 x
        if (!detected)
            Move();

        // 타겟 감지 o, 타겟 공격 가능 상태
        if (detected && CanAttackTarget())
            Attack();
    }

    /// <summary>
    /// 타겟 감지 (자식에서 구현)
    /// </summary>
    protected virtual bool DetectTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            rootPos.position,
            attackDistance,
            targetLayer
        );

        return hit != null;
    }

    /// <summary>
    /// 공격 가능 판단
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanAttackTarget()
    {
        return attackTimer <= 0;
    }
    
    /// <summary>
    /// 이동 방향 결정 (자식 책임)
    /// </summary>
    protected abstract Vector2 GetMoveDirection();
    protected abstract LayerMask GetTargetLayer();

    #endregion

    #region 행동
    
    protected virtual void Move()
    {
        Vector2 dir = GetMoveDirection();
        rb.linearVelocity = new Vector2(dir.x * applySpeed, rb.linearVelocity.y);
        
        anim.SetBool(MoveAnimParam, true);
    }

    protected virtual void Attack()
    {
        Debug.Log("-- 공격 실행 --");
        
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger(AttackAnimParam);
        attackTimer = attackDelay;
    }

    #endregion

    #region 데미지 로직

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;
        anim.SetTrigger(DamageAnimParam);

        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger(DeathAnimParam);
    }

    #endregion

    public virtual void OnSpawn()
    {
        isDead = false;
        currentHP = maxHP;
        attackTimer = 0f;

        rb.linearVelocity = Vector2.zero;

        anim.Rebind();
        anim.Update(0f);
    }

    public virtual void OnDespawn()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
