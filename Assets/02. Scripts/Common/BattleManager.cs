using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private EnemyController[] normalEnemyPrefabs;
    [SerializeField] private EnemyController[] bossPrefabs;

    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistance = 12f;
    [SerializeField] private float respawnDelay = 0.5f;

    private EnemyController currentEnemy;
    
    private int killCount; // 현재 스테이지에서 잡은 잡몹 수
    private bool isBossPhase; // 보스 페이지인지 여부

    private void Start()
    {
        killCount = 0;
        isBossPhase = false;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // 잡몹 10마리 처치 → 보스 페이즈
        if (killCount >= 10 && !isBossPhase)
        {
            SpawnBoss();
            return;
        }
        
        // 잡몹 스폰
        EnemyController prefab = GetRandomNormalEnemy();

        Vector3 spawnPos = new Vector3(
            player.position.x + spawnDistance,
            player.position.y,
            0f
        );

        currentEnemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        currentEnemy.OnDead += HandleEnemyDead;
    }
    
    private void SpawnBoss()
    {
        EnemyController boss = GetRandomBoss();

        Vector3 spawnPos = new Vector3(
            player.position.x + spawnDistance,
            player.position.y,
            0f
        );

        currentEnemy = Instantiate(boss, spawnPos, Quaternion.identity);
        currentEnemy.OnDead += HandleEnemyDead;

        // 보스 연출 시작
        StartCoroutine(BossIntroSequence(boss));
    }

    private EnemyController GetRandomNormalEnemy()
    {
        int index = Random.Range(0, normalEnemyPrefabs.Length);
        return normalEnemyPrefabs[index];
    }
    
    private EnemyController GetRandomBoss()
    {
        int index = Random.Range(0, bossPrefabs.Length);
        return bossPrefabs[index];
    }
    
    private IEnumerator BossIntroSequence(EnemyController boss)
    {
        // 시간 멈춤
        Time.timeScale = 0f;

        // 화면 효과
        ScreenEffectManager.Instance.PlayBossIntroEffect();

        // 보스 UI 표시
        BossUIManager.Instance.ShowBossUI(
            boss.EnemyName,
            boss.MaxHP,
            boss.CurrentHP
        );

        // 연출 시간 (실시간 기준)
        yield return new WaitForSecondsRealtime(1.5f);

        // 전투 시작
        Time.timeScale = 1f;
    }

    private void HandleEnemyDead(BaseCharacter dead)
    {
        dead.OnDead -= HandleEnemyDead;
        Destroy(dead.gameObject);

        DataManager.Instance.AddGold(10);
        DataManager.Instance.AddExp(20);
        
        if (dead is EnemyController enemy && enemy.IsBoss)
        {
            DataManager.Instance.NextStage();

            // 다음 스테이지 준비
            killCount = 0;
            isBossPhase = false;
        }
        else
        {
            // 잡몹 처치
            killCount++;
            Debug.Log($"KillCount: {killCount}");
        }

        Invoke(nameof(SpawnEnemy), respawnDelay);
    }
}   