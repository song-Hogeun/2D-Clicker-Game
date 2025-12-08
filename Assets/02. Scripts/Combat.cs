using UnityEngine;

public class Combat : MonoBehaviour
{
    private Hitbox meleeHitbox;

    private void Awake()
    {
        // 인스펙터에서 안 넣어줬으면 자식에서 자동으로 찾기
        if (meleeHitbox == null)
        {
            meleeHitbox = GetComponentInChildren<Hitbox>();
        }
    }

    // 애니메이션 이벤트에서 호출할 함수
    public void HitboxOn()
    {
        meleeHitbox.Activate();
    }

    public void HitboxOff()
    {
        meleeHitbox.Deactivate();
    }
}