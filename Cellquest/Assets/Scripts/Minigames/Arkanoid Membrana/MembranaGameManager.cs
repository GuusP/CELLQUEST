using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

#region Explicação

/* 
GameManager do minigame da fase 4. define as variáveis de começar o jogo, de acabar o jogo, as vidas do player etc
 
*/

#endregion

public class MembranaGameManager : MonoBehaviour
{
    #region  Singleton
    private static MembranaGameManager _instance;

    public static MembranaGameManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake(){
        if (_instance != null){
            Destroy(gameObject);
        }else {
            _instance = this;
        }
    }
    #endregion
    [SerializeField] int playerLives;

    public int PlayerLives {
        get
        {
            return playerLives;
        }

        set
        {
            playerLives = value;
        }
    }
    public bool IsGameStarted {get; set;}
    public bool IsBallFlying { get; set; }
    public Action<Image[], Sprite, int> OnPlayerDamaged;
    public Action<Image[], Sprite, int> OnGameFinished;
    [SerializeField] Image[] LivesUI_Images;
    [SerializeField] Sprite LifeSprite;
    [SerializeField] OnPhaseEnded OnPhaseEndedScript;


    public AudioToPlay baterParede;
    public AudioToPlay baterBloco;
    public AudioToPlay ultimaBatidaBloco;
    public AudioManager AudioManager;

    
    void Start()
    {
        IsGameStarted = true;
        OnPlayerDamaged += UpdateGameState;
        OnPlayerDamaged += UIManager.Instance.UpdateMembranaPlayerLivesUI;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateGameState(Image[] image, Sprite sprite, int i)
    {
        if (i <= 0)
        {
            IsGameStarted = false;
            OnGameFinishedMethod(levelFinishedStatus.Lost);
        }
        else
        {
            IsGameStarted = true;
            BallsManager.Instance.InitializeBall();
        }

        
    }

    public void OnGameFinishedMethod(levelFinishedStatus levelFinishedStatus)
    {
        OnPhaseEndedScript.gameObject.SetActive(true);
        OnPhaseEndedScript.StarsAchieved = playerLives;
        OnPhaseEndedScript.levelFinishedStatus = levelFinishedStatus;
    }

    public void TakeDamage(int value)
    {
        PlayerLives -= value;
        OnPlayerDamaged?.Invoke(LivesUI_Images, LifeSprite, PlayerLives);
        IsBallFlying = false;
    }
}
