public interface IPoolable
{
    void OnSpawn();      // Pool에서 꺼낼 때
    void OnDespawn();    // Pool로 돌아갈 때
}