using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;


#region Explicação

/* 
 * Gerencia a aba perfil, a função de equipar um item, de carregar os itens que o jogador já tem
 * e de desativar o SpriteRenderer dos cabelos que o player não ta usando
*/

#endregion

[System.Serializable]
public class PerfilManager : SingletonSingleScene<PerfilManager>, ISavable
{
    public itemUI itemUI;
    public bool editMode;
    public List<SpriteRenderer> hairRenderers = new List<SpriteRenderer>();
    public List<SpriteRenderer> hairRenderers2 = new List<SpriteRenderer>();
    public SpriteRenderer[] blusaRenderer;
    public SpriteRenderer[] skinRenderer;
    public SpriteRenderer[] oculosRenderer;
    public Dictionary<itemType, int> itemUsedID = new Dictionary<itemType, int>() {
        {itemType.Skin, new int() },
        {itemType.Blusa, 0 },
        {itemType.Hair_1, 0},
        {itemType.Hair_2, 0},
        {itemType.Hair_3, 0},
        {itemType.Hair_4, 0},
        {itemType.Hair_5, 0},
        {itemType.Oculos, 0},
    };

    public int hairInUse = 0;
    public ItemDatabaseSO databaseSO;
    public string savePath;
    public float secondsToLoad = 2f;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        StartCoroutine(SaveEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void EquipItem(ItemSO item, bool isStarting)
    {
        if (editMode || isStarting)
        {
            switch (item.itemType)
            {
                case itemType.Skin:
                    foreach (SpriteRenderer spriteRenderer in skinRenderer)
                        item.EquipItem(spriteRenderer);

                    break;

                case itemType.Blusa:
                    foreach (SpriteRenderer spriteRenderer in blusaRenderer)
                        item.EquipItem(spriteRenderer);
                    break;

                case itemType.Hair_1:
                    item.EquipItem(hairRenderers[0]);
                    DisableHairs(hairRenderers[0], hairRenderers);
                    item.EquipItem(hairRenderers2[0]);
                    DisableHairs(hairRenderers2[0], hairRenderers2);
                    if (!isStarting)
                        hairInUse = 0;
                    break;

                case itemType.Hair_2:
                    item.EquipItem(hairRenderers[1]);
                    DisableHairs(hairRenderers[1], hairRenderers);
                    item.EquipItem(hairRenderers2[1]);
                    DisableHairs(hairRenderers2[1], hairRenderers2);
                    if (!isStarting)
                        hairInUse = 1;
                    break;

                case itemType.Hair_3:
                    item.EquipItem(hairRenderers[2]);
                    DisableHairs(hairRenderers[2], hairRenderers);
                    item.EquipItem(hairRenderers2[2]);
                    DisableHairs(hairRenderers2[2], hairRenderers2);
                    if (!isStarting)
                        hairInUse = 2;
                    break;

                case itemType.Hair_4:
                    item.EquipItem(hairRenderers[3]);
                    DisableHairs(hairRenderers[3], hairRenderers);
                    item.EquipItem(hairRenderers2[3]);
                    DisableHairs(hairRenderers2[3], hairRenderers2);
                    if (!isStarting)
                        hairInUse = 3;
                    break;

                case itemType.Hair_5:
                    item.EquipItem(hairRenderers[4]);
                    DisableHairs(hairRenderers[4], hairRenderers);
                    item.EquipItem(hairRenderers2[4]);
                    DisableHairs(hairRenderers2[4], hairRenderers2);
                    if (!isStarting)
                        hairInUse = 4;
                    break;

                case itemType.Oculos:
                    foreach (SpriteRenderer spriteRenderer in oculosRenderer)
                        item.EquipItem(spriteRenderer);
                    break;
            }

            itemUsedID[item.itemType] = item.itemID;
        }
    }

    public void EditMode()
    {
        if (editMode)
        {
            editMode = false;
        }
        else
        {
            editMode = true;
        }
    }

    public void DisableHairs(SpriteRenderer hair, List<SpriteRenderer> spriteRenderers)
    {
        List<SpriteRenderer> sprites = new List<SpriteRenderer>() { hair };
        var list = spriteRenderers.Except(sprites);
        foreach (SpriteRenderer spriteRenderer in list)
        {
            spriteRenderer.gameObject.SetActive(false);
        }

        hair.gameObject.SetActive(true);
    }

    public void LoadProfile()
    {

    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        Debug.Log("Salvei");
        SaveData saveData = new SaveData();
        Debug.Log(itemUsedID[itemType.Blusa]);
        a_SaveData.playerClothing.blusaID = itemUsedID[itemType.Blusa];
        a_SaveData.playerClothing.skinID = itemUsedID[itemType.Skin];
        a_SaveData.playerClothing.oculosID = itemUsedID[itemType.Oculos];
        a_SaveData.playerClothing.hairInUse = hairInUse;
        a_SaveData.hairsID.Add(itemUsedID[itemType.Hair_1]);
        a_SaveData.hairsID.Add(itemUsedID[itemType.Hair_2]);
        a_SaveData.hairsID.Add(itemUsedID[itemType.Hair_3]);
        a_SaveData.hairsID.Add(itemUsedID[itemType.Hair_4]);
        a_SaveData.hairsID.Add(itemUsedID[itemType.Hair_5]);
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        itemUsedID[itemType.Blusa] = a_SaveData.playerClothing.blusaID;
        itemUsedID[itemType.Skin] = a_SaveData.playerClothing.skinID;
        itemUsedID[itemType.Oculos] = a_SaveData.playerClothing.oculosID;
        hairInUse = a_SaveData.playerClothing.hairInUse;
        List<int> hairsId = new List<int>();
        hairsId = a_SaveData.hairsID;
        SetLoadedItems(hairsId);
    }

    public void SetLoadedItems(List<int> hairsID)
    {
       
        EquipItem(databaseSO.Items[itemUsedID[itemType.Blusa]], true);
        EquipItem(databaseSO.Items[itemUsedID[itemType.Skin]], true);
        EquipItem(databaseSO.Items[hairsID[hairInUse]], true);
        
        
        DisableHairs(hairRenderers[hairInUse], hairRenderers);
        
        DisableHairs(hairRenderers2[hairInUse], hairRenderers2);
    }

    public override void SaveData()
    {
        List<PerfilManager> perfilManagers = new List<PerfilManager>() { this };
        SaveDataManager.SaveJsonData(perfilManagers, savePath);
    }

    public override void LoadData()
    {
        List<PerfilManager> perfilManagers = new List<PerfilManager>() { this };
        SaveDataManager.LoadJsonData(perfilManagers, savePath);
    }

    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(secondsToLoad);
        LoadData();
        StopCoroutine(LoadCoroutine());
    }


}
