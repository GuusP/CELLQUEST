using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

#region Explicação

/* 
 * passa um item para cada slot na lista de slots (itensSlots). tem um inventário para saber quais itens tem disponível para passar para
 * cada slot
*/

#endregion

public class itemUI : ISave { 
    public InventorySO inventory;
    public List<ItemSlot> itensSlots = new List<ItemSlot>();
    public float sizeMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
      
        SetItem();
        StartCoroutine(SaveEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }

    }

    public void SetItem()
    {
        foreach(ItemSlot itemSlot1 in itensSlots)
        {
            itemSlot1.item = null;
        }

        for (int i = 0; i < inventory.inventoryList.Count; i++)
        {
            itensSlots[i].item = inventory.inventoryList[i];
        }

        foreach (ItemSlot itemSlot in itensSlots)
            itemSlot.UpdateUI();
    }

    public override void SaveData()
    {
        List<InventorySO> inventories = new List<InventorySO>() { inventory };
        SaveDataManager.SaveJsonData(inventories, inventory.savePath);
    }

    public override void LoadData()
    {
        List<InventorySO> inventories = new List<InventorySO>() { inventory };
        SaveDataManager.LoadJsonData(inventories, inventory.savePath);
        SetItem();
    }
}
