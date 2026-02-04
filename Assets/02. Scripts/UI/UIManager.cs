using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text currentStageText;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text currentGoldText;
    [SerializeField] private Text currentExpText;
    
    [SerializeField] private Button[] buttons;
    
    [SerializeField] private GameObject[] popups;
    
    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // 이벤트 연결
        DataManager.Instance.OnGoldChanged += UpdateGoldUI;
        DataManager.Instance.OnExpChanged += UpdateExpUI;
        DataManager.Instance.OnStageChanged += UpdateStageUI;
        DataManager.Instance.OnLevelChanged += UpdateLevelUI;
    }
    
    private void UpdateGoldUI(int gold)
    {
        currentGoldText.text = gold.ToString();
    }
    
    private void UpdateExpUI(int exp, int requireExp)
    {
        currentExpText.text = $"{exp} / {requireExp}";
    }
    
    private void UpdateStageUI(int stage)
    {
        currentStageText.text = $"스테이지 {stage}";
    }

    private void UpdateLevelUI(int level)
    {
        currentLevelText.text = $"LV. {level}";
    }
    
    private void OnPopupByIndex(int index)
    {
        if (index < 0 || index >= popups.Length) return;

        popups[index].SetActive(true);
    }

    public void ExitPopup(GameObject popup)
    {
        popup.SetActive(false);
    }
}
