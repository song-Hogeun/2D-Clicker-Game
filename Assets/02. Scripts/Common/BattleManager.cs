using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private EnemyController[] enemyPrefabs;

    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistance = 12f;
    [SerializeField] private float respawnDelay = 0.5f;

    private EnemyController currentEnemy;
    private int killCount;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        EnemyController prefab = GetRandomEnemyPrefab();

        Vector3 spawnPos = new Vector3(
            player.position.x + spawnDistance,
            player.position.y,
            0f
        );

        currentEnemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        currentEnemy.OnDead += HandleEnemyDead;
    }

    private EnemyController GetRandomEnemyPrefab()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[index];
    }

    private void HandleEnemyDead(BaseCharacter dead)
    {
        dead.OnDead -= HandleEnemyDead;

        // if (killCount % 10 == 0)
        //     DataManager.Instance.NextStage();

        Destroy(dead.gameObject);
        Invoke(nameof(SpawnEnemy), 0.5f);
    }
}   