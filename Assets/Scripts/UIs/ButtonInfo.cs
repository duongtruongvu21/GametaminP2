using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    [SerializeField] TMP_Text infoItem, priceItems;
    [SerializeField] Image ItemIcon;

    public void SetInfo(int id)
    {
        ItemID = id;
        STO_ShopItem item = GameData.Instance.ShopItems.Find(x => x.Id == id);
        if (item.Type == ShopItemType.Bundle)
            infoItem.text = $"<color=red>{item.Name}</color>\n{item.Description}";
        else
            infoItem.text = $"<color=yellow>{item.Name}</color>";

        priceItems.text = $"${item.Price}";
        ItemIcon.sprite = item.Avatar;
        if (item.Price == 0) priceItems.text = $"Xem quảng cáo";
    }
}
