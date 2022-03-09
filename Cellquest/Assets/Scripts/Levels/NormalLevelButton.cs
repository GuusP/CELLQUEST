using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

#region Explicação

/* 
Script usado para definir os botões de nível
 
*/

#endregion

public class NormalLevelButton : BaseLevelButton, ISavable
{
    [SerializeField] private Sprite _unlockedImage;
    [SerializeField] private Sprite _lockedImage;
    [SerializeField] private bool _isLocked = true;
    [SerializeField] private int _levelNumber;
    [SerializeField] private TextMeshProUGUI _levelText;
    public override Sprite UnlockedImage { get { return _unlockedImage; } set { _unlockedImage = value; } }
    public override Sprite LockedImage { get { return _lockedImage; } set { _lockedImage = value; } }
    public override bool IsLocked { get { return _isLocked; } set { _isLocked = value; } }
    public override int LevelNumber { get { return _levelNumber; } set { _levelNumber = value; } }
    public override TextMeshProUGUI LevelText { get { return _levelText; } set { _levelText = value; } }
    [SerializeField] private GameObject[] StarsObjects;

    [HideInInspector] public string savePath;
    public int stars;
    public int teste;
    public LevelConsequenceSO levelConsequence;
    public PerguntaSO pergunta;
    public string sceneToGo;
    // Start is called before the first frame update

   
    void Start()
    {

      
        LevelText = GetComponentInChildren<TextMeshProUGUI>();
        int.TryParse(LevelText.text, out _levelNumber);
        savePath = "Level " + _levelNumber + ".dat";
        


        gameObject.name = "Level " + _levelNumber;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("starObj"))
            {
                StarsObjects[i] = transform.GetChild(i).gameObject;
            }
        }
        LoadData();
        LevelManager.Instance.normalLevelButtons = LevelManager.Instance.GetLevels();
        PlayerManager.Instance.UpdateStars();
        UpdateButtonImages();
        StartCoroutine(SaveEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SelectLevel()
    {
        if (!IsLocked)
        {
            LevelManager.OnSelectLevel.Invoke(LevelNumber);
        
        }
        
    }

    public override void UnlockLevel()
    {
        _isLocked = false;
    }

    private void UpdateButtonImages()
    {
        if (!IsLocked)
        {
            GetComponent<Image>().sprite = UnlockedImage;
            
            for (int i = 0; i < StarsObjects.Length; i++)
            {
                if (i == stars - 1)
                {
                    StarsObjects[i].SetActive(true);
                }
                else
                {
                    StarsObjects[i].SetActive(false);
                }
            }
        }

    }

    public void UpdateStarsNumber(int receivedStars)
    {
        if (receivedStars > stars)
        {
            stars = receivedStars;
        }
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.LevelData levelData = new SaveData.LevelData(); // cria uma nova variável do struct levelData para receber os dados desse level
        levelData.isLocked = IsLocked;
        levelData.stars = stars;
        levelData.levelNumber = LevelNumber;
        levelData.alreadyFinished = levelConsequence.alreadyFinished;
        a_SaveData.levelDatas.Add(levelData); // adiciona o struct de dados criado a partir dos dados desse level a lista de levelData
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {

        foreach (SaveData.LevelData levelData in a_SaveData.levelDatas)
        {
            if (LevelNumber == levelData.levelNumber)
            {
                IsLocked = levelData.isLocked;
                stars = levelData.stars;
                levelConsequence.alreadyFinished = levelData.alreadyFinished;
            }

        }
    }





    public override void SaveData()
    {
        List<NormalLevelButton> selectedLevel = new List<NormalLevelButton>();
        selectedLevel.Add(this);
        SaveDataManager.SaveJsonData(selectedLevel, savePath);
    }

    public override void LoadData()
    {
        List<NormalLevelButton> selectedLevel = new List<NormalLevelButton>();
        selectedLevel.Add(this);
        SaveDataManager.LoadJsonData(selectedLevel, savePath);
        UpdateButtonImages();
    }

}
