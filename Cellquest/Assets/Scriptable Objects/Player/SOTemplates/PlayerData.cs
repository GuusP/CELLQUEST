using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Explicação

/* 
 * Essa classe define os dados que o jogador possui, como vida, estrelas etc
*/

#endregion

[System.Serializable]
public class PlayerData : ISavable
{
    public int MaxHP = 5;
    public int HP = 5;
    public int StarsCollected;
    public int Coins;
    public string ProfileLevel;
    public string savePath = "PlayerData.dat";
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        HP = a_SaveData.playerData.HP;
        StarsCollected = a_SaveData.playerData.StarsCollected;
        Coins = a_SaveData.playerData.Coins;
        ProfileLevel = a_SaveData.playerData.ProfileLevel;
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.PlayerData saveData = new SaveData.PlayerData();
        saveData.HP = HP;
        saveData.Coins = Coins;
        saveData.StarsCollected = StarsCollected;
        saveData.ProfileLevel = ProfileLevel;
        a_SaveData.playerData = saveData;
    }
}
