using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    [SerializeField] private Text currentStageText;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text currentGoldText;
    [SerializeField] private Text currentExpText;

    [SerializeField] private Image expImage;
    
    [SerializeField] private Button[] buttons;
    
    [SerializeField] private GameObject[] popups;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        Init();
    }

    private void Start()
    {
        DataManager.Instance.OnGoldChanged += UpdateGoldUI;
        DataManager.Instance.OnExpChanged += UpdateExpUI;
        DataManager.Instance.OnStageChanged += UpdateStageUI;
        DataManager.Instance.OnExpChanged += UpdateExpUI;
    }

    private void Init()
    {
        int currentGold = DataManager.Instance.Gold;
        int currentExp = DataManager.Instance.CurrentExp;
        int requireExp = DataManager.Instance.RequiredExp;
        int currentStage = DataManager.Instance.Stage;
        int currentLevel = DataManager.Instance.Level;

        UpdateExpUI(currentExp, requireExp);
        UpdateGoldUI(currentGold);
        UpdateStageUI(currentStage);
        UpdateLevelUI(currentLevel);
    }
    
    private void UpdateGoldUI(int gold)
    {
        currentGoldText.text = string.Format($"#,###", gold.ToString());
    }
    
    private void UpdateExpUI(int exp, int requireExp)
    {
        float percentage = exp / requireExp;
     
        currentExpText.text = percentage + " %";
        expImage.fillAmount = percentage;
    }
    
    private void UpdateStageUI(int stage)
    {
        currentStageText.text = "스테이지 " + stage;
    }

    private void UpdateLevelUI(int level)
    {
        currentLevelText.text = "LV. " + level;
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
