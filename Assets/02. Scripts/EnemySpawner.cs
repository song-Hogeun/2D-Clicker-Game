using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemyPrefabs;
    [SerializeField] private Transform spawnPoint;

    private EnemyController currentEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        currentEnemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        currentEnemy.OnDead += HandleEnemyDead;
    }

    private void HandleEnemyDead(BaseCharacter dead)
    {
        dead.OnDead -= HandleEnemyDead;

        PoolManager.Instance.Release(dead as EnemyController);
        Invoke(nameof(SpawnEnemy), 0.5f);
    }
}

