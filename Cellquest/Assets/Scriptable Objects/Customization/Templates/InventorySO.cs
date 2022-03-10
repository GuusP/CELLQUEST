using System.Collections;
using System.Collections.Generic;
using UnityEngine;




#region Explicação

/* 
 * ScriptableObject para criar inventários. Tem função para remover item, adicionar item
 * Tem uma lista de itens que funciona como inventário e tem um ItemDatabaseSO para
 * referenciar  qual ItemDatabase esse inventário deve usar como comparação para os IDs
*/

#endregion

[System.Serializable]
[CreateAssetMenu(fileName ="Inventory", menuName ="Itens/Inventory")]
public class InventorySO : ScriptableObject, ISavable
{
    public ItemDatabaseSO databaseSO;
    public List<ItemSO> inventoryList;
    public string savePath;
    public void AddItem(ItemSO item)
    {
        inventoryList.Add(item);
        item.itemID = databaseSO.GetID[item];
        List<InventorySO> inventories = new List<InventorySO>() { this };
        SaveDataManager.SaveJsonData(inventories, this.savePath);
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        inventoryList = new List<ItemSO>();
        foreach(int id in a_SaveData.itemsIDs)
        {
            inventoryList.Add(databaseSO.Items[id]);
        }
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData saveData = new SaveData();
        foreach (ItemSO item in inventoryList)
        {
            saveData.itemsIDs.Add(item.itemID);
        }
        a_SaveData.itemsIDs = saveData.itemsIDs;
    }

    public void RemoveItem(ItemSO item)
    {
        inventoryList.Remove(item);
        List<InventorySO> inventories = new List<InventorySO>() { this };
        SaveDataManager.SaveJsonData(inventories, this.savePath);
    }


}
