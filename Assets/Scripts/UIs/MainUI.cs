using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject myItemsArea;
    [SerializeField] TMP_Text dolarText;
    [SerializeField] Button getDolarButton, openShopButton, myItemButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SyncData();
        getDolarButton.onClick.AddListener(() => getDolarOnEvent());
        openShopButton.onClick.AddListener(() => openShopOnEvent());
    }

    void getDolarOnEvent()
    {
        GameData.Instance.LocalData.Dolar += 1307;
        SaveSystem.Save();
        SyncData();
    }

    void openShopOnEvent()
    {
        UIController.Instance.OpenShopUI();
    }

    public void SyncData()
    {
        LocalData data = GameData.Instance.LocalData;
        dolarText.text = $"${data.Dolar}";

        var items = GameData.Instance.LocalData.MyItems;
        scrollRect.horizontalNormalizedPosition = 1;

        foreach (Transform child in myItemsArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var i in items)
        {
            STO_ShopItem item = GameData.Instance.ShopItems.Find(x => x.Id == i.Key);

            Button b = Instantiate(myItemButton);
            b.GetComponent<RectTransform>().SetParent(myItemsArea.transform);
            b.transform.localScale = new Vector3(1, 1, 1);

            b.transform.GetChild(0).GetComponent<Image>().sprite = item.Avatar;
            b.GetComponentInChildren<TMP_Text>().text = $"<color=blue>{item.Name}</color> x{i.Value}";
        }
    }
}
