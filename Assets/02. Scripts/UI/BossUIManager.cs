using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : Singleton<BossUIManager>
{
    [SerializeField] private GameObject bossUI;
    [SerializeField] private Text bossNameText;
    [SerializeField] private Slider hpFill;

    private float maxHP;

    private void Awake()
    {
        base.Awake();
        bossUI.SetActive(false);
    }

    public void ShowBossUI(string name, float maxHp, float currentHp)
    {
        bossUI.SetActive(true);

        bossNameText.text = name;

        maxHP = maxHp;
        UpdateHP(currentHp);
    }

    public void UpdateHP(float currentHp)
    {
        hpFill.value = currentHp / maxHP;
    }

    public void HideBossUI()
    {
        bossUI.SetActive(false);
    }
}