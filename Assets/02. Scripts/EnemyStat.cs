using UnityEngine;

[CreateAssetMenu(menuName = "Combat/EnemyStat")]
public class EnemyStat : ScriptableObject
{
    public float maxHP;
    public float speed;
    public float attackPower;
    public float attackDelay;
    public float attackDistance;
}