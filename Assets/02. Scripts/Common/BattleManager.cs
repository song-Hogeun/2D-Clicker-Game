using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private EnemyController currentEnemy;
    private int killCount;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        currentEnemy = PoolManager.Instance.Get<EnemyController>();
        currentEnemy.transform.position = spawnPoint.position;

        currentEnemy.OnDead += HandleEnemyDead;
        currentEnemy.OnSpawn();
    }

    private void HandleEnemyDead(BaseCharacter dead)
    {
        dead.OnDead -= HandleEnemyDead;

        killCount++;

        PoolManager.Instance.Release(dead as EnemyController);

        Invoke(nameof(SpawnEnemy), 0.5f);
    }
}