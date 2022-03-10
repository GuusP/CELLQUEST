using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType{
    Skin, Blusa, Hair_1, Hair_2, Hair_3, Hair_4, Hair_5, Nao_Equipavel, Oculos, Vida
}

[CreateAssetMenu(fileName="Item", menuName ="Itens/Basic Item")]
public class ItemSO : ScriptableObject
{
    public itemType itemType;
    public Sprite itemSprite;
    public bool isLocked = true;
    [SerializeField] InventorySO inventoryToGo;
    public int price;
    public int itemID;

    public void UnlockItem(bool save)
    {
        isLocked = false;
        inventoryToGo.AddItem(this, save);
        
    }

    public void LockItem(bool save)
    {
        isLocked = true;
        inventoryToGo.RemoveItem(this, save);
    }

    public void EquipItem(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sprite = itemSprite;
    }
}
