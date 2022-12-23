using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] GameObject ShopUIObj, NoticeUIObj;

    private void Awake()
    {
        Instance = this;
        ShopUIObj.SetActive(false);
        NoticeUIObj.SetActive(false);
    }

    public void OpenShopUI()
    {
        ShopUIObj.SetActive(true);
        ShopUI.Instance.CreateItem();
    }

    public void CloseShopUI()
    {
        ShopUIObj.SetActive(false);
    }

    public void OpenNoticeUI(string content)
    {
        NoticeUIObj.SetActive(true);
        NoticeUI.Instance.SetNotice(content);
    }

    public void CloseNoticeUI()
    {
        NoticeUIObj.SetActive(false);
    }
}
