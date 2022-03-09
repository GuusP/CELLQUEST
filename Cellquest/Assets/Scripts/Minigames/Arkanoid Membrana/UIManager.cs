using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

#region Explicação

/* 
Gerencia a UI do jogador no minigame. Acho que pode ser melhorado (caso seja de interesse) juntando os scripts/managers de UI que estão no jogo
porque tem muitos scripts de UI, que tem função de gerenciar a UI, que podiam ser juntados entre si 
 
*/

#endregion

public class UIManager : MonoBehaviour
{
    #region  Singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public TextMeshProUGUI bricksDestroyedText;
    
    public void UpdateUIText(TextMeshProUGUI TMPro, string text)
    {
        TMPro.text = text;
    }

    public void UpdateUIImage(Image Image, Sprite Sprite)
    {
        Image.sprite = Sprite;
    }

    public void UpdateMembranaPlayerLivesUI(Image[] Image, Sprite Sprite, int PlayerLives)
    {
        for (int i = Image.Length - 1; i >= 0; i--)
        {
            if (i >= PlayerLives)
            {
                Image[i].enabled = false;
            }else
            {
                Image[i].enabled = true;
                Image[i].sprite = Sprite;
            }
        }
    }


}
