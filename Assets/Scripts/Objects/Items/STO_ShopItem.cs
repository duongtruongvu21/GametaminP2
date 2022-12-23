using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "MySTO/ShopItem")]
public class STO_ShopItem : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] string name;
    [SerializeField][TextArea] string description;
    [SerializeField] int price;
    [SerializeField] ShopItemType type;
    [SerializeField] Sprite avatar;
    [SerializeField] bool isShortcut;


    public int Id { get { return id; } }
    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public int Price { get { return price; } }
    public ShopItemType Type { get { return type; } }
    public Sprite Avatar { get { return avatar; } }
    public bool IsShortcut { get { return isShortcut; } }
}
