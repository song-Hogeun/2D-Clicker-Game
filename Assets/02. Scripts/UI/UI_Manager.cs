using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager instance;
    
    // 스테이지 정보 표시 텍스트
    [SerializeField] private Text stageInfoText;
    
    // 강화, 장비, 동료, 상점 이벤트 버튼
    [SerializeField] private Button[] bottomButtons;
    // 팝업창들
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
    }
    
    private void Start()
    {
        InitBottomButtons();
    }
    
    private void InitBottomButtons()
    {
        for (int i = 0; i < bottomButtons.Length; i++)
        {
            int index = i;
            bottomButtons[i].onClick.AddListener(() => OnPopupByIndex(index));
        }
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
