using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region Explicação

/* 
Usado para gerenciar a UI que deve aparecer quando uma fase acaba. Acho que pode ser melhorado (caso seja de interesse) juntando os scripts/managers de UI que estão no jogo
porque tem muitos scripts de UI, que tem função de gerenciar a UI, que podiam ser juntados entre si 
 
*/

#endregion

public class OnPhaseEnded : MonoBehaviour
{
    public int StarsAchieved;
    [SerializeField] GameObject StarsGameObject;
    [SerializeField] GameObject[] StarsGameObjects;
    [HideInInspector] public levelFinishedStatus levelFinishedStatus;
    public void Start()
    {
        for (int i = 0; i < StarsGameObject.transform.childCount; i++)
        {
            StarsGameObjects[i] = StarsGameObject.transform.GetChild(i).gameObject;
        }
        UpdateStarsImages();
    }

    public void UpdateStarsImages()
    {
        for (int i = 0; i < StarsGameObjects.Length; i++)
        {
            if (i == StarsAchieved - 1)
            {
                StarsGameObjects[i].gameObject.SetActive(true);
            }
            else
            {
                StarsGameObjects[i].gameObject.SetActive(false);
            }
        }
    }

    public void LoadNextScene()
    {
        LevelManager.Instance.LevelFinished(levelFinishedStatus, StarsAchieved);
    }
}
