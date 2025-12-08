using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    private Animator anim;

    [Header("데미지 설정")]
    [SerializeField] private float damage => 10f;

    [Header("타겟 레이어")]
    [SerializeField] private LayerMask targetLayerMask;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        anim = GetComponentInParent<Animator>();
        
        _collider.isTrigger = true; // 히트박스는 보통 Trigger로 사용
    }

    /// <summary>
    /// 히트박스 활성화 (애니메이션 이벤트에서 호출 추천)
    /// </summary>
    public void Activate()
    {
        _collider.enabled = true;
    }

    /// <summary>
    /// 히트박스 비활성화 (애니메이션 이벤트에서 호출 추천)
    /// </summary>
    public void Deactivate()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 레이어 필터 (맞추고 싶은 레이어만 맞게)
        if ((targetLayerMask.value & (1 << other.gameObject.layer)) == 0)
            return;

        // IDamageable 가진 애만 데미지 입히기
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}