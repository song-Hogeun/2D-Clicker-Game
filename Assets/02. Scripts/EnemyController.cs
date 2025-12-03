using UnityEngine;
using static Constants;

// 필수 컴포넌트
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IDamageable
{
    // 컴포넌트
    private Rigidbody2D rb;
    private Animator anim;
    
    // 기본 스탯
    private float applySpeed;
    private float moveSpeed = 3f;
    private float maxHP = 100f;
    private float currentHP;
    
    // 공격 관련
    private float attackPower = 5f;
    private float attackDelay = 5f;
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
        
        applySpeed = moveSpeed;
        currentHP = maxHP;
    }
    
    private void Update()
    {
        CheckPlayer();
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
        if (!isAttack)
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
    private void CheckPlayer()
    {
        // 오른쪽 방향으로 Player 레이어만 레이캐스트
        RaycastHit2D hit = Physics2D.Raycast(
            rb.position,
            Vector2.left,
            1f,
            playerLayer
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
    
    private void OnDrawGizmos()
    {
        if (rb == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rb.position, rb.position + Vector2.left * 1f);
    }
}
