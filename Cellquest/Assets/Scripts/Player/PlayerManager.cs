using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

#region Explicação

/* 
gerencia os dados do jogador (perder vida, ganhar vida, estrelas etc)
 
*/

#endregion

public class PlayerManager : Singleton<PlayerManager>
{
    


    [SerializeField] public PlayerData PlayerData;
    
    public Action OnChangePlayerData;
    void Start()
    {
        LoadData();
        MenuUiManager.Instance.SetCharacterBar();
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
            Debug.Log("p");
            LoadData();
        }
    }

    public void UpdateCoins(int additionalCoins)
    {
        PlayerData.Coins += additionalCoins;
        OnChangePlayerData?.Invoke();
    }

    public void UpdateLife(int additionalHealth)
    {
        PlayerData.HP += additionalHealth;
        OnChangePlayerData?.Invoke();
    }

    public void UpdatePlayerData(int additionalHealth, int additionalCoins)
    {
        PlayerData.HP += additionalHealth;
        PlayerData.Coins += additionalCoins;
        OnChangePlayerData?.Invoke();
    }

    public void UpdateStars()
    {
        PlayerData.StarsCollected = LevelManager.Instance.GetCollectedStars();
        ChangeProfileLevel();
        OnChangePlayerData?.Invoke();
    }

    public int GetCoins()
    {
        return PlayerData.Coins;
    }

    public int GetLife()
    {
        return PlayerData.HP;
    }

    public int GetStars()
    {
        return PlayerData.StarsCollected;
    }

    public override void SaveData()
    {
        List<PlayerData> playerDatas = new List<PlayerData>() { PlayerData };
        SaveDataManager.SaveJsonData(playerDatas, PlayerData.savePath);
    }

    public override void LoadData()
    {
        List<PlayerData> playerDatas = new List<PlayerData>() { PlayerData };
        SaveDataManager.LoadJsonData(playerDatas, PlayerData.savePath);
        MenuUiManager.Instance.SetCharacterBar();
    }

    public void ChangeProfileLevel()
    {
        if (PlayerData.StarsCollected < 8)
        {
            PlayerData.ProfileLevel = "ESTUDANTE";
        }else if(PlayerData.StarsCollected >= 8 && PlayerData.StarsCollected < 16)
        {
            PlayerData.ProfileLevel = "GRADUANDO";
        }
        else if (PlayerData.StarsCollected >= 16 && PlayerData.StarsCollected < 24)
        {
            PlayerData.ProfileLevel = "MESTRANDO";
        }
        else if (PlayerData.StarsCollected >= 24 && PlayerData.StarsCollected < 32)
        {
            PlayerData.ProfileLevel = "DOUTORANDO";
        }
        else if (PlayerData.StarsCollected >= 32)
        {
            PlayerData.ProfileLevel = "CIENTISTA";
        }
    }

    public string GetProfileLevel()
    {
        return PlayerData.ProfileLevel;
    }

}
