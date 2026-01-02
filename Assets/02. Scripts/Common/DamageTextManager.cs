using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField] private DamageText damageTextPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void Spawn(float damage, Vector3 position)
    {
        DamageText text = Instantiate(damageTextPrefab, position, Quaternion.identity);
        text.Init(damage);
    }
}