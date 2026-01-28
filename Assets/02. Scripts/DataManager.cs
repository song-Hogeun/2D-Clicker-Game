using System;
using UnityEngine;

/// <summary>
/// 골드, 경험치, 레벨, 스테이지 등
/// 게임의 핵심 진행 데이터를 관리하는 싱글톤 매니저
/// </summary>
public class DataManager : MonoBehaviour
{
    /// <summary>
    /// 전역 접근을 위한 싱글톤 인스턴스
    /// </summary>
    public static DataManager Instance { get; private set; }

    #region Player Data

    [Header("Currency")]
    [SerializeField] private int gold;

    [Header("Experience")]
    [SerializeField] private int level = 1;
    [SerializeField] private int currentExp;
    [SerializeField] private int requiredExp = 100;

    [Header("Stage")]
    [SerializeField] private int stage = 1;

    #endregion

    #region Events

    /// <summary>
    /// 골드 값이 변경될 때 호출
    /// </summary>
    public Action<int> OnGoldChanged;

    /// <summary>
    /// 경험치가 변경될 때 호출
    /// (현재 경험치, 필요 경험치)
    /// </summary>
    public Action<int, int> OnExpChanged;

    /// <summary>
    /// 레벨업 시 호출
    /// </summary>
    public Action<int> OnLevelUp;

    /// <summary>
    /// 스테이지 변경 시 호출
    /// </summary>
    public Action<int> OnStageChanged;

    #endregion

    #region Life Cycle

    /// <summary>
    /// 싱글톤 초기화
    /// 씬 전환 시에도 유지됨
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Gold

    /// <summary>
    /// 현재 보유 골드 반환
    /// </summary>
    public int Gold => gold;

    /// <summary>
    /// 골드를 증가시킴
    /// </summary>
    /// <param name="amount">추가할 골드 양</param>
    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }

    /// <summary>
    /// 골드를 소비함
    /// </summary>
    /// <param name="amount">소비할 골드 양</param>
    /// <returns>소비 성공 여부</returns>
    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;

        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        return true;
    }

    #endregion

    #region Experience

    /// <summary>
    /// 현재 레벨 반환
    /// </summary>
    public int Level => level;

    /// <summary>
    /// 현재 경험치 반환
    /// </summary>
    public int CurrentExp => currentExp;

    /// <summary>
    /// 레벨업에 필요한 경험치 반환
    /// </summary>
    public int RequiredExp => requiredExp;

    /// <summary>
    /// 경험치를 추가하고, 필요 시 레벨업 처리
    /// </summary>
    /// <param name="amount">추가할 경험치 양</param>
    public void AddExp(int amount)
    {
        if (amount <= 0) return;

        currentExp += amount;

        // 경험치가 충분하면 레벨업 반복 처리
        while (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            LevelUp();
        }

        OnExpChanged?.Invoke(currentExp, requiredExp);
    }

    /// <summary>
    /// 레벨을 증가시키고 다음 레벨의 필요 경험치를 갱신
    /// </summary>
    private void LevelUp()
    {
        level++;
        requiredExp = CalculateRequiredExp(level);

        OnLevelUp?.Invoke(level);
    }

    /// <summary>
    /// 다음 레벨에 필요한 경험치 계산
    /// </summary>
    /// <param name="nextLevel">다음 레벨</param>
    /// <returns>필요 경험치</returns>
    private int CalculateRequiredExp(int nextLevel)
    {
        // 간단한 선형 증가 공식
        return 100 + (nextLevel - 1) * 50;
    }

    #endregion

    #region Stage

    /// <summary>
    /// 현재 스테이지 반환
    /// </summary>
    public int Stage => stage;

    /// <summary>
    /// 다음 스테이지로 진행
    /// </summary>
    public void NextStage()
    {
        stage++;
        OnStageChanged?.Invoke(stage);
    }

    /// <summary>
    /// 스테이지 값을 강제로 설정
    /// </summary>
    /// <param name="value">설정할 스테이지</param>
    public void SetStage(int value)
    {
        stage = Mathf.Max(1, value);
        OnStageChanged?.Invoke(stage);
    }

    #endregion
}
