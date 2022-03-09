using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#region Explicação

/* 
Era para ser o Manager de UI do jogo mas, como você já deve ter visto, fiz uma confusão com aonde gerenciar a UI de cada parte, criando vários
scripts pra isso.

Atualmente, ele gerencia a UI dos dados do jogador (vida, moedas etc) da aba Home e o texto de nível do jogador da aba perfil
 
*/

#endregion

public class MenuUiManager : SingletonSingleScene<MenuUiManager>
{
    [Header("Character Bar's UI Elements")]
    [SerializeField] PlayerData PlayerData;
    [SerializeField] TextMeshProUGUI CurrentLivesText;
    [SerializeField] TextMeshProUGUI CoinsCollectedText;
    [SerializeField] TextMeshProUGUI StarsCollectedText;
    [SerializeField] TextMeshProUGUI ProfileLevelText;
    [Space(50)]
    [Header("Config Window")]
    public GameObject ConfigWindow;
    [Space(50)]
    [Header("Question UI")]
    public TextMeshProUGUI QuestionLifeText;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            PlayerManager.Instance.OnChangePlayerData += SetCharacterBar;
            SetCharacterBar();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCharacterBar()
    {
        CurrentLivesText.text = PlayerManager.Instance.GetLife().ToString();
        CoinsCollectedText.text = PlayerManager.Instance.GetCoins().ToString();
        StarsCollectedText.text = PlayerManager.Instance.GetStars().ToString();
        ProfileLevelText.text = PlayerManager.Instance.GetProfileLevel();
    }

    public void SetQuestionLife(int lifeNumber)
    {
        QuestionLifeText.text = lifeNumber.ToString();
    }

}
