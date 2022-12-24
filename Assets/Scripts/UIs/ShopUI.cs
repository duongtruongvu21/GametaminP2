using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject bundleItemsArea;
    [SerializeField] GameObject coinItemsArea;
    [SerializeField] Button bundleButton, coinButton, moreButton;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateItem(bool isShortcut = true)
    {
        List<STO_ShopItem> shopItems = GameData.Instance.ShopItems;
        scrollRect.horizontalNormalizedPosition = 1;

        foreach (Transform child in bundleItemsArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in coinItemsArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var i in shopItems)
        {
            if (isShortcut && !i.IsShortcut) continue;
            Button b;
            if (i.Type == ShopItemType.Coin)
            {
                b = Instantiate(coinButton);
                b.GetComponent<RectTransform>().SetParent(coinItemsArea.transform);
                b.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                b = Instantiate(bundleButton);
                b.GetComponent<RectTransform>().SetParent(bundleItemsArea.transform);
                b.transform.localScale = new Vector3(1, 1, 1);
            }
            b.GetComponent<ButtonInfo>().SetInfo(i.Id);
            b.onClick.AddListener(() => purchaseItem(i.Id));
        }

        Button more = Instantiate(moreButton);
        more.GetComponent<RectTransform>().SetParent(coinItemsArea.transform);
        more.transform.localScale = new Vector3(1, 1, 1);
        if (isShortcut)
        {
            more.onClick.AddListener(() => XemThem());
        }
        else
        {
            more.onClick.AddListener(() => AnBot());
            more.GetComponentInChildren<TMP_Text>().text = "Ẩn bớt";
        }
    }

    void XemThem()
    {
        CreateItem(false);
    }

    void AnBot()
    {
        CreateItem();
    }

    string timeRemaining(int secs)
    {
        TimeSpan t = TimeSpan.FromSeconds(secs);
        return string.Format("{0:D2} phút {1:D2} giây", t.Minutes, t.Seconds);
    }

    void purchaseItem(int id)
    {
        STO_ShopItem item = GameData.Instance.ShopItems.Find(x => x.Id == id);
        LocalData data = GameData.Instance.LocalData;

        if (item.Price == 0)
        {
            if (data.LastViewAds.Add(new TimeSpan(0, 1, 37)) <= DateTime.Now)
            {
                if (!data.MyItems.ContainsKey(item.Id))
                    data.MyItems[item.Id] = 0;
                data.MyItems[item.Id] += 1;
                data.LastViewAds = DateTime.Now;
                SaveSystem.Save();
                MainUI.Instance.SyncData();
                UIController.Instance.OpenNoticeUI($"Xem quảng cáo thành công, nhận được 1 x {item.Name}!!");
            }
            else
            {
                int i = 0;
                for (i = 0; data.LastViewAds.Add(new TimeSpan(0, 1, 37)) > DateTime.Now.Add(new TimeSpan(0, 0, i)); i++) { }
                UIController.Instance.OpenNoticeUI($"Vui lòng đợi {timeRemaining(i)} nữa để xem quảng cáo tiếp theo!!");
            }
            return;
        }

        if (data.Dolar > item.Price)
        {
            data.Dolar -= item.Price;
            if (!data.MyItems.ContainsKey(item.Id))
                data.MyItems[item.Id] = 0;
            data.MyItems[item.Id] += 1;
            SaveSystem.Save();
            MainUI.Instance.SyncData();
            UIController.Instance.OpenNoticeUI($"Mua {item.Name} thành công!!");
        }
        else
        {
            UIController.Instance.OpenNoticeUI($"Mua {item.Name} thất bại!!");
        }
    }
}
