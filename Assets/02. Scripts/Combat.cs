using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private Hitbox hitbox;

    // 애니메이션 이벤트에서 호출할 함수
    public void HitboxOn()
    {
        hitbox.Activate();
    }

    public void HitboxOff()
    {
        hitbox.Deactivate();
    }
}