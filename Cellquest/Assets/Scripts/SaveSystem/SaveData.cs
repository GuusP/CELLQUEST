using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 

#region Explicação

/* 
 * atributos que serão salvos e métodos de conversão para Json
 * SaveSystem baseado nesse vídeo: https://www.youtube.com/watch?v=uD7y4T4PVk0&t=680s
*/

#endregion

[System.Serializable]
public class SaveData {
    [System.Serializable]
    public struct LevelData // struct que define os dados dos leveis que seram salvos
    {
        public bool isLocked;
        public int stars;
        public int levelNumber;
        public bool alreadyFinished;
    }
    [System.Serializable]
    public struct PlayerData
    {
        public int HP;
        public int StarsCollected;
        public int Coins;
        public string ProfileLevel;
    }

    public Dictionary<itemType, int> kvp = new Dictionary<itemType, int>() {
        { itemType.Skin, 0 },
        { itemType.Blusa, 0 },
        { itemType.Hair_1, 0 },
        { itemType.Hair_2, 0 },
        { itemType.Hair_3, 0 },
        { itemType.Hair_4, 0 },
        { itemType.Hair_5, 0 },

    };

    [System.Serializable]
    public struct PlayerClothing
    {
        public int blusaID;
        public int skinID;
        public int oculosID;
        public int hairInUse;
    }

    public List<int> hairsID = new List<int>();

    public PlayerClothing playerClothing;
    public List<int> itemsIDs = new List<int>();
    public PlayerData playerData;
    public List<LevelData> levelDatas = new List<LevelData>(); // lista de levelData para salvar os dados de cada level

    public string ToJson() // método para converter para Json
    {
        
        return JsonUtility.ToJson(this); // retorna a conversão dessa classe para Json
    }

    public void LoadFromJson(string a_Json) // desconverte de Json para essa classe
    {
        JsonUtility.FromJsonOverwrite(a_Json, this); // passa o Json passado por parâmetro e sobrescreve nessa classe
    }

}


    


public interface ISavable // interface para todas as classes/objts que serão salváveis (savables)
{
    void PopulateSaveData(SaveData a_SaveData); // método que irá "povoar/popular" a classe SaveData, para colocar os dados atuais do jogo nela
    void LoadFromSaveData(SaveData a_SaveData); // método que irá carregar os dados da classe SaveData para os dados do jogo 
}
