using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.IO;

#region Explicação

/* 
LevelManager funciona como o principal GameManager do jogo. Ele lida com a transição entre cenas, a resolução do jogo, finalização de uma fase
estrelas coletadas, chamar o desbloqueio de outros níveis etc
 
*/

#endregion

public enum levelFinishedStatus
{
    Won, Lost
}
public class LevelManager : Singleton<LevelManager>
{
    string path;


    public static int lastPlayedLevel;
    public static int selectedLevel { get; private set; }
    public static NormalLevelButton nextLevelGO { get; private set; }
    public static Action<int> OnSelectLevel = (a) => { selectedLevel = a; };
    public static NormalLevelButton selectedLevelScript;
    public List<NormalLevelButton> normalLevelButtons = new List<NormalLevelButton>();

    private void Start()
    {
 
        Screen.SetResolution(1080, 1920, true);



    }
   
    public void Jogar()
    {
        Debug.Log("selected: " + selectedLevel);
        if (selectedLevel == normalLevelButtons.Count)
        {
            nextLevelGO = GameObject.Find("Level " + (selectedLevel)).GetComponent<NormalLevelButton>();
        }
        else
        {
            nextLevelGO = GameObject.Find("Level " + (selectedLevel + 1)).GetComponent<NormalLevelButton>();
        }
            
        
        
        selectedLevelScript = GameObject.Find("Level " + (selectedLevel)).GetComponent<NormalLevelButton>();
        LoadSceneManager.Instance.LoadScene(selectedLevelScript.sceneToGo);


    }

    public void LevelFinished(levelFinishedStatus levelFinishedStatus, int starsReceived)
    {
        Debug.Log(selectedLevel);
        if (levelFinishedStatus == levelFinishedStatus.Won)
        {
            selectedLevelScript.levelConsequence.Won();
            
            selectedLevelScript.UpdateStarsNumber(starsReceived);
            selectedLevelScript.SaveData();
            nextLevelGO.UnlockLevel();
            nextLevelGO.SaveData();
            PlayerManager.Instance.UpdateStars();
        }else
        {
            selectedLevelScript.levelConsequence.Lost();
        }
        
        LoadSceneManager.Instance.LoadScene("SampleScene", 1f);
        normalLevelButtons = GetLevels();
        Debug.Log("Star 2: " + selectedLevelScript.stars);
    }


    public int GetCollectedStars()
    {
        int stars_ = 0;
        foreach(NormalLevelButton levelbutton in normalLevelButtons)
        {
            stars_ += levelbutton.stars;
        }
        
        return stars_;
    }

    public List<NormalLevelButton> GetLevels()
    {
        List<NormalLevelButton> normalLevelButtons_ = FindObjectsOfType<NormalLevelButton>().ToList();
        return normalLevelButtons_;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}



